using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using Domain.Model;
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
            specialtiesDataGridView.AutoGenerateColumns = false;
            specialtiesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            specialtiesDataGridView.MultiSelect = false;

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
                specialtiesDataGridView.DataSource = null;
                var specialties = await SpecialtiesApiClient.GetAllAsync();
                if (specialties == null || !specialties.Any())
                {
                    eliminarButton.Enabled = false;
                    modificarButton.Enabled = false;
                    return;
                }
                specialtiesDataGridView.DataSource = specialties;
                specialtiesDataGridView.Rows[0].Selected = true;
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
            if (specialtiesDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una especialidad para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var specialty = SelectedSpecialty();

                var result = MessageBox.Show(
                    $"¿Está seguro que desea eliminar la especialidad {specialty.Id}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    await SpecialtiesApiClient.DeleteAsync(specialty.Id);
                    GetAllSpecialties();
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
