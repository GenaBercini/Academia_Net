namespace WindowsForms
{
    partial class UserList
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UsersDataGridView = new DataGridView();
            addUserButton = new Button();
            deleteButton = new Button();
            updateButton = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)UsersDataGridView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // UsersDataGridView
            // 
            UsersDataGridView.AllowUserToOrderColumns = true;
            UsersDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UsersDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            UsersDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            UsersDataGridView.Dock = DockStyle.Fill;
            UsersDataGridView.Location = new Point(0, 0);
            UsersDataGridView.Margin = new Padding(2, 1, 2, 1);
            UsersDataGridView.MultiSelect = false;
            UsersDataGridView.Name = "UsersDataGridView";
            UsersDataGridView.ReadOnly = true;
            UsersDataGridView.RowHeadersWidth = 82;
            UsersDataGridView.RowTemplate.Height = 41;
            UsersDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            UsersDataGridView.Size = new Size(801, 403);
            UsersDataGridView.TabIndex = 0;
            // 
            // addUserButton
            // 
            addUserButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            addUserButton.Location = new Point(464, 9);
            addUserButton.Margin = new Padding(2, 1, 2, 1);
            addUserButton.Name = "addUserButton";
            addUserButton.Size = new Size(81, 22);
            addUserButton.TabIndex = 1;
            addUserButton.Text = "Agregar";
            addUserButton.UseVisualStyleBackColor = true;
            addUserButton.Click += AddUserButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deleteButton.Location = new Point(704, 9);
            deleteButton.Margin = new Padding(2, 1, 2, 1);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(81, 22);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "Eliminar";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += DeleteButton_Click;
            // 
            // updateButton
            // 
            updateButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            updateButton.Location = new Point(584, 9);
            updateButton.Margin = new Padding(2, 1, 2, 1);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(81, 22);
            updateButton.TabIndex = 3;
            updateButton.Text = "Modificar";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += UpdateUserButton_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(addUserButton);
            panel1.Controls.Add(deleteButton);
            panel1.Controls.Add(updateButton);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 403);
            panel1.Name = "panel1";
            panel1.Size = new Size(801, 45);
            panel1.TabIndex = 4;
            // 
            // UserList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = Properties.Resources.fondo;
            ClientSize = new Size(801, 448);
            Controls.Add(UsersDataGridView);
            Controls.Add(panel1);
            Margin = new Padding(2, 1, 2, 1);
            Name = "UserList";
            Text = "Clientes";
            Load += Users_Load;
            ((System.ComponentModel.ISupportInitialize)UsersDataGridView).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView UsersDataGridView;
        private Button addUserButton;
        private Button deleteButton;
        private Button updateButton;
        private Panel panel1;
    }
}