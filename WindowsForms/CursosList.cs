using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;
using static WindowsForms.CursosDetalle;


namespace WindowsForms
{
    public partial class CursosList : Form
    {
        public CursosList()
        {
            InitializeComponent();
            this.GetAllCourses();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            coursesDataGridView.AutoGenerateColumns = false;
            coursesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            coursesDataGridView.MultiSelect = false;

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 120
            });

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cupo",
                HeaderText = "Cupo",
                DataPropertyName = "Cupo",
                Width = 185
            });

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AñoCalendario",
                HeaderText = "Año Calendario",
                DataPropertyName = "Año_calendario",
                Width = 185
            });

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Turno",
                HeaderText = "Turno",
                DataPropertyName = "Turno",
                Width = 186
            });

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Comision",
                HeaderText = "Comisión",
                DataPropertyName = "Comision",
                Width = 186
            });

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SpecialtyId",
                HeaderText = "ID especialidad",
                DataPropertyName = "SpecialtyId",
                Width = 100
            });

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SpecialtyDescripcion",
                HeaderText = "Descripción de la Especialidad",
                DataPropertyName = "SpecialtyDescripcion",
                Width = 200
            });
        }

        private void Cursos(object sender, EventArgs e)
        {
            CourseDTO cursoNuevo = new CourseDTO();
            CursosDetalle cursosDetalles = new CursosDetalle(FormMode.Add, cursoNuevo);
            cursosDetalles.ShowDialog();
            this.GetAllCourses();
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (coursesDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un curso para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var course = SelectedCourse();

                var result = MessageBox.Show(
                    $"¿Está seguro que desea eliminar el curso {course.Id}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    await CoursesApiClient.DeleteAsync(course.Id);
                    GetAllCourses();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar curso: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private CourseDTO SelectedCourse()
        {
            return (CourseDTO)coursesDataGridView.SelectedRows[0].DataBoundItem;
        }

        private async void GetAllCourses()
        {
            try
            {
                this.coursesDataGridView.DataSource = null;
                var courses = await CoursesApiClient.GetAllAsync();

                if (courses == null || !courses.Any())
                {
                    eliminarButton.Enabled = false;
                    modificarButton.Enabled = false;
                    return;
                }

                this.coursesDataGridView.DataSource = courses;
                coursesDataGridView.Rows[0].Selected = true;
                eliminarButton.Enabled = true;
                modificarButton.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de cursos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                eliminarButton.Enabled = false;
                modificarButton.Enabled = false;
            }
        }

        private async void modificarButton_Click(object sender, EventArgs e)
        {
            if (coursesDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un curso para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = SelectedCourse().Id;
                var curso = await CoursesApiClient.GetAsync(id);
                var detalle = new CursosDetalle(FormMode.Update, curso);
                detalle.ShowDialog();
                GetAllCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar curso para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void inscribirButton_Click(object sender, EventArgs e)
        {
            if (this.coursesDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un curso primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int courseId = (int)coursesDataGridView.SelectedRows[0].Cells["Id"].Value;

            var form = new EnrollmentUser(courseId);
            form.ShowDialog();
        }

    }
}
