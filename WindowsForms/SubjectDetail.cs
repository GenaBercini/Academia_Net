using System;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class SubjectDetail : Form
    {
        private SubjectDTO subject;
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

        private bool ValidateSubject()
        {
            bool isValid = true;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(descripcionSubjectTextBox.Text))
            {
                errorProvider1.SetError(descripcionSubjectTextBox, "La descripción es obligatoria.");
                isValid = false;
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
                        Desc = descripcionSubjectTextBox.Text,
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
                        Desc = descripcionSubjectTextBox.Text,
                        HsSemanales = int.Parse(horasSemanalesTextBox.Text),
                        Obligatoria = checkBoxObligatoriaSi.Checked,
                        Habilitado = Subject.Habilitado
                    };

                    await SubjectsApiClient.UpdateAsync(updateSubject);
                }

                MessageBox.Show("Materia guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
