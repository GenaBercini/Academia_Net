using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class SubjectDetail : Form
    {
        private SubjectDTO? subject;
        private FormMode mode;

        public SubjectDTO Subject
        {
            get => subject;
            set
            {
                subject = value;
                LoadFormData();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set
            {
                mode = value;
                ApplyFormMode();
            }
        }

        public SubjectDetail()
        {
            InitializeComponent();
        }

        public SubjectDetail(FormMode mode, SubjectDTO subject) : this()
        {
            this.Mode = mode;
            this.Subject = subject;
        }

        private void ApplyFormMode()
        {
            if (Mode == FormMode.Add)
                this.Text = "Agregar Materia";
            else if (Mode == FormMode.Update)
                this.Text = "Editar Materia";
        }

        private void LoadFormData()
        {
            if (Subject != null && Mode == FormMode.Update)
            {
                descripcionSubjectTextBox.Text = Subject.Desc;
                horasSemanalesTextBox.Text = Subject.HsSemanales.ToString();
                checkBoxObligatoriaSi.Checked = Subject.Obligatoria;
                checkBoxObligatoriaNo.Checked = !Subject.Obligatoria;
            }
        }

        private void HorasSemanalesTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(horasSemanalesTextBox, "Solo se permiten números.");
            }
            else
            {
                errorProvider1.SetError(horasSemanalesTextBox, "");
            }
        }

        private void DescripcionSubjectTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(descripcionSubjectTextBox.Text))
            {
                errorProvider1.SetError(descripcionSubjectTextBox, "La descripción es obligatoria.");
            }
            else if (descripcionSubjectTextBox.Text.Length < 3)
            {
                errorProvider1.SetError(descripcionSubjectTextBox, "La descripción debe tener al menos 3 caracteres.");
            }

            else
            {
                errorProvider1.SetError(descripcionSubjectTextBox, "");
            }
        }

        private void HorasSemanalesTextBox_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(horasSemanalesTextBox.Text, out int horas) || horas <= 0)
            {
                errorProvider1.SetError(horasSemanalesTextBox, "Ingrese un número válido mayor que cero.");
            }
            else
            {
                errorProvider1.SetError(horasSemanalesTextBox, "");
            }
        }

        private bool ValidateSubject()
        {
            bool isValid = true;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(descripcionSubjectTextBox.Text))
            {
                errorProvider1.SetError(descripcionSubjectTextBox, "La descripción es obligatoria.");
                isValid = false;
            }
            else if (!Regex.IsMatch(descripcionSubjectTextBox.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                errorProvider1.SetError(descripcionSubjectTextBox, "La descripción solo puede contener letras.");
            }

            if (!int.TryParse(horasSemanalesTextBox.Text, out int horas) || horas <= 0)
            {
                errorProvider1.SetError(horasSemanalesTextBox, "Ingrese un número válido de horas semanales.");
                isValid = false;
            }

            if (!checkBoxObligatoriaSi.Checked && !checkBoxObligatoriaNo.Checked)
            {
                errorProvider1.SetError(checkBoxObligatoriaSi, "Debe seleccionar si es obligatoria o no.");
                isValid = false;
            }

            return isValid;
        }
        private void DescripcionSubjectTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; 
                errorProvider1.SetError(descripcionSubjectTextBox, "Solo se permiten letras.");
            }
            else
            {
                errorProvider1.SetError(descripcionSubjectTextBox, "");
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateSubject())
                return;

            try
            {
                if (Mode == FormMode.Add)
                {
                    var createSubject = new SubjectDTO
                    {
                        Desc = descripcionSubjectTextBox.Text.Trim(),
                        HsSemanales = int.Parse(horasSemanalesTextBox.Text),
                        Obligatoria = checkBoxObligatoriaSi.Checked,
                    };

                    await SubjectsApiClient.AddAsync(createSubject);
                }
                else
                {
                    var updateSubject = new SubjectDTO
                    {
                        Id = Subject.Id,
                        Desc = descripcionSubjectTextBox.Text.Trim(),
                        HsSemanales = int.Parse(horasSemanalesTextBox.Text),
                        Obligatoria = checkBoxObligatoriaSi.Checked,
                        Habilitado = Subject.Habilitado
                    };

                    await SubjectsApiClient.UpdateAsync(updateSubject);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la materia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxObligatoriaSi_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxObligatoriaSi.Checked)
                checkBoxObligatoriaNo.Checked = false;
        }

        private void checkBoxObligatoriaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxObligatoriaNo.Checked)
                checkBoxObligatoriaSi.Checked = false;
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
