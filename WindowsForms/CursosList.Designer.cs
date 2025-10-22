namespace WindowsForms
{
    partial class CursosList
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
            coursesDataGridView = new DataGridView();
            eliminarButton = new Button();
            modificarButton = new Button();
            agregarButton = new Button();
            bottomPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)coursesDataGridView).BeginInit();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // coursesDataGridView
            // 
            coursesDataGridView.AllowUserToAddRows = false;
            coursesDataGridView.AllowUserToDeleteRows = false;
            coursesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            coursesDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            coursesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            coursesDataGridView.Dock = DockStyle.Fill;
            coursesDataGridView.Location = new Point(0, 0);
            coursesDataGridView.Margin = new Padding(2, 1, 2, 1);
            coursesDataGridView.MultiSelect = false;
            coursesDataGridView.Name = "coursesDataGridView";
            coursesDataGridView.ReadOnly = true;
            coursesDataGridView.RowHeadersWidth = 82;
            coursesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            coursesDataGridView.Size = new Size(800, 405);
            coursesDataGridView.TabIndex = 1;
            // 
            // eliminarButton
            // 
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(706, 11);
            eliminarButton.Margin = new Padding(2, 1, 2, 1);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(81, 22);
            eliminarButton.TabIndex = 3;
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
            modificarButton.TabIndex = 4;
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
            agregarButton.TabIndex = 5;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += Cursos;
            // 
            // bottomPanel
            // 
            bottomPanel.BorderStyle = BorderStyle.FixedSingle;
            bottomPanel.Controls.Add(eliminarButton);
            bottomPanel.Controls.Add(agregarButton);
            bottomPanel.Controls.Add(modificarButton);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 405);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(800, 45);
            bottomPanel.TabIndex = 6;
            // 
            // CursosList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo;
            ClientSize = new Size(800, 450);
            Controls.Add(coursesDataGridView);
            Controls.Add(bottomPanel);
            Name = "CursosList";
            Text = "Cursos";
            Click += Cursos;
            ((System.ComponentModel.ISupportInitialize)coursesDataGridView).EndInit();
            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView coursesDataGridView;
        private Button eliminarButton;
        private Button modificarButton;
        private Button agregarButton;
        private Panel bottomPanel;
    }
}