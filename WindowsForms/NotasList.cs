using API.Clients;
using DTOs;
using Shared.Types;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;
using Shared.Types;

namespace WindowsForms
{
    public partial class NotasList : Form
    {
        private int _profesorId;
        private int _courseId;
        private List<UserCourseSubjectDTO> _materiasProfesor = new();
        private List<UserCourseSubjectDTO> _alumnosMateria = new();

        public NotasList(int profesorId = 0, int courseId = 0)
        {
            InitializeComponent();
            _profesorId = profesorId;
            _courseId = courseId;

            subjectDataGridView.SelectionChanged += subjectDataGridView_SelectionChanged;
        }

        private async void NotasList_Load(object sender, EventArgs e)
        {
            notasComboBox.DataSource = Enumerable.Range(1, 10).ToList();

            try
            {
              
                if (_profesorId == 0)
                {
                    try
                    {
                        var auth = AuthServiceProvider.Instance;
                        var current = await auth.GetCurrentUserAsync();
                        if (current != null)
                            _profesorId = current.Id;
                    }
                    catch
                    {
                       
                    }
                }

                if (_profesorId == 0)
                {
                    var profesores = await UsersApiClient.GetAllAsync();
                    _profesorId = profesores.FirstOrDefault(u => u.TypeUser == UserType.Teacher)?.Id ?? 0;
                }

                if (_courseId == 0)
                {
                    var cursos = await CoursesApiClient.GetAllAsync();
                    foreach (var c in cursos)
                    {
                        try
                        {
                            var insc = await UserCourseSubjectsApiClient.GetByUserAndCourseAsync(_profesorId, c.Id);
                            if (insc != null && insc.Any())
                            {
                                _courseId = c.Id;
                                break;
                            }
                        }
                        catch
                        {
                            
                        }
                    }

                    if (_courseId == 0)
                        _courseId = (await CoursesApiClient.GetAllAsync()).FirstOrDefault()?.Id ?? 0;
                }

                if (_profesorId > 0 && _courseId > 0)
                {
                    await CargarMateriasProfesorAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo determinar el profesor o curso.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inicializando la vista de notas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            if (subjectDataGridView.Rows.Count > 0)
            {
                subjectDataGridView.ClearSelection();
                subjectDataGridView.Rows[0].Selected = true;
            }
        }

        private async Task<IEnumerable<UserCourseSubjectDTO>> GetEnrollmentsForCourseAndSubjectAsync(int courseId, int subjectId)
        {
            var clientType = typeof(UserCourseSubjectsApiClient);

            try
            {
                var m = clientType.GetMethod("GetByCourseAndSubjectAsync", BindingFlags.Public | BindingFlags.Static);
                if (m != null)
                {
                    var taskObj = m.Invoke(null, new object[] { courseId, subjectId });
                    if (taskObj is Task t)
                    {
                        await t.ConfigureAwait(false);
                        var resultProp = taskObj.GetType().GetProperty("Result");
                        var res = resultProp?.GetValue(taskObj) as IEnumerable<UserCourseSubjectDTO>;
                        if (res != null) return res;
                    }
                }
            }
            catch
            {
           
            }

            try
            {
                var m = clientType.GetMethod("GetByCourseAsync", BindingFlags.Public | BindingFlags.Static);
                if (m != null)
                {
                    var taskObj = m.Invoke(null, new object[] { courseId });
                    if (taskObj is Task t)
                    {
                        await t.ConfigureAwait(false);
                        var resultProp = taskObj.GetType().GetProperty("Result");
                        var res = resultProp?.GetValue(taskObj) as IEnumerable<UserCourseSubjectDTO>;
                        if (res != null) return res.Where(x => x.SubjectId == subjectId);
                    }
                }
            }
            catch
            {
               
            }

            try
            {
                var m = clientType.GetMethod("GetAllAsync", BindingFlags.Public | BindingFlags.Static);
                if (m != null)
                {
                    var taskObj = m.Invoke(null, null);
                    if (taskObj is Task t)
                    {
                        await t.ConfigureAwait(false);
                        var resultProp = taskObj.GetType().GetProperty("Result");
                        var res = resultProp?.GetValue(taskObj) as IEnumerable<UserCourseSubjectDTO>;
                        if (res != null) return res.Where(x => x.CourseId == courseId && x.SubjectId == subjectId);
                    }
                }
            }
            catch
            {
               
            }

           
            var final = new List<UserCourseSubjectDTO>();
            try
            {
                var users = await UsersApiClient.GetAllAsync();
                var methodByUser = clientType.GetMethod("GetByUserAndCourseAsync", BindingFlags.Public | BindingFlags.Static);
                if (methodByUser != null)
                {
                    foreach (var u in users.Where(u => u.TypeUser != UserType.Admin))
                    {
                        try
                        {
                            var taskObj = methodByUser.Invoke(null, new object[] { u.Id, courseId });
                            if (taskObj is Task t)
                            {
                                await t.ConfigureAwait(false);
                                var resultProp = taskObj.GetType().GetProperty("Result");
                                var res = resultProp?.GetValue(taskObj) as IEnumerable<UserCourseSubjectDTO>;
                                if (res != null)
                                {
                                    final.AddRange(res.Where(r => r.SubjectId == subjectId));
                                }
                            }
                        }
                        catch
                        {
                           
                        }
                    }
                }
            }
            catch
            {
               
            }

            return final;
        }

        private async void subjectDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (subjectDataGridView.SelectedRows.Count == 0)
            {
                studentDataGridView.DataSource = null;
                return;
            }

            try
            {
                var row = subjectDataGridView.SelectedRows[0];
                int courseId = (int)row.Cells["CourseId"].Value;
                int subjectId = (int)row.Cells["SubjectId"].Value;

        
                var inscripciones = (await GetEnrollmentsForCourseAndSubjectAsync(courseId, subjectId))?.ToList() ?? new List<UserCourseSubjectDTO>();

             
                _alumnosMateria = inscripciones.Where(i => i.UserId != _profesorId).ToList();

                var alumnosDisplay = new List<dynamic>();
                foreach (var a in _alumnosMateria)
                {
                    try
                    {
                        var alumno = await UsersApiClient.GetAsync(a.UserId);
                        alumnosDisplay.Add(new
                        {
                            a.UserId,
                            Alumno = alumno?.UserName ?? $"Id {a.UserId}",
                            Nota = a.NotaFinal
                        });
                    }
                    catch
                    {
                        alumnosDisplay.Add(new
                        {
                            a.UserId,
                            Alumno = $"Id {a.UserId}",
                            Nota = a.NotaFinal
                        });
                    }
                }

                studentDataGridView.DataSource = alumnosDisplay;
                studentDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                studentDataGridView.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando alumnos de la materia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            try
            {
                inscripcion.NotaFinal = nota;
                await UserCourseSubjectsApiClient.UpdateNotaAsync(inscripcion);

                MessageBox.Show("Nota cargada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                subjectDataGridView_SelectionChanged(subjectDataGridView, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la nota: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
