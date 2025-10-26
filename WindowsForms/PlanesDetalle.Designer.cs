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
            añoCalendarioLabel = new Label();
            descripcionLabel = new Label();
            añoCalendarioTextBox = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            especialidadLabel = new Label();
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
            // añoCalendarioLabel
            // 
            añoCalendarioLabel.AutoSize = true;
            añoCalendarioLabel.Location = new Point(63, 250);
            añoCalendarioLabel.Margin = new Padding(2, 0, 2, 0);
            añoCalendarioLabel.Name = "añoCalendarioLabel";
            añoCalendarioLabel.Size = new Size(89, 15);
            añoCalendarioLabel.TabIndex = 18;
            añoCalendarioLabel.Text = "Año Calendario";
            // 
            // descripcionLabel
            // 
            descripcionLabel.AutoSize = true;
            descripcionLabel.Location = new Point(63, 189);
            descripcionLabel.Margin = new Padding(2, 0, 2, 0);
            descripcionLabel.Name = "descripcionLabel";
            descripcionLabel.Size = new Size(69, 15);
            descripcionLabel.TabIndex = 19;
            descripcionLabel.Text = "Descripcion";
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
            // especialidadLabel
            // 
            especialidadLabel.AutoSize = true;
            especialidadLabel.Location = new Point(63, 113);
            especialidadLabel.Margin = new Padding(2, 0, 2, 0);
            especialidadLabel.Name = "especialidadLabel";
            especialidadLabel.Size = new Size(72, 15);
            especialidadLabel.TabIndex = 21;
            especialidadLabel.Text = "Especialidad";
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
            Controls.Add(especialidadLabel);
            Controls.Add(añoCalendarioTextBox);
            Controls.Add(descripcionLabel);
            Controls.Add(añoCalendarioLabel);
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
        private Label añoCalendarioLabel;
        private Label descripcionLabel;
        private TextBox añoCalendarioTextBox;
        private ErrorProvider errorProvider1;
        private Label especialidadLabel;
        private ComboBox cmbEspecialidades;
    }
}