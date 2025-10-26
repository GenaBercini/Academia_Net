using API.Clients;
using Domain.Model;
using DTOs;
using System;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        private CourseDTO course = null!;
        private FormMode mode;

        public CourseDTO Course
        {
            get => course;
            set
            {
                course = value;
                SetCourse();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set => mode = value;
        }

        public CursosDetalle()
        {
            InitializeComponent();
            LoadCombos(); 
        }

        public CursosDetalle(FormMode mode, CourseDTO course) : this()
        {
            Init(mode, course);
        }

        private async void Init(FormMode mode, CourseDTO course)
        {
            this.Mode = mode;
            this.course = course;
            await Task.CompletedTask;
            await Specialty_Load();
        }
        private async Task Specialty_Load()
        {
            try
            {
                var specialty = await SpecialtiesApiClient.GetAllAsync();
                var specialtyPlans = specialty.Select(s => new
                {
                    Id = s.Id,
                    DescEspecialidad = s.DescEspecialidad
                }).ToList();

                specialtyComboBox.DataSource = specialty;
                specialtyComboBox.DisplayMember = "DescEspecialidad";
                specialtyComboBox.ValueMember = "Id";
                specialtyComboBox.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar especialidades: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCombos()
        {
            añoCursoComboBox.Items.Clear();
            for (int i = 1; i <= 5; i++)
                añoCursoComboBox.Items.Add(i.ToString());

            indiceCursoComboBox.Items.Clear();
            for (int i = 1; i <= 10; i++)
                indiceCursoComboBox.Items.Add(i.ToString("00"));

            añoCursoComboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            indiceCursoComboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
        }

        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(añoCursoComboBox.Text) &&
                !string.IsNullOrWhiteSpace(indiceCursoComboBox.Text))
            {
                string comision = añoCursoComboBox.Text.Trim() + indiceCursoComboBox.Text.Trim();
                course.Comision = comision;
            }
        }

        private void SetCourse()
        {
            cupoCursoTextBox.Text = course.Cupo.ToString();
            año_calendarioCursoTextBox.Text = course.Año_calendario.ToString();
            turnoCursoTextBox.Text = course.Turno;

            if (!string.IsNullOrEmpty(course.Comision) && course.Comision.Length >= 2)
            {
                añoCursoComboBox.Text = course.Comision[0].ToString();
                indiceCursoComboBox.Text = course.Comision.Substring(1);
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateCurso())
                return;

            try
            {
                course.SpecialtyId = (int)specialtyComboBox.SelectedValue;
                course.Cupo = int.Parse(cupoCursoTextBox.Text.Trim());
                course.Año_calendario = int.Parse(año_calendarioCursoTextBox.Text.Trim());
                course.Turno = turnoCursoTextBox.Text.Trim();

                if (!string.IsNullOrWhiteSpace(añoCursoComboBox.Text) &&
                    !string.IsNullOrWhiteSpace(indiceCursoComboBox.Text))
                {
                    course.Comision = añoCursoComboBox.Text.Trim() + indiceCursoComboBox.Text.Trim();
                }

                if (Mode == FormMode.Update)
                    await CoursesApiClient.UpdateAsync(Course);
                else
                    await CoursesApiClient.AddAsync(Course);

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

            if (string.IsNullOrWhiteSpace(añoCursoComboBox.Text))
            {
                errorProvider1.SetError(añoCursoComboBox, "Seleccione un año (1 a 5).");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(indiceCursoComboBox.Text))
            {
                errorProvider1.SetError(indiceCursoComboBox, "Seleccione un índice (01 a 10).");
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
                e.Handled = true;
        }

        // (Opcional) Método preparado para más adelante
        private async Task RefreshSubjectsByYear(int year)
        {
            // Acá podrías traer las materias del año seleccionado
            var subjects = await SubjectsApiClient.GetAllAsync();
            var filtered = subjects.Where(s => s.Año == year).ToList();
            // Ejemplo: podrías usarlas después si el curso necesita mostrarlas
        }
    }
}
