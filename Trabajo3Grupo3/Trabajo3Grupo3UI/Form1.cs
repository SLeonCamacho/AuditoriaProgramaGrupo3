using System;
using System.Data.SqlClient;
using System.Windows.Forms; // Asegúrate de que esto esté presente para usar el DatabaseAuditor

namespace Trabajo3Grupo3UI
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        private DatabaseAuditor auditor;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string servidor = txtServidor.Text;
            string baseDatos = txtBaseDatos.Text;
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            // Construir el string de conexión
            string connectionString = $"Server={servidor};Database={baseDatos};User Id={usuario};Password={contraseña};Encrypt=false;";

            // Intentar establecer la conexión
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                lblMensajeError.Text = ""; // Limpiar el mensaje de error
                auditor = new DatabaseAuditor(connectionString); // Inicializar el auditor
                grpOpciones.Visible = true; // Mostrar las opciones
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Conexión inválida, por favor verifique los datos ingresados.";
                grpOpciones.Visible = false; // Ocultar las opciones
            }
        }

        private void btnEjecutarOpcion_Click(object sender, EventArgs e)
        {
            if (auditor == null)
            {
                MessageBox.Show("No se ha establecido conexión con la base de datos.", "Error");
                return;
            }

            richTextBoxOutput.Clear(); // Limpiar el output anterior

            if (rbtnOpcion1.Checked)
            {
                auditor.CheckForeignKeyIndexes();
                richTextBoxOutput.Text = GetLogContent("ForeignKeyIndexLog.txt");
            }
            else if (rbtnOpcion2.Checked)
            {
                auditor.CheckOrphanRecords();
                richTextBoxOutput.Text = GetLogContent("OrphanRecordsLog.txt");
            }
            else if (rbtnOpcion3.Checked)
            {
                auditor.IdentifyMissingForeignKeys();
                richTextBoxOutput.Text = GetLogContent("MissingForeignKeysLog.txt");
            }
            else if (rbtnOpcion4.Checked)
            {
                auditor.CheckReferentialActions();
                richTextBoxOutput.Text = GetLogContent("ReferentialActionsLog.txt");
            }
            else if (rbtnOpcion5.Checked)
            {
                auditor.CheckConstraints();
                richTextBoxOutput.Text = GetLogContent("ConstraintsLog.txt");
            }
            else if (rbtnOpcion6.Checked)
            {
                auditor.CheckDuplicateKeys();
                richTextBoxOutput.Text = GetLogContent("DuplicateKeysLog.txt");
            }
            else if (rbtnOpcion7.Checked)
            {
                auditor.CheckTriggers();
                richTextBoxOutput.Text = GetLogContent("TriggersLog.txt");
            }
            else if (rbtnOpcion8.Checked)
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Seleccione una opción válida.", "Opción no válida");
            }
        }

        private string GetLogContent(string logFileName)
        {
            string logDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            string logFilePath = System.IO.Path.Combine(logDirectory, logFileName);

            if (System.IO.File.Exists(logFilePath))
            {
                return System.IO.File.ReadAllText(logFilePath);
            }
            else
            {
                return "No se encontró el archivo de log.";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Este método se ejecuta cuando el formulario se carga por primera vez.
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
