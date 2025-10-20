using System;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class PlanesList : Form
    {
        public PlanesList()
        {
            InitializeComponent();
            this.GetAllPlans();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            plansDataGridView.AutoGenerateColumns = false;
            plansDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            plansDataGridView.MultiSelect = false;

            plansDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 120
            });

            plansDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Año_calendario",
                HeaderText = "Año Calendario",
                DataPropertyName = "Año_calendario",
                Width = 371
            });

            plansDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Width = 371
            });
        }

        private async void GetAllPlans()
        {
            try
            {
                var plans = await PlansApiClient.GetAllAsync();
                plansDataGridView.DataSource = plans;

                modificarButton.Enabled = eliminarButton.Enabled = plans.Count() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los planes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {
            var planNuevo = new PlanDTO();
            using (var planDetalle = new PlanesDetalle(FormMode.Add, planNuevo))
            {
                if (planDetalle.ShowDialog() == DialogResult.OK)
                    GetAllPlans();
            }
        }

        private async void modificarButton_Click(object sender, EventArgs e)
        {
            var seleccionado = SelectedPlan();
            if (seleccionado == null) return;

            var plan = await PlansApiClient.GetAsync(seleccionado.Id);
            using (var planDetalle = new PlanesDetalle(FormMode.Update, plan))
            {
                if (planDetalle.ShowDialog() == DialogResult.OK)
                    GetAllPlans();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            var seleccionado = SelectedPlan();
            if (seleccionado == null) return;

            var result = MessageBox.Show(
                $"¿Está seguro que desea eliminar el plan del año {seleccionado.Año_calendario}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await PlansApiClient.DeleteAsync(seleccionado.Id);
                    GetAllPlans();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el plan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private PlanDTO? SelectedPlan()
        {
            if (plansDataGridView.SelectedRows.Count == 0)
                return null;

            return (PlanDTO)plansDataGridView.SelectedRows[0].DataBoundItem!;
        }

    }
}
