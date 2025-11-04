namespace WindowsForms
{
    partial class TeacherMenu
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
            notasButton = new Button();
            button3 = new Button();
            primaryPanel = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(button6);
            panel1.Controls.Add(notasButton);
            panel1.Controls.Add(button3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(122, 450);
            panel1.TabIndex = 1;
            // 
            // button6
            // 
            button6.Dock = DockStyle.Bottom;
            button6.Location = new Point(0, 392);
            button6.Name = "button6";
            button6.Size = new Size(122, 29);
            button6.TabIndex = 6;
            button6.Text = "Profile";
            button6.UseVisualStyleBackColor = true;
            button6.Click += btnProfile_Click;
            // 
            // notasButton
            // 
            notasButton.Location = new Point(3, 3);
            notasButton.Name = "notasButton";
            notasButton.Size = new Size(116, 45);
            notasButton.TabIndex = 3;
            notasButton.Text = "Notas";
            notasButton.UseVisualStyleBackColor = true;
            notasButton.Click += notasButton_Click;
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
            // TeacherMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(primaryPanel);
            Controls.Add(panel1);
            Name = "TeacherMenu";
            Text = "TeacherMenu";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button notasButton;
        private Button button3;
        private Button button6;
        private Panel primaryPanel;
    }
}