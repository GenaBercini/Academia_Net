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
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)specialtiesDataGridView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // specialtiesDataGridView
            // 
            specialtiesDataGridView.AllowUserToAddRows = false;
            specialtiesDataGridView.AllowUserToDeleteRows = false;
            specialtiesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            specialtiesDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            specialtiesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            specialtiesDataGridView.Dock = DockStyle.Fill;
            specialtiesDataGridView.Location = new Point(0, 0);
            specialtiesDataGridView.Margin = new Padding(2, 1, 2, 1);
            specialtiesDataGridView.MultiSelect = false;
            specialtiesDataGridView.Name = "specialtiesDataGridView";
            specialtiesDataGridView.ReadOnly = true;
            specialtiesDataGridView.RowHeadersWidth = 82;
            specialtiesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            specialtiesDataGridView.Size = new Size(800, 405);
            specialtiesDataGridView.TabIndex = 2;
            // 
            // eliminarButton
            // 
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(704, 9);
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
            modificarButton.Location = new Point(584, 9);
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
            agregarButton.Location = new Point(464, 9);
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
            // EspecialidadesList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo;
            ClientSize = new Size(800, 450);
            Controls.Add(specialtiesDataGridView);
            Controls.Add(panel1);
            Name = "EspecialidadesList";
            Text = "Especialidades";
            ((System.ComponentModel.ISupportInitialize)specialtiesDataGridView).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView specialtiesDataGridView;
        private Button eliminarButton;
        private Button modificarButton;
        private Button agregarButton;
        private Panel panel1;
    }
}