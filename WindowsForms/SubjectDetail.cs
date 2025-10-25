using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using API.Clients;
using Domain.Model;
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
           Init(mode, subject);
        }


        private async void Init(FormMode mode, SubjectDTO subject)
        {
            await Plan_Load();
            this.Mode = mode;
            this.Subject = subject;
        }
        private async Task Plan_Load()
        {
            try
            {
                var plan = await PlansApiClient.GetAllAsync();
                var plansSubject = plan.Select(p => new
                {
                    Id = p.Id,
                    Descripcion = p.Descripcion,
                    Año_calendario = p.Año_calendario 
                }).ToList();

                planComboBox.DataSource = plan;
                planComboBox.DisplayMember = "Descripcion";
                planComboBox.ValueMember = "Id";
                planComboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar planes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                añoSubjectTextBox.Text = Subject.Año.ToString();
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

            if (!int.TryParse(añoSubjectTextBox.Text, out int año) || año <= 0 && año>5)
            {
                errorProvider1.SetError(horasSemanalesTextBox, "Ingrese un número válido de año.");
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
                subject.Año = int.Parse(añoSubjectTextBox.Text);
                subject.PlanId = (int)planComboBox.SelectedValue;
                subject.Desc= descripcionSubjectTextBox.Text.Trim();
                subject.HsSemanales= int.Parse(horasSemanalesTextBox.Text);
                subject.Obligatoria = checkBoxObligatoriaSi.Checked;


                if (Mode == FormMode.Add)
                {
                    await SubjectsApiClient.AddAsync(subject);
                }
                else
                {
                    await SubjectsApiClient.UpdateAsync(subject);
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
