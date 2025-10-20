namespace WindowsForms
{
    partial class UserDetail
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
            txtNombre = new TextBox();
            nombreLabel = new Label();
            btnCreate = new Button();
            errorProvider = new ErrorProvider(components);
            cancelButton = new Button();
            apellidoLabel = new Label();
            txtApellido = new TextBox();
            emailLabel = new Label();
            txtEmail = new TextBox();
            idLabel = new Label();
            txtUserName = new TextBox();
            label1 = new Label();
            txtDni = new TextBox();
            txtPassword = new TextBox();
            txtAdress = new TextBox();
            label2 = new Label();
            label3 = new Label();
            comboTypeUser = new ComboBox();
            label4 = new Label();
            lblJobPosition = new Label();
            comboJobPosition = new ComboBox();
            txtStudentNumber = new TextBox();
            lblStudentNumber = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(11, 75);
            txtNombre.Margin = new Padding(2, 1, 2, 1);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(305, 23);
            txtNombre.TabIndex = 0;
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Location = new Point(11, 59);
            nombreLabel.Margin = new Padding(2, 0, 2, 0);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(51, 15);
            nombreLabel.TabIndex = 1;
            nombreLabel.Text = "Nombre";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(359, 296);
            btnCreate.Margin = new Padding(2, 1, 2, 1);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(81, 22);
            btnCreate.TabIndex = 2;
            btnCreate.Text = "Aceptar";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += aceptarButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(235, 296);
            cancelButton.Margin = new Padding(2, 1, 2, 1);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(81, 22);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancelar";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelarButton_Click;
            // 
            // apellidoLabel
            // 
            apellidoLabel.AutoSize = true;
            apellidoLabel.Location = new Point(359, 59);
            apellidoLabel.Margin = new Padding(2, 0, 2, 0);
            apellidoLabel.Name = "apellidoLabel";
            apellidoLabel.Size = new Size(51, 15);
            apellidoLabel.TabIndex = 5;
            apellidoLabel.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(359, 75);
            txtApellido.Margin = new Padding(2, 1, 2, 1);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(314, 23);
            txtApellido.TabIndex = 1;
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(11, 9);
            emailLabel.Margin = new Padding(2, 0, 2, 0);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(36, 15);
            emailLabel.TabIndex = 7;
            emailLabel.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(11, 25);
            txtEmail.Margin = new Padding(2, 1, 2, 1);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(662, 23);
            txtEmail.TabIndex = 2;
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(11, 109);
            idLabel.Margin = new Padding(2, 0, 2, 0);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(110, 15);
            idLabel.TabIndex = 11;
            idLabel.Text = "Nombre de Usuario";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(11, 125);
            txtUserName.Margin = new Padding(2, 1, 2, 1);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(305, 23);
            txtUserName.TabIndex = 0;
            txtUserName.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(359, 109);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(25, 15);
            label1.TabIndex = 13;
            label1.Text = "Dni";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(359, 125);
            txtDni.Margin = new Padding(2, 1, 2, 1);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(314, 23);
            txtDni.TabIndex = 12;
            txtDni.TabStop = false;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(359, 175);
            txtPassword.Margin = new Padding(2, 1, 2, 1);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(314, 23);
            txtPassword.TabIndex = 14;
            txtPassword.TabStop = false;
            // 
            // txtAdress
            // 
            txtAdress.Location = new Point(11, 175);
            txtAdress.Margin = new Padding(2, 1, 2, 1);
            txtAdress.Name = "txtAdress";
            txtAdress.Size = new Size(305, 23);
            txtAdress.TabIndex = 15;
            txtAdress.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 159);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 16;
            label2.Text = "Direccion";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(359, 159);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 17;
            label3.Text = "Contraseña";
            // 
            // comboTypeUser
            // 
            comboTypeUser.FormattingEnabled = true;
            comboTypeUser.Location = new Point(11, 231);
            comboTypeUser.Name = "comboTypeUser";
            comboTypeUser.Size = new Size(305, 23);
            comboTypeUser.TabIndex = 18;
            comboTypeUser.SelectedIndexChanged += comboTypeUser_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 213);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(89, 15);
            label4.TabIndex = 19;
            label4.Text = "Tipo de Usuario";
            // 
            // lblJobPosition
            // 
            lblJobPosition.AutoSize = true;
            lblJobPosition.Location = new Point(443, 213);
            lblJobPosition.Margin = new Padding(2, 0, 2, 0);
            lblJobPosition.Name = "lblJobPosition";
            lblJobPosition.Size = new Size(39, 15);
            lblJobPosition.TabIndex = 20;
            lblJobPosition.Text = "Cargo";
            // 
            // comboJobPosition
            // 
            comboJobPosition.FormattingEnabled = true;
            comboJobPosition.Location = new Point(384, 231);
            comboJobPosition.Name = "comboJobPosition";
            comboJobPosition.Size = new Size(262, 23);
            comboJobPosition.TabIndex = 21;
            // 
            // txtStudentNumber
            // 
            txtStudentNumber.Location = new Point(359, 231);
            txtStudentNumber.Margin = new Padding(2, 1, 2, 1);
            txtStudentNumber.Name = "txtStudentNumber";
            txtStudentNumber.Size = new Size(314, 23);
            txtStudentNumber.TabIndex = 22;
            txtStudentNumber.TabStop = false;
            // 
            // lblStudentNumber
            // 
            lblStudentNumber.AutoSize = true;
            lblStudentNumber.Location = new Point(359, 213);
            lblStudentNumber.Margin = new Padding(2, 0, 2, 0);
            lblStudentNumber.Name = "lblStudentNumber";
            lblStudentNumber.Size = new Size(42, 15);
            lblStudentNumber.TabIndex = 23;
            lblStudentNumber.Text = "Legajo";
            // 
            // UserDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 411);
            Controls.Add(lblStudentNumber);
            Controls.Add(txtStudentNumber);
            Controls.Add(comboJobPosition);
            Controls.Add(lblJobPosition);
            Controls.Add(label4);
            Controls.Add(comboTypeUser);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtAdress);
            Controls.Add(txtPassword);
            Controls.Add(label1);
            Controls.Add(txtDni);
            Controls.Add(idLabel);
            Controls.Add(txtUserName);
            Controls.Add(emailLabel);
            Controls.Add(txtEmail);
            Controls.Add(apellidoLabel);
            Controls.Add(txtApellido);
            Controls.Add(cancelButton);
            Controls.Add(btnCreate);
            Controls.Add(nombreLabel);
            Controls.Add(txtNombre);
            Margin = new Padding(2, 1, 2, 1);
            Name = "UserDetail";
            Text = "Usuario";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private Label nombreLabel;
        private Button btnCreate;
        private ErrorProvider errorProvider;
        private Button cancelButton;
        private Label apellidoLabel;
        private TextBox txtApellido;
        private Label emailLabel;
        private TextBox txtEmail;
        private Label idLabel;
        private TextBox txtUserName;
        private Label label1;
        private TextBox txtDni;
        private ComboBox comboTypeUser;
        private Label label3;
        private Label label2;
        private TextBox txtAdress;
        private TextBox txtPassword;
        private ComboBox comboJobPosition;
        private Label lblJobPosition;
        private Label label4;
        private Label lblStudentNumber;
        private TextBox txtStudentNumber;
    }
}