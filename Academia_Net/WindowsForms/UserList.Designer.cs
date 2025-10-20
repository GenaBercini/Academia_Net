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
            ((System.ComponentModel.ISupportInitialize)UsersDataGridView).BeginInit();
            SuspendLayout();
            // 
            // UsersDataGridView
            // 
            UsersDataGridView.AllowUserToOrderColumns = true;
            UsersDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UsersDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            UsersDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            UsersDataGridView.Location = new Point(21, 18);
            UsersDataGridView.Margin = new Padding(2, 1, 2, 1);
            UsersDataGridView.MultiSelect = false;
            UsersDataGridView.Name = "UsersDataGridView";
            UsersDataGridView.ReadOnly = true;
            UsersDataGridView.RowHeadersWidth = 82;
            UsersDataGridView.RowTemplate.Height = 41;
            UsersDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            UsersDataGridView.Size = new Size(983, 280);
            UsersDataGridView.TabIndex = 0;
            // 
            // addUserButton
            // 
            addUserButton.Location = new Point(572, 401);
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
            deleteButton.Location = new Point(375, 401);
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
            updateButton.Location = new Point(474, 401);
            updateButton.Margin = new Padding(2, 1, 2, 1);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(81, 22);
            updateButton.TabIndex = 3;
            updateButton.Text = "Modificar";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += UpdateUserButton_Click;
            // 
            // UserList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1025, 433);
            Controls.Add(updateButton);
            Controls.Add(deleteButton);
            Controls.Add(addUserButton);
            Controls.Add(UsersDataGridView);
            Margin = new Padding(2, 1, 2, 1);
            Name = "UserList";
            Text = "Clientes";
            Load += Users_Load;
            ((System.ComponentModel.ISupportInitialize)UsersDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView UsersDataGridView;
        private Button addUserButton;
        private Button deleteButton;
        private Button updateButton;
    }
}