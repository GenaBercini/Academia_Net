using API.Clients;
using Domain.Model;
using DTOs;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WindowsForms
{
    public partial class SubjectList : Form
    {
        public SubjectList()
        {
            InitializeComponent();
            this.GetAllSubjects();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            this.subjectsDataGridView.AutoGenerateColumns = false;

            this.subjectsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 120
            });

            this.subjectsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Desc",
                HeaderText = "Descripción",
                DataPropertyName = "Desc",
                Width = 248
            });

            this.subjectsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HsSemanales",
                HeaderText = "Horas Semanales",
                DataPropertyName = "HsSemanales",
                Width = 247
            });

            this.subjectsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Año",
                HeaderText = "Año",
                DataPropertyName = "Año",
                Width = 247
            });

            this.subjectsDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Obligatoria",
                HeaderText = "Obligatoria",
                DataPropertyName = "Obligatoria",
                Width = 247
            });

            this.subjectsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanId",
                HeaderText = "ID del Plan",
                DataPropertyName = "PlanId",
                Width = 100
            });

            this.subjectsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanDescripcion",
                HeaderText = "Descripción del Plan",
                DataPropertyName = "PlanDescripcion",
                Width = 200
            });
        }

        private async void GetAllSubjects()
        {
            try
            {
                var materias = await SubjectsApiClient.GetAllAsync();
                subjectsDataGridView.DataSource = materias;

                eliminarButton.Enabled = materias.Any();
                modificarButton.Enabled = materias.Any();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las materias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                eliminarButton.Enabled = false;
                modificarButton.Enabled = false;
            }
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {
            SubjectDTO subjectNuevo = new SubjectDTO();
            SubjectDetail materiaDetalle = new SubjectDetail(FormMode.Add, subjectNuevo);
            materiaDetalle.ShowDialog();
            this.GetAllSubjects();
        }

        private async void modificarButton_Click(object sender, EventArgs e)
        {
            if (subjectsDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una materia para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = this.SelectedItem().Id;
            SubjectDTO subject = await SubjectsApiClient.GetAsync(id);
            SubjectDetail materiaDetalle = new SubjectDetail(FormMode.Update, subject);
            materiaDetalle.ShowDialog();
            this.GetAllSubjects();
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (subjectsDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una materia para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SubjectDTO subject = this.SelectedItem();
            var result = MessageBox.Show(
                $"¿Está seguro que desea eliminar la materia {subject.Desc}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                await SubjectsApiClient.DeleteAsync(subject.Id);
                this.GetAllSubjects();
            }
        }

        private SubjectDTO SelectedItem()
        {
            return (SubjectDTO)subjectsDataGridView.SelectedRows[0].DataBoundItem;
        }
    }
}
