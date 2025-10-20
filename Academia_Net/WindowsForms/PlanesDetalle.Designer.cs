namespace WindowsForms
{
    partial class PlanesDetalle
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
            idLabel = new Label();
            idTextBox = new TextBox();
            descripcionTextBox = new TextBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            label1 = new Label();
            label2 = new Label();
            añoCalendarioTextBox = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(77, 45);
            idLabel.Margin = new Padding(2, 0, 2, 0);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(17, 15);
            idLabel.TabIndex = 13;
            idLabel.Text = "Id";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(141, 42);
            idTextBox.Margin = new Padding(2, 1, 2, 1);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(221, 23);
            idTextBox.TabIndex = 14;
            idTextBox.TabStop = false;
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(141, 107);
            descripcionTextBox.Margin = new Padding(2, 1, 2, 1);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(221, 23);
            descripcionTextBox.TabIndex = 15;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(176, 228);
            aceptarButton.Margin = new Padding(2, 1, 2, 1);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(81, 22);
            aceptarButton.TabIndex = 16;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(281, 228);
            cancelarButton.Margin = new Padding(2, 1, 2, 1);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(81, 22);
            cancelarButton.TabIndex = 17;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 169);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 18;
            label1.Text = "Año Calendario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(52, 110);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 19;
            label2.Text = "Descripcion";
            // 
            // añoCalendarioTextBox
            // 
            añoCalendarioTextBox.Location = new Point(141, 169);
            añoCalendarioTextBox.Margin = new Padding(2, 1, 2, 1);
            añoCalendarioTextBox.Name = "añoCalendarioTextBox";
            añoCalendarioTextBox.Size = new Size(221, 23);
            añoCalendarioTextBox.TabIndex = 20;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // PlanesDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(añoCalendarioTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(descripcionTextBox);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            Name = "PlanesDetalle";
            Text = "Planes";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private TextBox idTextBox;
        private TextBox descripcionTextBox;
        private Button aceptarButton;
        private Button cancelarButton;
        private Label label1;
        private Label label2;
        private TextBox añoCalendarioTextBox;
        private ErrorProvider errorProvider1;
    }
}