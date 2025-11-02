namespace WindowsForms
{
    partial class UserProfile
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            chkShowPasswords = new CheckBox();
            txtNewPassword = new TextBox();
            txtCurrentPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            btnChangePassword = new Button();
            lblUser = new Label();
            lblName = new Label();
            lblEmail = new Label();
            lblDni = new Label();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 33);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 0;
            label2.Text = "Usuario:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 60);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 1;
            label3.Text = "Email: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 86);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 2;
            label4.Text = "DNI: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(12, 123);
            label5.Name = "label5";
            label5.Size = new Size(116, 15);
            label5.TabIndex = 3;
            label5.Text = "Cambiar contraseña";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 156);
            label6.Name = "label6";
            label6.Size = new Size(108, 15);
            label6.TabIndex = 4;
            label6.Text = "Contraseña actual: ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 186);
            label7.Name = "label7";
            label7.Size = new Size(108, 15);
            label7.TabIndex = 5;
            label7.Text = "Contraseña nueva: ";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 215);
            label8.Name = "label8";
            label8.Size = new Size(128, 15);
            label8.TabIndex = 6;
            label8.Text = "Confirmar contraseña: ";
            // 
            // chkShowPasswords
            // 
            chkShowPasswords.AutoSize = true;
            chkShowPasswords.Location = new Point(12, 243);
            chkShowPasswords.Name = "chkShowPasswords";
            chkShowPasswords.Size = new Size(133, 19);
            chkShowPasswords.TabIndex = 7;
            chkShowPasswords.Text = "Mostrar contraseñas";
            chkShowPasswords.UseVisualStyleBackColor = true;
            chkShowPasswords.CheckedChanged += chkShowPasswords_CheckedChanged;
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(146, 183);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(211, 23);
            txtNewPassword.TabIndex = 8;
            // 
            // txtCurrentPassword
            // 
            txtCurrentPassword.Location = new Point(146, 153);
            txtCurrentPassword.Name = "txtCurrentPassword";
            txtCurrentPassword.Size = new Size(211, 23);
            txtCurrentPassword.TabIndex = 9;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(146, 212);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(211, 23);
            txtConfirmPassword.TabIndex = 10;
            // 
            // btnChangePassword
            // 
            btnChangePassword.Location = new Point(112, 268);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(146, 23);
            btnChangePassword.TabIndex = 11;
            btnChangePassword.Text = "Actualizar contraseña";
            btnChangePassword.UseVisualStyleBackColor = true;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(68, 9);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(12, 15);
            lblUser.TabIndex = 12;
            lblUser.Text = "-";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(72, 33);
            lblName.Name = "lblName";
            lblName.Size = new Size(12, 15);
            lblName.TabIndex = 13;
            lblName.Text = "-";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(60, 60);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(12, 15);
            lblEmail.TabIndex = 14;
            lblEmail.Text = "-";
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(51, 86);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(12, 15);
            lblDni.TabIndex = 15;
            lblDni.Text = "-";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // UserProfile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblDni);
            Controls.Add(lblEmail);
            Controls.Add(lblName);
            Controls.Add(lblUser);
            Controls.Add(btnChangePassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtCurrentPassword);
            Controls.Add(txtNewPassword);
            Controls.Add(chkShowPasswords);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "UserProfile";
            Text = "UserProfile";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private CheckBox chkShowPasswords;
        private TextBox txtNewPassword;
        private TextBox txtCurrentPassword;
        private TextBox txtConfirmPassword;
        private Button btnChangePassword;
        private Label lblUser;
        private Label lblName;
        private Label lblEmail;
        private Label lblDni;
        private ErrorProvider errorProvider1;
    }
}