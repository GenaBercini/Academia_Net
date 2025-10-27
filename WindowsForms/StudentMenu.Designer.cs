namespace WindowsForms
{
    partial class StudentMenu
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
            button1 = new Button();
            button6 = new Button();
            button2 = new Button();
            button3 = new Button();
            primaryPanel = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(122, 450);
            panel1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Bottom;
            button1.Location = new Point(0, 395);
            button1.Name = "button1";
            button1.Size = new Size(122, 26);
            button1.TabIndex = 7;
            button1.Text = "Profile";
            button1.UseVisualStyleBackColor = true;
            button1.Click += profileButton_Click;
            // 
            // button6
            // 
            button6.Location = new Point(3, 3);
            button6.Name = "button6";
            button6.Size = new Size(116, 45);
            button6.TabIndex = 6;
            button6.Text = "Inscribir";
            button6.UseVisualStyleBackColor = true;
            button6.Click += inscribirButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(3, 54);
            button2.Name = "button2";
            button2.Size = new Size(116, 45);
            button2.TabIndex = 3;
            button2.Text = "Materias";
            button2.UseVisualStyleBackColor = true;
            button2.Click += materiasButton_Click;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Bottom;
            button3.Location = new Point(0, 421);
            button3.Name = "button3";
            button3.Size = new Size(122, 29);
            button3.TabIndex = 2;
            button3.Text = "Cerrar Sesion";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Logout;
            // 
            // primaryPanel
            // 
            primaryPanel.Dock = DockStyle.Fill;
            primaryPanel.Location = new Point(122, 0);
            primaryPanel.Name = "primaryPanel";
            primaryPanel.Size = new Size(678, 450);
            primaryPanel.TabIndex = 2;
            // 
            // StudentMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(primaryPanel);
            Controls.Add(panel1);
            Name = "StudentMenu";
            Text = "StudentMenu";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button6;
        private Button button2;
        private Button button3;
        private Button button1;
        private Panel primaryPanel;
    }
}