namespace WindowsForms
{
    partial class SubjectDetail
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
            descripcionSubjectTextBox = new TextBox();
            horasSemanalesTextBox = new TextBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            checkBoxObligatoriaSi = new CheckBox();
            checkBoxObligatoriaNo = new CheckBox();
            errorProvider1 = new ErrorProvider(components);
            label4 = new Label();
            planComboBox = new ComboBox();
            label5 = new Label();
            añoSubjectTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(66, 159);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 2;
            label1.Text = "Descripcion";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(66, 214);
            label2.Name = "label2";
            label2.Size = new Size(97, 15);
            label2.TabIndex = 3;
            label2.Text = "Horas Semanales";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 270);
            label3.Name = "label3";
            label3.Size = new Size(66, 15);
            label3.TabIndex = 4;
            label3.Text = "Obligatoria";
            // 
            // descripcionSubjectTextBox
            // 
            descripcionSubjectTextBox.Location = new Point(214, 159);
            descripcionSubjectTextBox.Name = "descripcionSubjectTextBox";
            descripcionSubjectTextBox.Size = new Size(274, 23);
            descripcionSubjectTextBox.TabIndex = 7;
            descripcionSubjectTextBox.KeyPress += DescripcionSubjectTextBox_KeyPress;
            descripcionSubjectTextBox.Leave += DescripcionSubjectTextBox_Leave;
            // 
            // horasSemanalesTextBox
            // 
            horasSemanalesTextBox.Location = new Point(214, 206);
            horasSemanalesTextBox.Name = "horasSemanalesTextBox";
            horasSemanalesTextBox.Size = new Size(274, 23);
            horasSemanalesTextBox.TabIndex = 8;
            horasSemanalesTextBox.KeyPress += HorasSemanalesTextBox_KeyPress;
            horasSemanalesTextBox.Leave += HorasSemanalesTextBox_Leave;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(407, 333);
            aceptarButton.Margin = new Padding(2, 1, 2, 1);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(81, 22);
            aceptarButton.TabIndex = 10;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(214, 333);
            cancelarButton.Margin = new Padding(2, 1, 2, 1);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(81, 22);
            cancelarButton.TabIndex = 18;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // checkBoxObligatoriaSi
            // 
            checkBoxObligatoriaSi.AutoSize = true;
            checkBoxObligatoriaSi.Location = new Point(260, 269);
            checkBoxObligatoriaSi.Name = "checkBoxObligatoriaSi";
            checkBoxObligatoriaSi.Size = new Size(35, 19);
            checkBoxObligatoriaSi.TabIndex = 19;
            checkBoxObligatoriaSi.Text = "Si";
            checkBoxObligatoriaSi.UseVisualStyleBackColor = true;
            checkBoxObligatoriaSi.CheckedChanged += checkBoxObligatoriaSi_CheckedChanged;
            // 
            // checkBoxObligatoriaNo
            // 
            checkBoxObligatoriaNo.AutoSize = true;
            checkBoxObligatoriaNo.Location = new Point(407, 270);
            checkBoxObligatoriaNo.Name = "checkBoxObligatoriaNo";
            checkBoxObligatoriaNo.Size = new Size(42, 19);
            checkBoxObligatoriaNo.TabIndex = 20;
            checkBoxObligatoriaNo.Text = "No";
            checkBoxObligatoriaNo.UseVisualStyleBackColor = true;
            checkBoxObligatoriaNo.CheckedChanged += checkBoxObligatoriaNo_CheckedChanged;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(66, 55);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 21;
            label4.Text = "Plan";
            // 
            // planComboBox
            // 
            planComboBox.FormattingEnabled = true;
            planComboBox.Location = new Point(214, 52);
            planComboBox.Name = "planComboBox";
            planComboBox.Size = new Size(274, 23);
            planComboBox.TabIndex = 22;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(66, 109);
            label5.Name = "label5";
            label5.Size = new Size(29, 15);
            label5.TabIndex = 23;
            label5.Text = "Año";
            // 
            // añoSubjectTextBox
            // 
            añoSubjectTextBox.Location = new Point(214, 109);
            añoSubjectTextBox.Name = "añoSubjectTextBox";
            añoSubjectTextBox.Size = new Size(274, 23);
            añoSubjectTextBox.TabIndex = 24;
            añoSubjectTextBox.KeyPress += HorasSemanalesTextBox_KeyPress;
            // 
            // SubjectDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 411);
            Controls.Add(añoSubjectTextBox);
            Controls.Add(label5);
            Controls.Add(planComboBox);
            Controls.Add(label4);
            Controls.Add(checkBoxObligatoriaNo);
            Controls.Add(checkBoxObligatoriaSi);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(horasSemanalesTextBox);
            Controls.Add(descripcionSubjectTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "SubjectDetail";
            Text = "Materias";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox descripcionSubjectTextBox;
        private TextBox horasSemanalesTextBox;
        private Button aceptarButton;
        private Button cancelarButton;
        private CheckBox checkBoxObligatoriaSi;
        private CheckBox checkBoxObligatoriaNo;
        private ErrorProvider errorProvider1;
        private ComboBox planComboBox;
        private Label label4;
        private TextBox añoSubjectTextBox;
        private Label label5;
    }
}