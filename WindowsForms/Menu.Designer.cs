namespace WindowsForms
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button2 = new Button();
            button3 = new Button();
            button1 = new Button();
            primaryPanel = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(button6);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(122, 450);
            panel1.TabIndex = 0;
            // 
            // button6
            // 
            button6.Location = new Point(3, 207);
            button6.Name = "button6";
            button6.Size = new Size(116, 45);
            button6.TabIndex = 6;
            button6.Text = "Cursos";
            button6.UseVisualStyleBackColor = true;
            button6.Click += BtnCursos_Click;
            // 
            // button5
            // 
            button5.Location = new Point(3, 156);
            button5.Name = "button5";
            button5.Size = new Size(116, 45);
            button5.TabIndex = 5;
            button5.Text = "Especialidades";
            button5.UseVisualStyleBackColor = true;
            button5.Click += BtnEspecialidades_Click;
            // 
            // button4
            // 
            button4.Location = new Point(3, 105);
            button4.Name = "button4";
            button4.Size = new Size(116, 45);
            button4.TabIndex = 4;
            button4.Text = "Planes";
            button4.UseVisualStyleBackColor = true;
            button4.Click += BtnPlanes_Click;
            // 
            // button2
            // 
            button2.Location = new Point(3, 54);
            button2.Name = "button2";
            button2.Size = new Size(116, 45);
            button2.TabIndex = 3;
            button2.Text = "Materias";
            button2.UseVisualStyleBackColor = true;
            button2.Click += BtnMaterias_Click;
            // 
            // button3
            // 
            button3.Location = new Point(12, 418);
            button3.Name = "button3";
            button3.Size = new Size(97, 29);
            button3.TabIndex = 2;
            button3.Text = "Cerrar Sesion";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Logout;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(116, 45);
            button1.TabIndex = 0;
            button1.Text = "Usuarios";
            button1.UseVisualStyleBackColor = true;
            button1.Click += BtnUsuarios_Click;
            // 
            // primaryPanel
            // 
            primaryPanel.BackgroundImage = Properties.Resources.fondo;
            primaryPanel.Dock = DockStyle.Fill;
            primaryPanel.Location = new Point(122, 0);
            primaryPanel.Name = "primaryPanel";
            primaryPanel.Size = new Size(678, 450);
            primaryPanel.TabIndex = 1;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(primaryPanel);
            Controls.Add(panel1);
            Name = "Menu";
            Text = "Menu";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private Panel primaryPanel;
        private Button button3;
        private Button button2;
        private Button button4;
        private Button button6;
        private Button button5;
    }
}