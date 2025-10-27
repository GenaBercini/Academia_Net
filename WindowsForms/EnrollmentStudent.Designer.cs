namespace WindowsForms
{
    partial class EnrollmentStudent
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
            courseComboBox = new ComboBox();
            enrollmentButton = new Button();
            courseLabel = new Label();
            dataGridView1 = new DataGridView();
            subjectLabel = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // courseComboBox
            // 
            courseComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            courseComboBox.FormattingEnabled = true;
            courseComboBox.Location = new Point(252, 12);
            courseComboBox.Name = "courseComboBox";
            courseComboBox.Size = new Size(281, 23);
            courseComboBox.TabIndex = 0;
            // 
            // enrollmentButton
            // 
            enrollmentButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            enrollmentButton.Location = new Point(252, 17);
            enrollmentButton.Name = "enrollmentButton";
            enrollmentButton.Size = new Size(281, 23);
            enrollmentButton.TabIndex = 1;
            enrollmentButton.Text = "Inscribirse";
            enrollmentButton.UseVisualStyleBackColor = true;
            // 
            // courseLabel
            // 
            courseLabel.AutoSize = true;
            courseLabel.Location = new Point(185, 15);
            courseLabel.Name = "courseLabel";
            courseLabel.Size = new Size(41, 15);
            courseLabel.TabIndex = 2;
            courseLabel.Text = "Curso:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 84);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(800, 303);
            dataGridView1.TabIndex = 3;
            // 
            // subjectLabel
            // 
            subjectLabel.AutoSize = true;
            subjectLabel.Location = new Point(3, 56);
            subjectLabel.Name = "subjectLabel";
            subjectLabel.Size = new Size(172, 15);
            subjectLabel.TabIndex = 4;
            subjectLabel.Text = "Materias Disponibles del Curso:";
            // 
            // panel1
            // 
            panel1.Controls.Add(subjectLabel);
            panel1.Controls.Add(courseLabel);
            panel1.Controls.Add(courseComboBox);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 84);
            panel1.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.Controls.Add(enrollmentButton);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 387);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 63);
            panel2.TabIndex = 6;
            // 
            // EnrollmentStudent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "EnrollmentStudent";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox courseComboBox;
        private Button enrollmentButton;
        private Label courseLabel;
        private DataGridView dataGridView1;
        private Label subjectLabel;
        private Panel panel1;
        private Panel panel2;
    }
}