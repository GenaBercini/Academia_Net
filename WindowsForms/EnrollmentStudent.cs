
using API.Clients;
using DTOs;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;
using Shared.Types;

namespace WindowsForms
{
    public partial class EnrollmentStudent : Form
    {
        private List<SubjectDTO> _subjects = new();

        public EnrollmentStudent()
        {
            InitializeComponent();

            this.Load += EnrollmentStudent_Load;
            courseComboBox.SelectedIndexChanged += courseComboBox_SelectedIndexChanged;
            enrollmentButton.Click += enrollmentButton_Click;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
        }

        private async void EnrollmentStudent_Load(object sender, EventArgs e)
        {
            try
            {
                var cursos = (await CoursesApiClient.GetAllAsync())?.ToList() ?? new List<CourseDTO>();
                courseComboBox.DataSource = cursos;
                courseComboBox.DisplayMember = "Comision";
                courseComboBox.ValueMember = "Id";

                if (courseComboBox.Items.Count > 0)
                    courseComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando cursos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ObtenerAñoDesdeComision(string comision)
        {
            if (string.IsNullOrWhiteSpace(comision))
                throw new Exception("La comisión no puede estar vacía.");

            var match = Regex.Match(comision, @"^(\d)");
            if (!match.Success)
                throw new Exception($"Formato de comisión inválido: {comision}");

            return int.Parse(match.Groups[1].Value);
        }

        private async void courseComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (courseComboBox.SelectedItem == null)
            {
                dataGridView1.DataSource = null;
                return;
            }

            try
            {
                var course = (CourseDTO)courseComboBox.SelectedItem;
                int courseId = course.Id;

                int añoCurso = 0;
                try { añoCurso = ObtenerAñoDesdeComision(course.Comision); }
                catch { añoCurso = 0; }

                var todasMaterias = (await SubjectsApiClient.GetAllAsync())?.ToList() ?? new List<SubjectDTO>();
                _subjects = añoCurso > 0 ? todasMaterias.Where(s => s.Año == añoCurso).ToList() : todasMaterias;

                var auth = AuthServiceProvider.Instance;
                var current = await auth.GetCurrentUserAsync();
                if (current == null)
                {
                    MessageBox.Show("No se pudo determinar el usuario actual.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                IEnumerable<UserCourseSubjectDTO> inscripciones;
                try
                {
                    inscripciones = (await UserCourseSubjectsApiClient.GetByUserAndCourseAsync(current.Id, courseId))?.ToList() ?? new List<UserCourseSubjectDTO>();
                }
                catch
                {
                    inscripciones = new List<UserCourseSubjectDTO>();
                }

                var enrolledIds = inscripciones.Select(x => x.SubjectId).ToHashSet();

                var available = _subjects
                    .Where(s => !enrolledIds.Contains(s.Id))
                    .Select(s => new { s.Id, s.Desc, s.Año, s.HsSemanales })
                    .ToList();

                dataGridView1.DataSource = available;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando materias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private async void enrollmentButton_Click(object sender, EventArgs e)
        {
            if (courseComboBox.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un curso.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una materia para inscribirse.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var course = (CourseDTO)courseComboBox.SelectedItem;
                int courseId = course.Id;

                var row = dataGridView1.SelectedRows[0];
                if (row.Cells["Id"].Value == null)
                {
                    MessageBox.Show("Materia seleccionada inválida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int subjectId = Convert.ToInt32(row.Cells["Id"].Value);

                var auth = AuthServiceProvider.Instance;
                var current = await auth.GetCurrentUserAsync();
                if (current == null)
                {
                    MessageBox.Show("No se pudo determinar el usuario actual.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new UserCourseSubjectDTO
                {
                    UserId = current.Id,
                    CourseId = courseId,
                    SubjectId = subjectId,
                    FechaInscripcion = DateTime.UtcNow
                };

                await UserCourseSubjectsApiClient.AddAsync(dto);

                MessageBox.Show("Inscripción realizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                courseComboBox_SelectedIndexChanged(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inscribirse: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
