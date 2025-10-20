namespace WindowsForms
{
    partial class Login
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
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            loginButton = new Button();
            button2 = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(81, 19);
            label1.Name = "label1";
            label1.Size = new Size(112, 21);
            label1.TabIndex = 0;
            label1.Text = "Iniciar Sesion";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 63);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 1;
            label2.Text = "Usuario:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 113);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 2;
            label3.Text = "Contraseña:";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(30, 87);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(215, 23);
            usernameTextBox.TabIndex = 3;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(30, 131);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(215, 23);
            passwordTextBox.TabIndex = 4;
            passwordTextBox.KeyPress += passwordTextBox_KeyPress;
            // 
            // loginButton
            // 
            loginButton.Location = new Point(30, 173);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(75, 23);
            loginButton.TabIndex = 5;
            loginButton.Text = "Iniciar";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(170, 173);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += cancelButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(273, 241);
            Controls.Add(button2);
            Controls.Add(loginButton);
            Controls.Add(passwordTextBox);
            Controls.Add(usernameTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Login";
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Button button2;
        private ErrorProvider errorProvider;
    }
}