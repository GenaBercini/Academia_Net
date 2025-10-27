namespace WindowsForms
{
    partial class NotasList
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
            studentDataGridView = new DataGridView();
            subjectDataGridView = new DataGridView();
            button1 = new Button();
            label3 = new Label();
            notasComboBox = new ComboBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            cargarNotas = new Button();
            panel2 = new Panel();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            label2 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel4 = new Panel();
            panel5 = new Panel();
            estudianteLabel = new Label();
            panel3 = new Panel();
            ((System.ComponentModel.ISupportInitialize)studentDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)subjectDataGridView).BeginInit();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // studentDataGridView
            // 
            studentDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            studentDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            studentDataGridView.Location = new Point(403, 3);
            studentDataGridView.Name = "studentDataGridView";
            studentDataGridView.Size = new Size(394, 271);
            studentDataGridView.TabIndex = 0;
            // 
            // subjectDataGridView
            // 
            subjectDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            subjectDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            subjectDataGridView.Location = new Point(3, 3);
            subjectDataGridView.Name = "subjectDataGridView";
            subjectDataGridView.Size = new Size(394, 271);
            subjectDataGridView.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(141, 65);
            button1.Name = "button1";
            button1.Size = new Size(556, 23);
            button1.TabIndex = 4;
            button1.Text = "Cargar Notas";
            button1.UseVisualStyleBackColor = true;
            button1.Click += cargarNotaButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(141, 20);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 2;
            label3.Text = "Nota :";
            // 
            // notasComboBox
            // 
            notasComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            notasComboBox.FormattingEnabled = true;
            notasComboBox.Location = new Point(193, 17);
            notasComboBox.Name = "notasComboBox";
            notasComboBox.Size = new Size(504, 23);
            notasComboBox.TabIndex = 3;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(193, 17);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(504, 23);
            comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(141, 20);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 2;
            label1.Text = "Nota :";
            // 
            // cargarNotas
            // 
            cargarNotas.Location = new Point(141, 65);
            cargarNotas.Name = "cargarNotas";
            cargarNotas.Size = new Size(556, 23);
            cargarNotas.TabIndex = 4;
            cargarNotas.Text = "Cargar Notas";
            cargarNotas.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(notasComboBox);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 100);
            panel2.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(cargarNotas);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(comboBox1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 350);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 100);
            panel1.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(subjectDataGridView, 0, 0);
            tableLayoutPanel1.Controls.Add(studentDataGridView, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 73);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(800, 277);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 52);
            label2.Name = "label2";
            label2.Size = new Size(108, 15);
            label2.TabIndex = 5;
            label2.Text = "Materias del Curso:\r\n";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(panel4, 0, 0);
            tableLayoutPanel2.Controls.Add(panel5, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(800, 73);
            tableLayoutPanel2.TabIndex = 7;
            // 
            // panel4
            // 
            panel4.Controls.Add(label2);
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(394, 67);
            panel4.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Controls.Add(estudianteLabel);
            panel5.Location = new Point(403, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(394, 67);
            panel5.TabIndex = 1;
            // 
            // estudianteLabel
            // 
            estudianteLabel.AutoSize = true;
            estudianteLabel.Dock = DockStyle.Bottom;
            estudianteLabel.Location = new Point(0, 52);
            estudianteLabel.Name = "estudianteLabel";
            estudianteLabel.Size = new Size(70, 15);
            estudianteLabel.TabIndex = 6;
            estudianteLabel.Text = "Estudiantes:";
            // 
            // panel3
            // 
            panel3.Controls.Add(tableLayoutPanel2);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(800, 73);
            panel3.TabIndex = 8;
            // 
            // NotasList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Name = "NotasList";
            Text = "Form1";
            Load += NotasList_Load;
            ((System.ComponentModel.ISupportInitialize)studentDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)subjectDataGridView).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView studentDataGridView;
        private DataGridView subjectDataGridView;
        private Button button1;
        private Label label3;
        private ComboBox notasComboBox;
        private ComboBox comboBox1;
        private Label label1;
        private Button cargarNotas;
        private Panel panel2;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel4;
        private Panel panel5;
        private Label estudianteLabel;
        private Panel panel3;
    }
}