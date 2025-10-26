namespace WindowsForms
{
    partial class CursosDetalle
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            turnoCursoTextBox = new TextBox();
            año_calendarioCursoTextBox = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            cupoCursoTextBox = new TextBox();
            añoCursoComboBox = new ComboBox();
            indiceCursoComboBox = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            specialtyComboBox = new ComboBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(403, 367);
            aceptarButton.Margin = new Padding(2, 1, 2, 1);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(81, 22);
            aceptarButton.TabIndex = 4;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(218, 367);
            cancelarButton.Margin = new Padding(2, 1, 2, 1);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(81, 22);
            cancelarButton.TabIndex = 18;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(65, 158);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 21;
            label2.Text = "Cupo";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(65, 37);
            label3.Name = "label3";
            label3.Size = new Size(58, 15);
            label3.TabIndex = 22;
            label3.Text = "Comision";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(65, 231);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 23;
            label4.Text = "Turno";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(65, 305);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 24;
            label5.Text = "Año Calendario";
            // 
            // turnoCursoTextBox
            // 
            turnoCursoTextBox.Location = new Point(218, 228);
            turnoCursoTextBox.Name = "turnoCursoTextBox";
            turnoCursoTextBox.Size = new Size(266, 23);
            turnoCursoTextBox.TabIndex = 29;
            turnoCursoTextBox.KeyPress += TurnoCursoTextBox_KeyPress;
            // 
            // año_calendarioCursoTextBox
            // 
            año_calendarioCursoTextBox.Location = new Point(218, 297);
            año_calendarioCursoTextBox.Name = "año_calendarioCursoTextBox";
            año_calendarioCursoTextBox.Size = new Size(266, 23);
            año_calendarioCursoTextBox.TabIndex = 30;
            año_calendarioCursoTextBox.KeyPress += año_calendarioCursoTextBox_KeyPress;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // cupoCursoTextBox
            // 
            cupoCursoTextBox.Location = new Point(218, 155);
            cupoCursoTextBox.Name = "cupoCursoTextBox";
            cupoCursoTextBox.Size = new Size(266, 23);
            cupoCursoTextBox.TabIndex = 27;
            cupoCursoTextBox.KeyPress += cupoCursoTextBox_KeyPress;
            // 
            // añoCursoComboBox
            // 
            añoCursoComboBox.FormattingEnabled = true;
            añoCursoComboBox.Location = new Point(218, 34);
            añoCursoComboBox.Name = "añoCursoComboBox";
            añoCursoComboBox.Size = new Size(81, 23);
            añoCursoComboBox.TabIndex = 31;
            // 
            // indiceCursoComboBox
            // 
            indiceCursoComboBox.FormattingEnabled = true;
            indiceCursoComboBox.Location = new Point(362, 34);
            indiceCursoComboBox.Name = "indiceCursoComboBox";
            indiceCursoComboBox.Size = new Size(122, 23);
            indiceCursoComboBox.TabIndex = 32;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(218, 6);
            label6.Name = "label6";
            label6.Size = new Size(29, 15);
            label6.TabIndex = 33;
            label6.Text = "Año";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(362, 6);
            label7.Name = "label7";
            label7.Size = new Size(39, 15);
            label7.TabIndex = 34;
            label7.Text = "Indice";
            // 
            // specialtyComboBox
            // 
            specialtyComboBox.FormattingEnabled = true;
            specialtyComboBox.Location = new Point(218, 94);
            specialtyComboBox.Name = "specialtyComboBox";
            specialtyComboBox.Size = new Size(266, 23);
            specialtyComboBox.TabIndex = 35;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(67, 97);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 36;
            label1.Text = "Especialidad";
            // 
            // CursosDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 411);
            Controls.Add(label1);
            Controls.Add(specialtyComboBox);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(indiceCursoComboBox);
            Controls.Add(añoCursoComboBox);
            Controls.Add(año_calendarioCursoTextBox);
            Controls.Add(turnoCursoTextBox);
            Controls.Add(cupoCursoTextBox);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Name = "CursosDetalle";
            Text = "Cursos";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button aceptarButton;
        private Button cancelarButton;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox turnoCursoTextBox;
        private TextBox año_calendarioCursoTextBox;
        private ErrorProvider errorProvider1;
        private Label label7;
        private Label label6;
        private ComboBox indiceCursoComboBox;
        private ComboBox añoCursoComboBox;
        private TextBox cupoCursoTextBox;
        private Label label1;
        private ComboBox specialtyComboBox;
    }
}