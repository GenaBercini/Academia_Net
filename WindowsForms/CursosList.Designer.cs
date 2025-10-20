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
            ((System.ComponentModel.ISupportInitialize)coursesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // coursesDataGridView
            // 
            coursesDataGridView.AllowUserToAddRows = false;
            coursesDataGridView.AllowUserToDeleteRows = false;
            coursesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            coursesDataGridView.Location = new Point(11, 10);
            coursesDataGridView.Margin = new Padding(2, 1, 2, 1);
            coursesDataGridView.MultiSelect = false;
            coursesDataGridView.Name = "coursesDataGridView";
            coursesDataGridView.ReadOnly = true;
            coursesDataGridView.RowHeadersWidth = 82;
            coursesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            coursesDataGridView.Size = new Size(778, 392);
            coursesDataGridView.TabIndex = 1;
            // 
            // eliminarButton
            // 
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(252, 418);
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
            modificarButton.Enabled = false;
            modificarButton.Location = new Point(131, 418);
            modificarButton.Margin = new Padding(2, 1, 2, 1);
            modificarButton.Name = "modificarButton";
            modificarButton.Size = new Size(81, 22);
            modificarButton.TabIndex = 4;
            modificarButton.Text = "Modificar";
            modificarButton.UseVisualStyleBackColor = true;
            modificarButton.Click += modificarButton_Click;

            agregarButton.Location = new Point(11, 418);
            agregarButton.Margin = new Padding(2, 1, 2, 1);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(81, 22);
            agregarButton.TabIndex = 5;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += Cursos;
            // 
            // CursosList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo;
            ClientSize = new Size(800, 450);
            Controls.Add(agregarButton);
            Controls.Add(modificarButton);
            Controls.Add(eliminarButton);
            Controls.Add(coursesDataGridView);
            Name = "CursosList";
            Text = "Cursos";
            Click += Cursos;
            ((System.ComponentModel.ISupportInitialize)coursesDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView coursesDataGridView;
        private Button eliminarButton;
        private Button modificarButton;
        private Button agregarButton;
    }
}