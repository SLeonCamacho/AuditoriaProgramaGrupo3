namespace Trabajo3Grupo3UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.TextBox txtBaseDatos;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblServidor;
        private System.Windows.Forms.Label lblBaseDatos;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.Label lblMensajeError;
        private System.Windows.Forms.GroupBox grpOpciones;
        private System.Windows.Forms.RadioButton rbtnOpcion1;
        private System.Windows.Forms.RadioButton rbtnOpcion2;
        private System.Windows.Forms.RadioButton rbtnOpcion3;
        private System.Windows.Forms.RadioButton rbtnOpcion4;
        private System.Windows.Forms.RadioButton rbtnOpcion5;
        private System.Windows.Forms.RadioButton rbtnOpcion6;
        private System.Windows.Forms.RadioButton rbtnOpcion7;
        private System.Windows.Forms.RadioButton rbtnOpcion8;
        private System.Windows.Forms.Button btnEjecutarOpcion;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;

        private void InitializeComponent()
        {
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.txtBaseDatos = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblServidor = new System.Windows.Forms.Label();
            this.lblBaseDatos = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblContraseña = new System.Windows.Forms.Label();
            this.lblMensajeError = new System.Windows.Forms.Label();
            this.grpOpciones = new System.Windows.Forms.GroupBox();
            this.rbtnOpcion1 = new System.Windows.Forms.RadioButton();
            this.rbtnOpcion2 = new System.Windows.Forms.RadioButton();
            this.rbtnOpcion3 = new System.Windows.Forms.RadioButton();
            this.rbtnOpcion4 = new System.Windows.Forms.RadioButton();
            this.rbtnOpcion5 = new System.Windows.Forms.RadioButton();
            this.rbtnOpcion6 = new System.Windows.Forms.RadioButton();
            this.rbtnOpcion7 = new System.Windows.Forms.RadioButton();
            this.rbtnOpcion8 = new System.Windows.Forms.RadioButton();
            this.btnEjecutarOpcion = new System.Windows.Forms.Button();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.Resultado = new System.Windows.Forms.Label();
            this.grpOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(220, 70);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(150, 26);
            this.txtServidor.TabIndex = 2;
            // 
            // txtBaseDatos
            // 
            this.txtBaseDatos.Location = new System.Drawing.Point(220, 110);
            this.txtBaseDatos.Name = "txtBaseDatos";
            this.txtBaseDatos.Size = new System.Drawing.Size(150, 26);
            this.txtBaseDatos.TabIndex = 4;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(220, 150);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(150, 26);
            this.txtUsuario.TabIndex = 6;
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(220, 190);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(150, 26);
            this.txtContraseña.TabIndex = 8;
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Location = new System.Drawing.Point(150, 230);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(100, 30);
            this.btnEjecutar.TabIndex = 9;
            this.btnEjecutar.Text = "Ingresar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(100, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(173, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "AuditorDB";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitulo.Click += new System.EventHandler(this.lblTitulo_Click);
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Location = new System.Drawing.Point(20, 70);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(225, 20);
            this.lblServidor.TabIndex = 1;
            this.lblServidor.Text = "Ingrese el nombre del servidor:";
            // 
            // lblBaseDatos
            // 
            this.lblBaseDatos.AutoSize = true;
            this.lblBaseDatos.Location = new System.Drawing.Point(20, 110);
            this.lblBaseDatos.Name = "lblBaseDatos";
            this.lblBaseDatos.Size = new System.Drawing.Size(284, 20);
            this.lblBaseDatos.TabIndex = 3;
            this.lblBaseDatos.Text = "Ingrese el nombre de la base de datos:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(20, 150);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(222, 20);
            this.lblUsuario.TabIndex = 5;
            this.lblUsuario.Text = "Ingrese el nombre del usuario:";
            // 
            // lblContraseña
            // 
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.Location = new System.Drawing.Point(20, 190);
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(248, 20);
            this.lblContraseña.TabIndex = 7;
            this.lblContraseña.Text = "Ingrese la contraseña del usuario:";
            // 
            // lblMensajeError
            // 
            this.lblMensajeError.AutoSize = true;
            this.lblMensajeError.ForeColor = System.Drawing.Color.Red;
            this.lblMensajeError.Location = new System.Drawing.Point(100, 270);
            this.lblMensajeError.Name = "lblMensajeError";
            this.lblMensajeError.Size = new System.Drawing.Size(0, 20);
            this.lblMensajeError.TabIndex = 10;
            // 
            // grpOpciones
            // 
            this.grpOpciones.Controls.Add(this.rbtnOpcion1);
            this.grpOpciones.Controls.Add(this.rbtnOpcion2);
            this.grpOpciones.Controls.Add(this.rbtnOpcion3);
            this.grpOpciones.Controls.Add(this.rbtnOpcion4);
            this.grpOpciones.Controls.Add(this.rbtnOpcion5);
            this.grpOpciones.Controls.Add(this.rbtnOpcion6);
            this.grpOpciones.Controls.Add(this.rbtnOpcion7);
            this.grpOpciones.Controls.Add(this.rbtnOpcion8);
            this.grpOpciones.Controls.Add(this.btnEjecutarOpcion);
            this.grpOpciones.Location = new System.Drawing.Point(20, 300);
            this.grpOpciones.Name = "grpOpciones";
            this.grpOpciones.Size = new System.Drawing.Size(350, 210);
            this.grpOpciones.TabIndex = 11;
            this.grpOpciones.TabStop = false;
            this.grpOpciones.Text = "Seleccione una opción";
            this.grpOpciones.Visible = false;
            // 
            // rbtnOpcion1
            // 
            this.rbtnOpcion1.AutoSize = true;
            this.rbtnOpcion1.Location = new System.Drawing.Point(10, 20);
            this.rbtnOpcion1.Name = "rbtnOpcion1";
            this.rbtnOpcion1.Size = new System.Drawing.Size(299, 24);
            this.rbtnOpcion1.TabIndex = 0;
            this.rbtnOpcion1.TabStop = true;
            this.rbtnOpcion1.Text = "1. Verificar índices de claves foráneas";
            // 
            // rbtnOpcion2
            // 
            this.rbtnOpcion2.AutoSize = true;
            this.rbtnOpcion2.Location = new System.Drawing.Point(10, 40);
            this.rbtnOpcion2.Name = "rbtnOpcion2";
            this.rbtnOpcion2.Size = new System.Drawing.Size(250, 24);
            this.rbtnOpcion2.TabIndex = 1;
            this.rbtnOpcion2.TabStop = true;
            this.rbtnOpcion2.Text = "2. Verificar registros huérfanos";
            // 
            // rbtnOpcion3
            // 
            this.rbtnOpcion3.AutoSize = true;
            this.rbtnOpcion3.Location = new System.Drawing.Point(10, 60);
            this.rbtnOpcion3.Name = "rbtnOpcion3";
            this.rbtnOpcion3.Size = new System.Drawing.Size(302, 24);
            this.rbtnOpcion3.TabIndex = 2;
            this.rbtnOpcion3.TabStop = true;
            this.rbtnOpcion3.Text = "3. Identificar claves foráneas faltantes";
            // 
            // rbtnOpcion4
            // 
            this.rbtnOpcion4.AutoSize = true;
            this.rbtnOpcion4.Location = new System.Drawing.Point(10, 80);
            this.rbtnOpcion4.Name = "rbtnOpcion4";
            this.rbtnOpcion4.Size = new System.Drawing.Size(271, 24);
            this.rbtnOpcion4.TabIndex = 3;
            this.rbtnOpcion4.TabStop = true;
            this.rbtnOpcion4.Text = "4. Verificar acciones referenciales";
            // 
            // rbtnOpcion5
            // 
            this.rbtnOpcion5.AutoSize = true;
            this.rbtnOpcion5.Location = new System.Drawing.Point(10, 100);
            this.rbtnOpcion5.Name = "rbtnOpcion5";
            this.rbtnOpcion5.Size = new System.Drawing.Size(202, 24);
            this.rbtnOpcion5.TabIndex = 4;
            this.rbtnOpcion5.TabStop = true;
            this.rbtnOpcion5.Text = "5. Verificar restricciones";
            // 
            // rbtnOpcion6
            // 
            this.rbtnOpcion6.AutoSize = true;
            this.rbtnOpcion6.Location = new System.Drawing.Point(10, 120);
            this.rbtnOpcion6.Name = "rbtnOpcion6";
            this.rbtnOpcion6.Size = new System.Drawing.Size(237, 24);
            this.rbtnOpcion6.TabIndex = 5;
            this.rbtnOpcion6.TabStop = true;
            this.rbtnOpcion6.Text = "6. Verificar claves duplicadas";
            // 
            // rbtnOpcion7
            // 
            this.rbtnOpcion7.AutoSize = true;
            this.rbtnOpcion7.Location = new System.Drawing.Point(10, 140);
            this.rbtnOpcion7.Name = "rbtnOpcion7";
            this.rbtnOpcion7.Size = new System.Drawing.Size(166, 24);
            this.rbtnOpcion7.TabIndex = 6;
            this.rbtnOpcion7.TabStop = true;
            this.rbtnOpcion7.Text = "7. Verificar triggers";
            // 
            // rbtnOpcion8
            // 
            this.rbtnOpcion8.AutoSize = true;
            this.rbtnOpcion8.Location = new System.Drawing.Point(10, 160);
            this.rbtnOpcion8.Name = "rbtnOpcion8";
            this.rbtnOpcion8.Size = new System.Drawing.Size(82, 24);
            this.rbtnOpcion8.TabIndex = 7;
            this.rbtnOpcion8.TabStop = true;
            this.rbtnOpcion8.Text = "8. Salir";
            // 
            // btnEjecutarOpcion
            // 
            this.btnEjecutarOpcion.Location = new System.Drawing.Point(120, 180);
            this.btnEjecutarOpcion.Name = "btnEjecutarOpcion";
            this.btnEjecutarOpcion.Size = new System.Drawing.Size(100, 30);
            this.btnEjecutarOpcion.TabIndex = 8;
            this.btnEjecutarOpcion.Text = "Ejecutar";
            this.btnEjecutarOpcion.UseVisualStyleBackColor = true;
            this.btnEjecutarOpcion.Click += new System.EventHandler(this.btnEjecutarOpcion_Click);
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Location = new System.Drawing.Point(438, 70);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(543, 440);
            this.richTextBoxOutput.TabIndex = 12;
            this.richTextBoxOutput.Text = "";
            // 
            // Resultado
            // 
            this.Resultado.AutoSize = true;
            this.Resultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resultado.Location = new System.Drawing.Point(431, 20);
            this.Resultado.Name = "Resultado";
            this.Resultado.Size = new System.Drawing.Size(169, 37);
            this.Resultado.TabIndex = 13;
            this.Resultado.Text = "Resultado";
            this.Resultado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1014, 535);
            this.Controls.Add(this.Resultado);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblServidor);
            this.Controls.Add(this.txtServidor);
            this.Controls.Add(this.lblBaseDatos);
            this.Controls.Add(this.txtBaseDatos);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblContraseña);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.lblMensajeError);
            this.Controls.Add(this.grpOpciones);
            this.Controls.Add(this.richTextBoxOutput);
            this.Name = "Form1";
            this.Text = "AuditorDB";
            this.grpOpciones.ResumeLayout(false);
            this.grpOpciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label Resultado;
    }
}
