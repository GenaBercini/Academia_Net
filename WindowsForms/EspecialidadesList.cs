using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;
using static WindowsForms.CursosDetalle;

namespace WindowsForms
{
    public partial class EspecialidadesList : Form
    {
        public EspecialidadesList()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void EspecialidadesList_Load(object sender, EventArgs e)
        {
            this.GetAllSpecialties();
        }

        private void ConfigurarColumnas()
        {
            this.specialtiesDataGridView.AutoGenerateColumns = false;

            this.specialtiesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 80
            });

            this.specialtiesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DescEspecialidad",
                HeaderText = "Descripción",
                DataPropertyName = "DescEspecialidad",
                Width = 250
            });

            this.specialtiesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DuracionAnios",
                HeaderText = "Duración(Horas)",
                DataPropertyName = "DuracionAnios",
                Width = 120
            });
        }

        private async void GetAllSpecialties()
        {
            try
            {
                this.specialtiesDataGridView.DataSource = null;
                var specialties = await SpecialtiesApiClient.GetAllAsync();
                this.specialtiesDataGridView.DataSource = specialties;

                if (specialtiesDataGridView.Rows.Count > 0)
                {
                    specialtiesDataGridView.Rows[0].Selected = true;
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
                MessageBox.Show($"Error al cargar especialidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                eliminarButton.Enabled = false;
                modificarButton.Enabled = false;
            }
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {
            SpecialtyDTO specialtyNuevo = new SpecialtyDTO();
            EspecialidadesDetalle specialtyDetalle = new EspecialidadesDetalle(FormMode.Add, specialtyNuevo);

            specialtyDetalle.ShowDialog();

            this.GetAllSpecialties();
        }

        private async void modificarButton_Click(object sender, EventArgs e)
        {
            try
            {
                int id = this.SelectedSpecialty().Id;
                SpecialtyDTO specialty = await SpecialtiesApiClient.GetAsync(id);

                EspecialidadesDetalle specialtyDetalle = new EspecialidadesDetalle(FormMode.Update, specialty);
                specialtyDetalle.ShowDialog();

                this.GetAllSpecialties();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar especialidad para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            try
            {
                SpecialtyDTO specialty = this.SelectedSpecialty();

                var result = MessageBox.Show(
                    $"¿Está seguro que desea eliminar la especialidad {specialty.DescEspecialidad}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    await SpecialtiesApiClient.DeleteAsync(specialty.Id);
                    this.GetAllSpecialties();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar especialidad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private SpecialtyDTO SelectedSpecialty()
        {
            return (SpecialtyDTO)specialtiesDataGridView.SelectedRows[0].DataBoundItem;
        }
    }
}
