using API.Clients;
using DTOs;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsForms
{
    public enum FormMode
    {
        Add,
        Update
    }

    public partial class CursosDetalle : Form
    {
        private CourseDTO course =  null!;
        private FormMode mode;

        public CourseDTO Course
        {
            get => course;
            set
            {
                course = value;
                SetFormFields();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set
            {
                mode = value;
            }
        }

        public CursosDetalle()
        {
            InitializeComponent();
            Mode = FormMode.Add;
            Course = new CourseDTO();
        }

        public CursosDetalle(FormMode mode, CourseDTO course) : this()
        {
            this.Mode = mode;
            this.Course = course;
        }


        private void SetFormFields()
        {
            cupoCursoTextBox.Text = course.Cupo.ToString();
            año_calendarioCursoTextBox.Text = course.Año_calendario.ToString();
            turnoCursoTextBox.Text = course.Turno;
            comisionTextBox.Text = course.Comision;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateCurso())
                return;
            
            

            try
            {
                course.Cupo = int.Parse(cupoCursoTextBox.Text.Trim());
                course.Año_calendario = int.Parse(año_calendarioCursoTextBox.Text.Trim());
                course.Turno = turnoCursoTextBox.Text.Trim();
                course.Comision = comisionTextBox.Text.Trim();

                if (Mode == FormMode.Update)
                {

                    await CoursesApiClient.UpdateAsync(Course);
                }
                else
                {
                    await CoursesApiClient.AddAsync(Course);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar curso: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateCurso()
        {
            bool isValid = true;
            errorProvider1.Clear();
            if (!int.TryParse(cupoCursoTextBox.Text.Trim(), out int cupo) || cupo <= 0)
            {
                errorProvider1.SetError(cupoCursoTextBox, "El cupo debe ser un número mayor a 0.");
                isValid = false;
            }

            if (!int.TryParse(año_calendarioCursoTextBox.Text.Trim(), out int año) ||
                año < 2000 || año > DateTime.Now.Year + 1)
            {
                errorProvider1.SetError(año_calendarioCursoTextBox, "Ingrese un año calendario válido.");
                isValid = false;
            }
            if (Regex.IsMatch(turnoCursoTextBox.Text.Trim(), @"\d"))
            {
                errorProvider1.SetError(turnoCursoTextBox, "El turno no puede contener números.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(turnoCursoTextBox.Text.Trim()))
            {
                errorProvider1.SetError(turnoCursoTextBox, "El turno es obligatorio.");
                isValid = false;
            }
            else if (turnoCursoTextBox.Text.Trim().Length > 50)
            {
                errorProvider1.SetError(turnoCursoTextBox, "El turno no puede superar los 50 caracteres.");
                isValid = false;
            }

            string comision = comisionTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(comision))
            {
                errorProvider1.SetError(comisionTextBox, "La comisión es obligatoria.");
                isValid = false;
            }
            else if (comision.Length > 10)
            {
                errorProvider1.SetError(comisionTextBox, "La comisión no puede tener más de 10 caracteres.");
                isValid = false;
            }
            else if (!Regex.IsMatch(comision, @"^[a-zA-Z0-9]+$"))
            {
                errorProvider1.SetError(comisionTextBox, "La comisión solo puede contener letras y números.");
                isValid = false;
            }

            return isValid;
        }

        private void cupoCursoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void año_calendarioCursoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        private void TurnoCursoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
