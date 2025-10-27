using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using Domain.Model;
using DTOs;
using Shared.Types;

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

        private bool _isLoadingUsers = false;

        private async Task CargarUsuariosAsync()
        {
            try
            {
                _isLoadingUsers = true;
                var usuarios = await UsersApiClient.GetAllAsync();
                var usuariosNoAdmin = usuarios
                    .Where(u => u.TypeUser != UserType.Admin)
                    .ToList();

                usuarioComboBox.DataSource = usuariosNoAdmin;
                usuarioComboBox.DisplayMember = "UserName";
                usuarioComboBox.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingUsers = false;
            }
        }

        private async void usuarioComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingUsers || usuarioComboBox.SelectedValue == null || usuarioComboBox.SelectedIndex < 0)
                return;

            await CargarMateriasAsync();
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

        private async Task CargarMateriasAsync()
        {
            if (usuarioComboBox.SelectedValue == null)
                return;

            try
            {
                int userId = (int)usuarioComboBox.SelectedValue;

                var curso = await CoursesApiClient.GetAsync(_courseId);
                if (curso == null)
                    throw new Exception("No se encontró el curso seleccionado.");

                int añoCurso = ObtenerAñoDesdeComision(curso.Comision);

                var todasMaterias = await SubjectsApiClient.GetAllAsync();
                _subjects = todasMaterias.Where(s => s.Año == añoCurso).ToList();

                try
                {
                    var inscripciones = await UserCourseSubjectsApiClient.GetByUserAndCourseAsync(userId, _courseId);
                    _enrollments = inscripciones?.ToList() ?? new List<UserCourseSubjectDTO>();
                }
                catch (Exception ex)
                {
                
                    if (ex.Message.Contains("no se encontraron inscripciones", StringComparison.OrdinalIgnoreCase))
                    {
                        _enrollments = new List<UserCourseSubjectDTO>();
                    }
                    else
                    {
                        throw; 
                    }
                }

                var enrolledIds = _enrollments.Select(e => e.SubjectId).ToList();
                var availableSubjects = _subjects.Where(s => !enrolledIds.Contains(s.Id)).ToList();

                subjectDataGridView.DataSource = null;
                subjectDataGridView.DataSource = availableSubjects
                    .Select(s => new { s.Id, s.Desc, s.Año, s.HsSemanales })
                    .ToList();

                inscripcionDataGridView.DataSource = null;
                inscripcionDataGridView.DataSource = _enrollments
                    .Select(e => new
                    {
                        e.SubjectId,
                        Nombre = _subjects.FirstOrDefault(s => s.Id == e.SubjectId)?.Desc
                    })
                    .ToList();

                subjectDataGridView.MultiSelect = true;
                subjectDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                inscribirButton.Enabled = availableSubjects.Any();
                eliminarButton.Enabled = _enrollments.Any();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar materias: {ex.Message}\n\nDetalle: {ex.InnerException?.Message ?? "Sin detalle."}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
                MessageBox.Show("Debe seleccionar al menos una materia.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = (int)usuarioComboBox.SelectedValue;

            try
            {
                var nuevasInscripciones = new List<UserCourseSubjectDTO>();

                foreach (DataGridViewRow row in subjectDataGridView.SelectedRows)
                {
                    int subjectId = (int)row.Cells["Id"].Value;

                    if (_enrollments.Any(e => e.SubjectId == subjectId))
                        continue;

                    nuevasInscripciones.Add(new UserCourseSubjectDTO
                    {
                        UserId = userId,
                        CourseId = _courseId,
                        SubjectId = subjectId,
                        FechaInscripcion = DateTime.Now
                    });
                }

                if (!nuevasInscripciones.Any())
                {
                    MessageBox.Show("El usuario ya estaba inscripto en las materias seleccionadas.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var ins in nuevasInscripciones)
                    await UserCourseSubjectsApiClient.AddAsync(ins);

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

            try
            {
                foreach (DataGridViewRow row in inscripcionDataGridView.SelectedRows)
                {
                    int subjectId = (int)row.Cells["SubjectId"].Value;
                    await UserCourseSubjectsApiClient.DeleteAsync(userId, _courseId, subjectId);
                }

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
