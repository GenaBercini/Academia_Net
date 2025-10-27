using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class NotasList : Form
    {
        private int _profesorId;
        private int _courseId;
        private List<UserCourseSubjectDTO> _materiasProfesor = new();
        private List<UserCourseSubjectDTO> _alumnosMateria = new();

        public NotasList()
        {
            InitializeComponent();
        }

        private async void NotasList_Load(object sender, EventArgs e)
        {
            notasComboBox.DataSource = Enumerable.Range(1, 10).ToList();

            if (_profesorId == 0 || _courseId == 0)
            {
                await CargarCursosYProfesoresAsync();
            }
            else
            {
                await CargarMateriasProfesorAsync();
            }
        }

        private async Task CargarCursosYProfesoresAsync()
        {
            var profesores = await UsersApiClient.GetAllAsync();
            var cursos = await CoursesApiClient.GetAllAsync();

            _profesorId = profesores.FirstOrDefault(u => u.TypeUser == Domain.Model.UserType.Teacher)?.Id ?? 0;
            _courseId = cursos.FirstOrDefault()?.Id ?? 0;

            if (_profesorId > 0 && _courseId > 0)
                await CargarMateriasProfesorAsync();
            else
                MessageBox.Show("No se pudo determinar el profesor o curso.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private async Task CargarMateriasProfesorAsync()
        {
            var inscripciones = await UserCourseSubjectsApiClient.GetByUserAndCourseAsync(_profesorId, _courseId);
            _materiasProfesor = inscripciones.ToList();

            var materiasDisplay = new List<dynamic>();

            foreach (var m in _materiasProfesor)
            {
                try
                {
                    var materia = await SubjectsApiClient.GetAsync(m.SubjectId);

                    if (materia == null)
                    {
                        MessageBox.Show(
                            "Este profesor no tiene asignada ninguna materia.",
                            "Información",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        return;
                    }

                    materiasDisplay.Add(new
                    {
                        m.CourseId,
                        m.SubjectId,
                        Materia = materia.Desc
                    });
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Este profesor no tiene asignada ninguna materia.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }
            }

            subjectDataGridView.DataSource = materiasDisplay;
            subjectDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            subjectDataGridView.MultiSelect = false;
        }

        private async void subjectDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (subjectDataGridView.SelectedRows.Count == 0)
                return;

            var row = subjectDataGridView.SelectedRows[0];
            int courseId = (int)row.Cells["CourseId"].Value;
            int subjectId = (int)row.Cells["SubjectId"].Value;

            var inscripciones = await UserCourseSubjectsApiClient.GetByUserAndCourseAsync(_profesorId, courseId);
            _alumnosMateria = inscripciones
                .Where(i => i.SubjectId == subjectId && i.UserId != _profesorId)
                .ToList();

            var alumnosDisplay = new List<dynamic>();
            foreach (var a in _alumnosMateria)
            {
                var alumno = await UsersApiClient.GetAsync(a.UserId);
                alumnosDisplay.Add(new
                {
                    a.UserId,
                    Alumno = alumno.UserName,
                    Nota = a.NotaFinal
                });
            }

            studentDataGridView.DataSource = alumnosDisplay;
            studentDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            studentDataGridView.MultiSelect = false;
        }

        private async void cargarNotaButton_Click(object sender, EventArgs e)
        {
            if (studentDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un alumno.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = studentDataGridView.SelectedRows[0];
            int userId = (int)row.Cells["UserId"].Value;
            int nota = (int)notasComboBox.SelectedItem;

            var inscripcion = _alumnosMateria.FirstOrDefault(a => a.UserId == userId);
            if (inscripcion == null)
                return;

            inscripcion.NotaFinal = nota;
            await UserCourseSubjectsApiClient.UpdateNotaAsync(inscripcion);

            MessageBox.Show("Nota cargada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            subjectDataGridView_SelectionChanged(subjectDataGridView, EventArgs.Empty);
        }
    }
}
