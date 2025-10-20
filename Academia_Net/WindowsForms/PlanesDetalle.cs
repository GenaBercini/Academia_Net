using API.Clients;
using DTOs;
using System;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class PlanesDetalle : Form
    {
        private PlanDTO plan;
        private FormMode mode;

        public PlanDTO Plan
        {
            get => plan;
            set
            {
                plan = value;
                SetFormFields();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set
            {
                SetFormMode(value);
            }
        }

        public PlanesDetalle()
        {
            InitializeComponent();
            Mode = FormMode.Add;
            Plan = new PlanDTO();
        }

        public PlanesDetalle(FormMode mode, PlanDTO plan) : this()
        {
            Init(mode, plan);
        }

        private void Init(FormMode mode, PlanDTO plan)
        {
            this.Mode = mode;
            this.Plan = plan;
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
            }
            else if (mode == FormMode.Update)
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
            }
        }

        private void SetFormFields()
        {
            if (plan == null)
            {
                descripcionTextBox.Text = string.Empty;
                añoCalendarioTextBox.Text = string.Empty;
            }
            else
            {
                idTextBox.Text = plan.Id > 0 ? plan.Id.ToString() : string.Empty;
                descripcionTextBox.Text = plan.Descripcion ?? string.Empty;
                añoCalendarioTextBox.Text = plan.Año_calendario > 0 ? plan.Año_calendario.ToString() : string.Empty;
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidatePlan())
                return;

            try
            {
                plan.Descripcion = descripcionTextBox.Text;
                plan.Año_calendario = int.Parse(añoCalendarioTextBox.Text);

                if (Mode == FormMode.Add)
                {
                    await PlansApiClient.AddAsync(plan);
                }
                else
                {
                    await PlansApiClient.UpdateAsync(plan);
                }

                MessageBox.Show("Plan guardado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar plan: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidatePlan()
        {
            bool isValid = true;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                errorProvider1.SetError(descripcionTextBox, "La descripción es obligatoria.");
                isValid = false;
            }

            if (!int.TryParse(añoCalendarioTextBox.Text, out int año) ||
                año < 2000 || año > DateTime.Now.Year + 1)
            {
                errorProvider1.SetError(añoCalendarioTextBox, "Ingrese un año calendario válido.");
                isValid = false;
            }

            return isValid;
        }
    }
}
