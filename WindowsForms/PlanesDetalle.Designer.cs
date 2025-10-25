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
            descripcionTextBox = new TextBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            label1 = new Label();
            label2 = new Label();
            añoCalendarioTextBox = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            label3 = new Label();
            cmbEspecialidades = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(237, 186);
            descripcionTextBox.Margin = new Padding(2, 1, 2, 1);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(251, 23);
            descripcionTextBox.TabIndex = 15;
            descripcionTextBox.KeyPress += DescripcionTextBox_KeyPress;
            descripcionTextBox.Leave += DescripcionTextBox_Leave;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(407, 335);
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
            cancelarButton.Location = new Point(237, 335);
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
            label1.Location = new Point(63, 250);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 18;
            label1.Text = "Año Calendario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(63, 189);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 19;
            label2.Text = "Descripcion";
            // 
            // añoCalendarioTextBox
            // 
            añoCalendarioTextBox.Location = new Point(237, 250);
            añoCalendarioTextBox.Margin = new Padding(2, 1, 2, 1);
            añoCalendarioTextBox.Name = "añoCalendarioTextBox";
            añoCalendarioTextBox.Size = new Size(251, 23);
            añoCalendarioTextBox.TabIndex = 20;
            añoCalendarioTextBox.KeyPress += AñoCalendarioTextBox_KeyPress;
            añoCalendarioTextBox.Leave += AñoCalendarioTextBox_Leave;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(63, 113);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 21;
            label3.Text = "Especialidad";
            // 
            // cmbEspecialidades
            // 
            cmbEspecialidades.FormattingEnabled = true;
            cmbEspecialidades.Location = new Point(237, 113);
            cmbEspecialidades.Name = "cmbEspecialidades";
            cmbEspecialidades.Size = new Size(251, 23);
            cmbEspecialidades.TabIndex = 22;
            // 
            // PlanesDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 411);
            Controls.Add(cmbEspecialidades);
            Controls.Add(label3);
            Controls.Add(añoCalendarioTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(descripcionTextBox);
            Name = "PlanesDetalle";
            Text = "Planes";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox descripcionTextBox;
        private Button aceptarButton;
        private Button cancelarButton;
        private Label label1;
        private Label label2;
        private TextBox añoCalendarioTextBox;
        private ErrorProvider errorProvider1;
        private Label label3;
        private ComboBox cmbEspecialidades;
    }
}