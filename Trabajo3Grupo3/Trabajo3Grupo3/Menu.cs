using System;

public class Menu
{
    private DatabaseAuditor auditor;

    public void Show()
    {
        // Solicitar la información de conexión
        Console.Write("Ingrese el nombre del servidor: ");
        string server = Console.ReadLine();

        Console.Write("Ingrese el nombre de la base de datos: ");
        string database = Console.ReadLine();

        Console.Write("Ingrese el nombre de usuario: ");
        string user = Console.ReadLine();

        Console.Write("Ingrese la contraseña: ");
        string password = Console.ReadLine();

        // Construir el string de conexión
        string connectionString = $"Server={server};Database={database};User Id={user};Password={password};Encrypt=false;";

        // Inicializar el auditor con el string de conexión proporcionado
        auditor = new DatabaseAuditor(connectionString);

        // Mostrar el menú
        while (true)
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Verificar índices de claves foráneas");
            Console.WriteLine("2. Verificar registros huérfanos");
            Console.WriteLine("3. Identificar claves foráneas faltantes");
            Console.WriteLine("4. Verificar acciones referenciales");
            Console.WriteLine("5. Verificar restricciones");
            Console.WriteLine("6. Verificar claves duplicadas");
            Console.WriteLine("7. Verificar triggers");
            Console.WriteLine("8. Salir");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    auditor.CheckForeignKeyIndexes();
                    Console.WriteLine("Verificación de índices de claves foráneas completada.");
                    break;
                case "2":
                    auditor.CheckOrphanRecords();
                    Console.WriteLine("Verificación de registros huérfanos completada.");
                    break;
                case "3":
                    auditor.IdentifyMissingForeignKeys();
                    Console.WriteLine("Identificación de claves foráneas faltantes completada.");
                    break;
                case "4":
                    auditor.CheckReferentialActions();
                    Console.WriteLine("Verificación de acciones referenciales completada.");
                    break;
                case "5":
                    auditor.CheckConstraints();
                    Console.WriteLine("Verificación de restricciones completada.");
                    break;
                case "6":
                    auditor.CheckDuplicateKeys();
                    Console.WriteLine("Verificación de claves duplicadas completada.");
                    break;
                case "7":
                    auditor.CheckTriggers();
                    Console.WriteLine("Verificación de triggers completada.");
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }
}
