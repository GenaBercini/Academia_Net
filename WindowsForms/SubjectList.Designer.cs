namespace WindowsForms
{
    partial class SubjectList
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
            subjectsDataGridView = new DataGridView();
            eliminarButton = new Button();
            modificarButton = new Button();
            agregarButton = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)subjectsDataGridView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // subjectsDataGridView
            // 
            subjectsDataGridView.AllowUserToAddRows = false;
            subjectsDataGridView.AllowUserToDeleteRows = false;
            subjectsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            subjectsDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            subjectsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            subjectsDataGridView.Dock = DockStyle.Fill;
            subjectsDataGridView.Location = new Point(0, 0);
            subjectsDataGridView.Margin = new Padding(2, 1, 2, 1);
            subjectsDataGridView.MultiSelect = false;
            subjectsDataGridView.Name = "subjectsDataGridView";
            subjectsDataGridView.ReadOnly = true;
            subjectsDataGridView.RowHeadersWidth = 82;
            subjectsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            subjectsDataGridView.Size = new Size(800, 405);
            subjectsDataGridView.TabIndex = 2;
            // 
            // eliminarButton
            // 
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(706, 11);
            eliminarButton.Margin = new Padding(2, 1, 2, 1);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(81, 22);
            eliminarButton.TabIndex = 4;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // modificarButton
            // 
            modificarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            modificarButton.Enabled = false;
            modificarButton.Location = new Point(586, 11);
            modificarButton.Margin = new Padding(2, 1, 2, 1);
            modificarButton.Name = "modificarButton";
            modificarButton.Size = new Size(81, 22);
            modificarButton.TabIndex = 5;
            modificarButton.Text = "Modificar";
            modificarButton.UseVisualStyleBackColor = true;
            modificarButton.Click += modificarButton_Click;
            // 
            // agregarButton
            // 
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(466, 11);
            agregarButton.Margin = new Padding(2, 1, 2, 1);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(81, 22);
            agregarButton.TabIndex = 6;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(agregarButton);
            panel1.Controls.Add(eliminarButton);
            panel1.Controls.Add(modificarButton);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 405);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 45);
            panel1.TabIndex = 7;
            // 
            // SubjectList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo;
            ClientSize = new Size(800, 450);
            Controls.Add(subjectsDataGridView);
            Controls.Add(panel1);
            Name = "SubjectList";
            Text = "Materias";
            ((System.ComponentModel.ISupportInitialize)subjectsDataGridView).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView subjectsDataGridView;
        private Button eliminarButton;
        private Button modificarButton;
        private Button agregarButton;
        private Panel panel1;
    }
}