namespace WindowsForms
{
    partial class EspecialidadesDetalle
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
            aceptarButton = new Button();
            cancelarButton = new Button();
            duracionTextBox = new TextBox();
            descripcionTextBox = new TextBox();
            nombreLabel = new Label();
            label1 = new Label();
            label2 = new Label();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(177, 249);
            aceptarButton.Margin = new Padding(2, 1, 2, 1);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(81, 22);
            aceptarButton.TabIndex = 3;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(286, 249);
            cancelarButton.Margin = new Padding(2, 1, 2, 1);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(81, 22);
            cancelarButton.TabIndex = 4;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // duracionTextBox
            // 
            duracionTextBox.Location = new Point(146, 142);
            duracionTextBox.Margin = new Padding(2, 1, 2, 1);
            duracionTextBox.Name = "duracionTextBox";
            duracionTextBox.Size = new Size(221, 23);
            duracionTextBox.TabIndex = 5;
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(146, 92);
            descripcionTextBox.Margin = new Padding(2, 1, 2, 1);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(221, 23);
            descripcionTextBox.TabIndex = 8;
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Location = new Point(48, 95);
            nombreLabel.Margin = new Padding(2, 0, 2, 0);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(69, 15);
            nombreLabel.TabIndex = 13;
            nombreLabel.Text = "Descripcion";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 150);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 14;
            label1.Text = "Duracion";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(48, 194);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(0, 15);
            label2.TabIndex = 15;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // EspecialidadesDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(nombreLabel);
            Controls.Add(descripcionTextBox);
            Controls.Add(duracionTextBox);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Name = "EspecialidadesDetalle";
            Text = "Especialidades";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button aceptarButton;
        private Button cancelarButton;
        private TextBox duracionTextBox;
        private TextBox descripcionTextBox;
        private Label nombreLabel;
        private Label label1;
        private Label label2;
        private ErrorProvider errorProvider1;
    }
}