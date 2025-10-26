using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class EnrollmentUser : Form
    {
        private readonly int _courseId;
        private List<SubjectDTO> _subjects = new();
        private List<UserCourseSubjectDTO> _enrollments = new();

        public EnrollmentUser(int courseId)
        {
            InitializeComponent();
            _courseId = courseId;
        }

        private async void EnrollmentUser_Load(object sender, EventArgs e)
        {
            await CargarUsuariosAsync();
            await CargarMateriasAsync();
        }

        private async Task CargarUsuariosAsync()
        {
            try
            {
                var usuarios = await UsersApiClient.GetAllAsync();

                usuarioComboBox.DataSource = usuarios.ToList();
                usuarioComboBox.DisplayMember = "UserName";
                usuarioComboBox.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarMateriasAsync()
        {
            if (usuarioComboBox.SelectedValue == null)
                return;

            try
            {
                int userId = (int)usuarioComboBox.SelectedValue;

                _subjects = (await SubjectsApiClient.GetByCourseIdAsync(_courseId)).ToList();
                _enrollments = (await UserCourseSubjectsApiClient.GetByUserAndCourseAsync(userId, _courseId)).ToList();

                var enrolledIds = _enrollments.Select(e => e.SubjectId).ToList();
                var availableSubjects = _subjects.Where(s => !enrolledIds.Contains(s.Id)).ToList();

                // 🔹 Limpiar y actualizar DataGridView de materias disponibles
                subjectDataGridView.DataSource = null;
                subjectDataGridView.DataSource = availableSubjects
                    .Select(s => new { s.Id, s.Desc, s.Año, s.HsSemanales })
                    .ToList();

                // 🔹 Limpiar y actualizar DataGridView de materias inscriptas
                inscripcionDataGridView.DataSource = null;
                inscripcionDataGridView.DataSource = _enrollments
                    .Select(e => new
                    {
                        e.SubjectId,
                        Nombre = _subjects.FirstOrDefault(s => s.Id == e.SubjectId)?.Desc
                    })
                    .ToList();

                // 🔹 Actualizar botones según disponibilidad
                inscribirButton.Enabled = availableSubjects.Any();
                eliminarButton.Enabled = _enrollments.Any();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar materias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void usuarioComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Evita ejecutar si todavía no hay datos cargados
            if (usuarioComboBox.SelectedValue == null || usuarioComboBox.SelectedIndex < 0)
                return;

            await CargarMateriasAsync();
        }

        private async void inscribirButton_Click(object sender, EventArgs e)
        {
            if (usuarioComboBox.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (subjectDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una materia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var enrollment = new UserCourseSubjectDTO
            {
                UserId = (int)usuarioComboBox.SelectedValue,
                CourseId = _courseId,
                SubjectId = (int)subjectDataGridView.SelectedRows[0].Cells["Id"].Value,
                FechaInscripcion = DateTime.Now
            };

            // 🔹 Validación extra por si el usuario ya está inscripto
            if (_enrollments.Any(e => e.SubjectId == enrollment.SubjectId))
            {
                MessageBox.Show("El usuario ya está inscripto en esta materia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await UserCourseSubjectsApiClient.AddAsync(enrollment);
                MessageBox.Show("Inscripción realizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarMateriasAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inscribir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (usuarioComboBox.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (inscripcionDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una materia inscripta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = (int)usuarioComboBox.SelectedValue;
            int subjectId = (int)inscripcionDataGridView.SelectedRows[0].Cells["SubjectId"].Value;

            try
            {
                await UserCourseSubjectsApiClient.DeleteAsync(userId, _courseId, subjectId);
                MessageBox.Show("Inscripción eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarMateriasAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar inscripción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salirButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
