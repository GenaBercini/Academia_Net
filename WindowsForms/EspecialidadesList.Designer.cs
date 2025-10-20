namespace WindowsForms
{
    partial class EspecialidadesList
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
            specialtiesDataGridView = new DataGridView();
            eliminarButton = new Button();
            modificarButton = new Button();
            agregarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)specialtiesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // specialtiesDataGridView
            // 
            specialtiesDataGridView.AllowUserToAddRows = false;
            specialtiesDataGridView.AllowUserToDeleteRows = false;
            specialtiesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            specialtiesDataGridView.Location = new Point(11, 10);
            specialtiesDataGridView.Margin = new Padding(2, 1, 2, 1);
            specialtiesDataGridView.MultiSelect = false;
            specialtiesDataGridView.Name = "specialtiesDataGridView";
            specialtiesDataGridView.ReadOnly = true;
            specialtiesDataGridView.RowHeadersWidth = 82;
            specialtiesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            specialtiesDataGridView.Size = new Size(862, 358);
            specialtiesDataGridView.TabIndex = 2;
            // 
            // eliminarButton
            // 
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(532, 395);
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
            modificarButton.Enabled = false;
            modificarButton.Location = new Point(663, 395);
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
            agregarButton.Location = new Point(792, 395);
            agregarButton.Margin = new Padding(2, 1, 2, 1);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(81, 22);
            agregarButton.TabIndex = 6;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // EspecialidadesList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 450);
            Controls.Add(agregarButton);
            Controls.Add(modificarButton);
            Controls.Add(eliminarButton);
            Controls.Add(specialtiesDataGridView);
            Name = "EspecialidadesList";
            Text = "Especialidades";
            ((System.ComponentModel.ISupportInitialize)specialtiesDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView specialtiesDataGridView;
        private Button eliminarButton;
        private Button modificarButton;
        private Button agregarButton;
    }
}