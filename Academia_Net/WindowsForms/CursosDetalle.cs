using API.Clients;
using DTOs;
using System;
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
        private CourseDTO course;
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
                SetFormMode(value);
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
            Init(mode, course);
        }

        private void Init(FormMode mode, CourseDTO course)
        {
            this.Mode = mode;
            this.Course = course;
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (mode == FormMode.Add)
            {
                idLabel.Visible = false;
                //fechaPedidoTextBox.Visible = false;
            }
            else if (mode == FormMode.Update)
            {
                idLabel.Visible = true;
                //fechaPedidoTextBox.Visible = true;
            }
        }

        private void SetFormFields()
        {
            if (course == null)
            {
                cupoCursoTextBox.Text = string.Empty;
                año_calendarioCursoTextBox.Text = string.Empty;
                turnoCursoTextBox.Text = string.Empty;
                comisionTextBox.Text = string.Empty;
            }
            else
            {
                cupoCursoTextBox.Text = course.Cupo > 0 ? course.Cupo.ToString() : string.Empty;
                año_calendarioCursoTextBox.Text = course.Año_calendario > 0 ? course.Año_calendario.ToString() : string.Empty;
                turnoCursoTextBox.Text = course.Turno ?? string.Empty;
                comisionTextBox.Text = course.Comision > 0 ? course.Comision.ToString() : string.Empty;
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateCurso())
                return;

            try
            {
                course.Cupo = int.Parse(cupoCursoTextBox.Text);
                course.Año_calendario = int.Parse(año_calendarioCursoTextBox.Text);
                course.Turno = turnoCursoTextBox.Text;
                course.Comision = int.Parse(comisionTextBox.Text);

                if (Mode == FormMode.Add)
                {
                    await CoursesApiClient.AddAsync(course);
                }
                else
                {
                    await CoursesApiClient.UpdateAsync(course);
                }

                MessageBox.Show("Curso guardado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            this.Close();
        }

        private bool ValidateCurso()
        {
            bool isValid = true;
            errorProvider1.Clear();

            if (!int.TryParse(cupoCursoTextBox.Text, out int cupo) || cupo <= 0)
            {
                errorProvider1.SetError(cupoCursoTextBox, "El cupo debe ser un número mayor a 0.");
                isValid = false;
            }

            if (!int.TryParse(año_calendarioCursoTextBox.Text, out int año) ||
                año < 2000 || año > DateTime.Now.Year + 1)
            {
                errorProvider1.SetError(año_calendarioCursoTextBox, "Ingrese un año calendario válido.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(turnoCursoTextBox.Text))
            {
                errorProvider1.SetError(turnoCursoTextBox, "El turno es obligatorio.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(comisionTextBox.Text))
            {
                errorProvider1.SetError(comisionTextBox, "La comisión es obligatoria.");
                isValid = false;
            }

            return isValid;
        }
    }
}
