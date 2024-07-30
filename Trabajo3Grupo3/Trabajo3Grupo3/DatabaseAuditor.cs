using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

public class DatabaseAuditor
{
    private string connectionString;

    public DatabaseAuditor(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void CheckForeignKeyIndexes()
    {
        string logFilePath = "";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        fk.name AS ForeignKey,
                        tp.name AS ParentTable,
                        cp.name AS ParentColumn,
                        tr.name AS ReferencedTable,
                        cr.name AS ReferencedColumn
                    FROM 
                        sys.foreign_keys AS fk
                    JOIN 
                        sys.foreign_key_columns AS fkc ON fk.object_id = fkc.constraint_object_id
                    JOIN 
                        sys.tables AS tp ON fk.parent_object_id = tp.object_id
                    JOIN 
                        sys.columns AS cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id
                    JOIN 
                        sys.tables AS tr ON fk.referenced_object_id = tr.object_id
                    JOIN 
                        sys.columns AS cr ON fkc.referenced_column_id = cr.column_id AND fkc.referenced_object_id = cr.object_id
                    LEFT JOIN 
                        sys.index_columns AS ic ON ic.object_id = cp.object_id AND ic.column_id = cp.column_id
                    WHERE 
                        ic.index_id IS NULL";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                    Directory.CreateDirectory(logDirectory);
                    logFilePath = Path.Combine(logDirectory, "ForeignKeyIndexLog.txt");

                    using (StreamWriter writer = new StreamWriter(logFilePath))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine($"Anomalía de índice de clave foránea: {reader["ForeignKey"]}");
                        }
                    }
                }
            }

            Console.WriteLine("Log de verificación de índices de claves foráneas creado:");
            DisplayLog(logFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en CheckForeignKeyIndexes: {ex.Message}");
        }
    }

    public void CheckOrphanRecords()
    {
        string logFilePath = "";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        fk.name AS ForeignKey,
                        tp.name AS ParentTable,
                        cp.name AS ParentColumn,
                        tr.name AS ReferencedTable,
                        cr.name AS ReferencedColumn
                    FROM 
                        sys.foreign_keys AS fk
                    JOIN 
                        sys.foreign_key_columns AS fkc ON fk.object_id = fkc.constraint_object_id
                    JOIN 
                        sys.tables AS tp ON fk.parent_object_id = tp.object_id
                    JOIN 
                        sys.columns AS cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id
                    JOIN 
                        sys.tables AS tr ON fk.referenced_object_id = tr.object_id
                    JOIN 
                        sys.columns AS cr ON fkc.referenced_column_id = cr.column_id AND fkc.referenced_object_id = cr.object_id";

                var foreignKeys = new List<ForeignKeyInfo>();

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        foreignKeys.Add(new ForeignKeyInfo
                        {
                            ForeignKey = reader["ForeignKey"].ToString(),
                            ParentTable = reader["ParentTable"].ToString(),
                            ParentColumn = reader["ParentColumn"].ToString(),
                            ReferencedTable = reader["ReferencedTable"].ToString(),
                            ReferencedColumn = reader["ReferencedColumn"].ToString()
                        });
                    }
                }

                string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                Directory.CreateDirectory(logDirectory);
                logFilePath = Path.Combine(logDirectory, "OrphanRecordsLog.txt");

                using (StreamWriter writer = new StreamWriter(logFilePath))
                {
                    foreach (var fk in foreignKeys)
                    {
                        try
                        {
                            string orphanQuery = $@"
                                SELECT 
                                    tp.{fk.ParentColumn} AS OrphanRecord
                                FROM 
                                    {fk.ParentTable} tp
                                LEFT JOIN 
                                    {fk.ReferencedTable} tr ON tp.{fk.ParentColumn} = tr.{fk.ReferencedColumn}
                                WHERE 
                                    tr.{fk.ReferencedColumn} IS NULL";

                            using (SqlCommand orphanCommand = new SqlCommand(orphanQuery, connection))
                            using (SqlDataReader orphanReader = orphanCommand.ExecuteReader())
                            {
                                while (orphanReader.Read())
                                {
                                    writer.WriteLine($"Registro huérfano detectado en la tabla {fk.ParentTable}: {fk.ParentColumn} = {orphanReader["OrphanRecord"]}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine($"Error al procesar la clave foránea {fk.ForeignKey}: {ex.Message}");
                        }
                    }
                }
            }

            Console.WriteLine("Log de verificación de registros huérfanos creado:");
            DisplayLog(logFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en CheckOrphanRecords: {ex.Message}");
        }
    }

    public void IdentifyMissingForeignKeys()
    {
        string logFilePath = "";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string primaryKeyQuery = @"
                    SELECT 
                        t.name AS TableName,
                        c.name AS ColumnName
                    FROM 
                        sys.tables AS t
                    JOIN 
                        sys.indexes AS i ON t.object_id = i.object_id
                    JOIN 
                        sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                    JOIN 
                        sys.columns AS c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
                    WHERE 
                        i.is_primary_key = 1";

                var primaryKeys = new List<TableColumn>();

                using (SqlCommand command = new SqlCommand(primaryKeyQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        primaryKeys.Add(new TableColumn
                        {
                            TableName = reader["TableName"].ToString(),
                            ColumnName = reader["ColumnName"].ToString()
                        });
                    }
                }

                string foreignKeyQuery = @"
                    SELECT 
                        tp.name AS ParentTable,
                        cp.name AS ParentColumn,
                        tr.name AS ReferencedTable,
                        cr.name AS ReferencedColumn
                    FROM 
                        sys.foreign_keys AS fk
                    JOIN 
                        sys.foreign_key_columns AS fkc ON fk.object_id = fkc.constraint_object_id
                    JOIN 
                        sys.tables AS tp ON fk.parent_object_id = tp.object_id
                    JOIN 
                        sys.columns AS cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id
                    JOIN 
                        sys.tables AS tr ON fk.referenced_object_id = tr.object_id
                    JOIN 
                        sys.columns AS cr ON fkc.referenced_column_id = cr.column_id AND fkc.referenced_object_id = cr.object_id";

                var foreignKeys = new List<TableColumnPair>();

                using (SqlCommand command = new SqlCommand(foreignKeyQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        foreignKeys.Add(new TableColumnPair
                        {
                            ParentTable = reader["ParentTable"].ToString(),
                            ParentColumn = reader["ParentColumn"].ToString(),
                            ReferencedTable = reader["ReferencedTable"].ToString(),
                            ReferencedColumn = reader["ReferencedColumn"].ToString()
                        });
                    }
                }

                string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                Directory.CreateDirectory(logDirectory);
                logFilePath = Path.Combine(logDirectory, "MissingForeignKeysLog.txt");

                using (StreamWriter writer = new StreamWriter(logFilePath))
                {
                    foreach (var pk in primaryKeys)
                    {
                        bool found = false;
                        foreach (var fk in foreignKeys)
                        {
                            if (fk.ReferencedTable == pk.TableName && fk.ReferencedColumn == pk.ColumnName)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            writer.WriteLine($"Posible clave foránea faltante: {pk.TableName}({pk.ColumnName})");
                        }
                    }
                }
            }

            Console.WriteLine("Log de identificación de claves foráneas faltantes creado:");
            DisplayLog(logFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en IdentifyMissingForeignKeys: {ex.Message}");
        }
    }

    public void CheckReferentialActions()
    {
        string logFilePath = "";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        fk.name AS ForeignKey,
                        tp.name AS ParentTable,
                        tr.name AS ReferencedTable,
                        fk.delete_referential_action_desc AS DeleteAction,
                        fk.update_referential_action_desc AS UpdateAction,
                        fk.is_disabled AS IsDisabled
                    FROM 
                        sys.foreign_keys AS fk
                    JOIN 
                        sys.tables AS tp ON fk.parent_object_id = tp.object_id
                    JOIN 
                        sys.tables AS tr ON fk.referenced_object_id = tr.object_id";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                    Directory.CreateDirectory(logDirectory);
                    logFilePath = Path.Combine(logDirectory, "ReferentialActionsLog.txt");

                    using (StreamWriter writer = new StreamWriter(logFilePath))
                    {
                        while (reader.Read())
                        {
                            string deleteAction = reader["DeleteAction"].ToString();
                            string updateAction = reader["UpdateAction"].ToString();
                            bool isDisabled = (bool)reader["IsDisabled"];

                            if (isDisabled || deleteAction == "NO_ACTION" || updateAction == "NO_ACTION")
                            {
                                writer.WriteLine($"Clave foránea {reader["ForeignKey"]} en la tabla {reader["ParentTable"]} -> {reader["ReferencedTable"]} no tiene acciones definidas para eliminar o actualizar, o está deshabilitada.");
                            }
                            else
                            {
                                writer.WriteLine($"Clave foránea {reader["ForeignKey"]} en la tabla {reader["ParentTable"]} -> {reader["ReferencedTable"]}: Eliminar={deleteAction}, Actualizar={updateAction}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Log de verificación de acciones referenciales creado:");
            DisplayLog(logFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en CheckReferentialActions: {ex.Message}");
        }
    }

    public void CheckConstraints()
    {
        string logFilePath = "";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        t.name AS TableName,
                        c.name AS ColumnName,
                        cc.definition AS CheckConstraint
                    FROM 
                        sys.check_constraints AS cc
                    JOIN 
                        sys.tables AS t ON cc.parent_object_id = t.object_id
                    JOIN 
                        sys.columns AS c ON cc.parent_object_id = c.object_id AND cc.parent_column_id = c.column_id";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                    Directory.CreateDirectory(logDirectory);
                    logFilePath = Path.Combine(logDirectory, "ConstraintsLog.txt");

                    using (StreamWriter writer = new StreamWriter(logFilePath))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine($"Restricción: {reader["CheckConstraint"]} en la tabla {reader["TableName"]} para la columna {reader["ColumnName"]}");
                        }
                    }
                }
            }

            Console.WriteLine("Log de verificación de restricciones creado:");
            DisplayLog(logFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en CheckConstraints: {ex.Message}");
        }
    }

    public void CheckDuplicateKeys()
    {
        string logFilePath = "";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    WITH DuplicateKeys AS (
                        SELECT 
                            t.name AS TableName,
                            k.name AS KeyName,
                            c.name AS ColumnName,
                            ROW_NUMBER() OVER (PARTITION BY t.name, k.name ORDER BY c.name) AS RowNum
                        FROM 
                            sys.tables AS t
                        JOIN 
                            sys.key_constraints AS k ON t.object_id = k.parent_object_id
                        JOIN 
                            sys.index_columns AS ic ON k.unique_index_id = ic.index_id AND k.parent_object_id = ic.object_id
                        JOIN 
                            sys.columns AS c ON ic.column_id = c.column_id AND ic.object_id = c.object_id
                        WHERE 
                            k.type = 'PK'
                    )
                    SELECT 
                        TableName,
                        KeyName,
                        STRING_AGG(ColumnName, ', ') WITHIN GROUP (ORDER BY ColumnName) AS Columns
                    FROM 
                        DuplicateKeys
                    GROUP BY 
                        TableName,
                        KeyName
                    HAVING 
                        COUNT(*) > 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                    Directory.CreateDirectory(logDirectory);
                    logFilePath = Path.Combine(logDirectory, "DuplicateKeysLog.txt");

                    using (StreamWriter writer = new StreamWriter(logFilePath))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine($"Clave duplicada: {reader["KeyName"]} en la tabla {reader["TableName"]} para las columnas {reader["Columns"]}");
                        }
                    }
                }
            }

            Console.WriteLine("Log de verificación de claves duplicadas creado:");
            DisplayLog(logFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en CheckDuplicateKeys: {ex.Message}");
        }
    }

    public void CheckTriggers()
    {
        string logFilePath = "";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        t.name AS TableName,
                        tr.name AS TriggerName,
                        tr.is_disabled AS IsDisabled
                    FROM 
                        sys.triggers AS tr
                    JOIN 
                        sys.tables AS t ON tr.parent_id = t.object_id";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                    Directory.CreateDirectory(logDirectory);
                    logFilePath = Path.Combine(logDirectory, "TriggersLog.txt");

                    using (StreamWriter writer = new StreamWriter(logFilePath))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine($"Trigger: {reader["TriggerName"]} en la tabla {reader["TableName"]}, Estado: {(reader["IsDisabled"].Equals(true) ? "Deshabilitado" : "Habilitado")}");
                        }
                    }
                }
            }

            Console.WriteLine("Log de verificación de triggers creado:");
            DisplayLog(logFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en CheckTriggers: {ex.Message}");
        }
    }

    private void DisplayLog(string logFilePath)
    {
        try
        {
            using (StreamReader reader = new StreamReader(logFilePath))
            {
                Console.WriteLine("\n--------------------------------------");
                Console.WriteLine($"Contenido del log ({Path.GetFileName(logFilePath)}):");
                Console.WriteLine("--------------------------------------");

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }

                Console.WriteLine("--------------------------------------\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al mostrar el contenido del log: {ex.Message}");
        }
    }

    private class ForeignKeyInfo
    {
        public string ForeignKey { get; set; }
        public string ParentTable { get; set; }
        public string ParentColumn { get; set; }
        public string ReferencedTable { get; set; }
        public string ReferencedColumn { get; set; }
    }

    private class TableColumn
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
    }

    private class TableColumnPair
    {
        public string ParentTable { get; set; }
        public string ParentColumn { get; set; }
        public string ReferencedTable { get; set; }
        public string ReferencedColumn { get; set; }
    }
}
