namespace WindowsForms
{
    partial class EnrollmentUser
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
            components = new System.ComponentModel.Container();
            subjectDataGridView = new DataGridView();
            errorProvider1 = new ErrorProvider(components);
            inscripcionDataGridView = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            inscribirButton = new Button();
            panel3 = new Panel();
            eliminarButton = new Button();
            SalirButton = new Button();
            panel4 = new Panel();
            usuarioComboBox = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            panel2 = new Panel();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)subjectDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)inscripcionDataGridView).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // subjectDataGridView
            // 
            subjectDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            subjectDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            subjectDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            subjectDataGridView.Dock = DockStyle.Fill;
            subjectDataGridView.Location = new Point(3, 106);
            subjectDataGridView.Name = "subjectDataGridView";
            subjectDataGridView.Size = new Size(394, 312);
            subjectDataGridView.TabIndex = 4;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // inscripcionDataGridView
            // 
            inscripcionDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            inscripcionDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            inscripcionDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            inscripcionDataGridView.Dock = DockStyle.Fill;
            inscripcionDataGridView.Location = new Point(403, 106);
            inscripcionDataGridView.Name = "inscripcionDataGridView";
            inscripcionDataGridView.Size = new Size(394, 312);
            inscripcionDataGridView.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(inscripcionDataGridView, 1, 1);
            tableLayoutPanel1.Controls.Add(panel1, 0, 2);
            tableLayoutPanel1.Controls.Add(panel3, 1, 2);
            tableLayoutPanel1.Controls.Add(subjectDataGridView, 0, 1);
            tableLayoutPanel1.Controls.Add(panel4, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // panel1
            // 
            panel1.Controls.Add(inscribirButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 424);
            panel1.Name = "panel1";
            panel1.Size = new Size(394, 23);
            panel1.TabIndex = 8;
            // 
            // inscribirButton
            // 
            inscribirButton.Dock = DockStyle.Fill;
            inscribirButton.Location = new Point(0, 0);
            inscribirButton.Name = "inscribirButton";
            inscribirButton.Size = new Size(394, 23);
            inscribirButton.TabIndex = 5;
            inscribirButton.Text = "Inscribir";
            inscribirButton.UseVisualStyleBackColor = true;
            inscribirButton.Click += inscribirButton_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(eliminarButton);
            panel3.Controls.Add(SalirButton);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(403, 424);
            panel3.Name = "panel3";
            panel3.Size = new Size(394, 23);
            panel3.TabIndex = 9;
            // 
            // eliminarButton
            // 
            eliminarButton.Dock = DockStyle.Left;
            eliminarButton.Location = new Point(0, 0);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(168, 23);
            eliminarButton.TabIndex = 7;
            eliminarButton.Text = "Eliminar Inscripcion";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // SalirButton
            // 
            SalirButton.Dock = DockStyle.Right;
            SalirButton.Location = new Point(269, 0);
            SalirButton.Name = "SalirButton";
            SalirButton.Size = new Size(125, 23);
            SalirButton.TabIndex = 6;
            SalirButton.Text = "Salir";
            SalirButton.UseVisualStyleBackColor = true;
            SalirButton.Click += salirButton_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(usuarioComboBox);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(label2);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(394, 97);
            panel4.TabIndex = 10;
            // 
            // usuarioComboBox
            // 
            usuarioComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            usuarioComboBox.FormattingEnabled = true;
            usuarioComboBox.Location = new Point(9, 38);
            usuarioComboBox.Name = "usuarioComboBox";
            usuarioComboBox.Size = new Size(382, 23);
            usuarioComboBox.TabIndex = 3;
            usuarioComboBox.SelectedIndexChanged += usuarioComboBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 6);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 0;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Bottom;
            label2.Location = new Point(0, 82);
            label2.Name = "label2";
            label2.Size = new Size(116, 15);
            label2.TabIndex = 1;
            label2.Text = "Materias Disponibles";
            // 
            // panel2
            // 
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(403, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(394, 97);
            panel2.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Bottom;
            label4.Location = new Point(0, 82);
            label4.Name = "label4";
            label4.Size = new Size(76, 15);
            label4.TabIndex = 4;
            label4.Text = "Inscripciones";
            // 
            // EnrollmentUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "EnrollmentUser";
            Text = "Inscripcion de Usuario a Materias";
            Load += EnrollmentUser_Load;
            ((System.ComponentModel.ISupportInitialize)subjectDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)inscripcionDataGridView).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView subjectDataGridView;
        private ErrorProvider errorProvider1;
        private DataGridView inscripcionDataGridView;
        private TableLayoutPanel tableLayoutPanel1;
        private Button eliminarButton;
        private Label label4;
        private Label label1;
        private Label label2;
        private Button inscribirButton;
        private Button SalirButton;
        private ComboBox usuarioComboBox;
        private Panel panel1;
        private Panel panel3;
        private Panel panel4;
        private Panel panel2;
    }
}