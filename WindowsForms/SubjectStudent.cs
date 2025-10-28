
using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;
using Shared.Types;

namespace WindowsForms
{
    public partial class SubjectStudent : Form
    {
        public SubjectStudent()
        {
            InitializeComponent();
            this.Load += SubjectStudent_Load;

            // DataGridView configuración mínima
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
        }

        private async void SubjectStudent_Load(object sender, EventArgs e)
        {
            try
            {
                var auth = AuthServiceProvider.Instance;
                var current = await auth.GetCurrentUserAsync();
                if (current == null)
                {
                    MessageBox.Show("No se pudo determinar el usuario actual.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var courses = (await CoursesApiClient.GetAllAsync())?.ToList() ?? new List<CourseDTO>();
                var subjectsCache = (await SubjectsApiClient.GetAllAsync())?.ToDictionary(s => s.Id) ?? new Dictionary<int, SubjectDTO>();

                var enrolledDisplay = new List<dynamic>();

                foreach (var c in courses)
                {
                    try
                    {
                        var insc = (await UserCourseSubjectsApiClient.GetByUserAndCourseAsync(current.Id, c.Id))?.ToList() ?? new List<UserCourseSubjectDTO>();
                        foreach (var eItem in insc)
                        {
                            var subjDesc = subjectsCache.TryGetValue(eItem.SubjectId, out var sDto) ? sDto.Desc : $"Id {eItem.SubjectId}";
                            enrolledDisplay.Add(new
                            {
                                CourseId = eItem.CourseId,
                                Curso = c.Comision,
                                SubjectId = eItem.SubjectId,
                                Materia = subjDesc,
                                Fecha = eItem.FechaInscripcion,
                                Nota = eItem.NotaFinal
                            });
                        }
                    }
                    catch
                    {
                        // ignorar fallo en un curso y continuar
                    }
                }

                dataGridView1.DataSource = enrolledDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando materias inscriptas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }
    }
}
