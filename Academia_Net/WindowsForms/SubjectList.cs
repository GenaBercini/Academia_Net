using System;
using System.Collections.Generic;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class SubjectList : Form
    {
        public SubjectList()
        {
            InitializeComponent();
            this.GetAllMaterias();
            ConfigurarColumnas();
        }

        private void SubjectList_Load(object sender, EventArgs e)
        {
            this.GetAllMaterias();
        }

        private void ConfigurarColumnas()
        {
            this.subjectsDataGridView.AutoGenerateColumns = false;

            this.subjectsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 80
            });

            this.subjectsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Desc",
                HeaderText = "Descripción",
                DataPropertyName = "Desc",
                Width = 250
            });

            this.subjectsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HsSemanales",
                HeaderText = "Horas Semanales",
                DataPropertyName = "HsSemanales",
                Width = 150
            });

            this.subjectsDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Obligatoria",
                HeaderText = "Obligatoria",
                DataPropertyName = "Obligatoria",
                Width = 100
            });
        }

        private async void GetAllMaterias()
        {
            var materias = await SubjectsApiClient.GetAllAsync();
            subjectsDataGridView.DataSource = materias;

            if (subjectsDataGridView.Rows.Count > 0)
            {
                eliminarButton.Enabled = true;
                modificarButton.Enabled = true;
            }
            else
            {
                eliminarButton.Enabled = false;
                modificarButton.Enabled = false;
            }
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {
            SubjectDTO subjectNuevo = new SubjectDTO();
            SubjectDetail materiaDetalle = new SubjectDetail(FormMode.Add, subjectNuevo);
            materiaDetalle.ShowDialog();
            this.GetAllMaterias();
        }

        private async void modificarButton_Click(object sender, EventArgs e)
        {
            int id = this.SelectedItem().Id;
            SubjectDTO subject = await SubjectsApiClient.GetAsync(id);
            SubjectDetail materiaDetalle = new SubjectDetail(FormMode.Update, subject);
            materiaDetalle.ShowDialog();
            this.GetAllMaterias();
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            SubjectDTO subject = this.SelectedItem();
            var result = MessageBox.Show($"¿Está seguro que desea eliminar la materia {subject.Desc}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                await SubjectsApiClient.DeleteAsync(subject.Id);
                this.GetAllMaterias();
            }
        }

        private SubjectDTO SelectedItem()
        {
            return (SubjectDTO)subjectsDataGridView.SelectedRows[0].DataBoundItem;
        }
    }
}
