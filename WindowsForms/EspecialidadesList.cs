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
            this.GetAllSpecialties();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            this.specialtiesDataGridView.AutoGenerateColumns = false;

            this.specialtiesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 120
            });

            this.specialtiesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DescEspecialidad",
                HeaderText = "Descripción",
                DataPropertyName = "DescEspecialidad",
                Width = 371
            });

            this.specialtiesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DuracionAnios",
                HeaderText = "Duración(Horas)",
                DataPropertyName = "DuracionAnios",
                Width = 371
            });
        }

        private async void GetAllSpecialties()
        {
            try
            {
                eliminarButton.Enabled = false;
                modificarButton.Enabled = false;
                this.specialtiesDataGridView.DataSource = null;

                this.specialtiesDataGridView.DataSource = await SpecialtiesApiClient.GetAllAsync();


                if (specialtiesDataGridView.Rows.Count > 0)
                {
                    specialtiesDataGridView.Rows[0].Selected = true;
                    eliminarButton.Enabled = true;
                    modificarButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar especialidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var selected = this.SelectedSpecialty();
                if (selected == null) return;

                SpecialtyDTO specialty = await SpecialtiesApiClient.GetAsync(selected.Id);
                if (specialty == null)
                {
                    MessageBox.Show("La especialidad seleccionada no existe o fue eliminada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
                var specialty = this.SelectedSpecialty();
                if (specialty == null) return;

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

        private SpecialtyDTO? SelectedSpecialty()
        {
            if (specialtiesDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una especialidad.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            return specialtiesDataGridView.SelectedRows[0].DataBoundItem as SpecialtyDTO;
        }
    }
}
