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
            ConfigurarColumnas();
        }

        private void CursosList_Load(object sender, EventArgs e)
        {
            this.GetAllCourses();
        }

        private void ConfigurarColumnas()
        {
            this.coursesDataGridView.AutoGenerateColumns = false;

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 80
            });

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cupo",
                HeaderText = "Cupo",
                DataPropertyName = "Cupo",
                Width = 250
            });

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AñoCalendario",
                HeaderText = "Año Calendario",
                DataPropertyName = "Año_calendario",
                Width = 120
            });

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Turno",
                HeaderText = "Turno",
                DataPropertyName = "Turno",
                Width = 120
            });

            this.coursesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Comision",
                HeaderText = "Comisión",
                DataPropertyName = "Comision",
                Width = 120
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
            try
            {
                CourseDTO course = this.SelectedCourse();

                var result = MessageBox.Show(
                    $"¿Está seguro que desea eliminar el curso {course.Id}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    await CoursesApiClient.DeleteAsync(course.Id);
                    this.GetAllCourses();
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
                this.coursesDataGridView.DataSource = courses;

                if (coursesDataGridView.Rows.Count > 0)
                {
                    coursesDataGridView.Rows[0].Selected = true;
                    eliminarButton.Enabled = true;
                    modificarButton.Enabled = true;
                }
                else
                {
                    eliminarButton.Enabled = false;
                    modificarButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar lista de cursos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                eliminarButton.Enabled = false;
                modificarButton.Enabled = false;
            }
        }

        private async void modificarButton_Click(object sender, EventArgs e)
        {
            try
            {
                int id = this.SelectedItem().Id;
                CourseDTO curso = await CoursesApiClient.GetAsync(id);

                CursosDetalle cursosDetalles = new CursosDetalle(FormMode.Update, curso);
                cursosDetalles.ShowDialog();

                this.GetAllCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar curso para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private CourseDTO SelectedItem()
        {
            return (CourseDTO)coursesDataGridView.SelectedRows[0].DataBoundItem;
        }
    }
}
