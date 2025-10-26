using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using API.Clients;
using Domain.Model;
using DTOs;
using System.Text.RegularExpressions;

namespace WindowsForms
{
    public partial class PlanesDetalle : Form
    {
        private PlanDTO plan = null!;
        private FormMode mode;

        public PlanDTO Plan
        {
            get => plan;
            set
            {
                plan = value;
                SetPlans();
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
        }

        private async void Init(FormMode mode, PlanDTO plan)
        {
            await Specialty_Load();
            this.Mode = mode;
            this.Plan = plan;
        }

        public PlanesDetalle(FormMode mode, PlanDTO plan) : this()
        {
            Init(mode, plan);
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (Mode == FormMode.Add)
            {
                this.Text = "Agregar Plan";
            }

            if (Mode == FormMode.Update)
            {
                this.Text = "Editar Plan";
            }
        }

        private void SetPlans()
        {
            if (plan == null)
            {
                descripcionTextBox.Text = string.Empty;
                añoCalendarioTextBox.Text = string.Empty;
            }
            else
            {
                descripcionTextBox.Text = plan.Descripcion ?? string.Empty;
                añoCalendarioTextBox.Text = plan.Año_calendario > 0 ? plan.Año_calendario.ToString() : string.Empty;
            }
        }

        private async Task Specialty_Load()
        {
            try
            {
                var specialty = await SpecialtiesApiClient.GetAllAsync();
                var specialtyPlans = specialty.Select(s => new
                {
                    Id = s.Id,
                    DescEspecialidad = s.DescEspecialidad
                }).ToList();

                cmbEspecialidades.DataSource = specialty;
                cmbEspecialidades.DisplayMember = "DescEspecialidad";
                cmbEspecialidades.ValueMember = "Id";
                cmbEspecialidades.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar especialidades: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DescripcionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
                errorProvider1.SetError(descripcionTextBox, "Solo se permiten letras.");
            }
            else
            {
                errorProvider1.SetError(descripcionTextBox, "");
            }
        }

        private void AñoCalendarioTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(añoCalendarioTextBox, "Solo se permiten números.");
            }
            else
            {
                errorProvider1.SetError(añoCalendarioTextBox, "");
            }
        }

        private void DescripcionTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                errorProvider1.SetError(descripcionTextBox, "La descripción es obligatoria.");
            }
            else if (descripcionTextBox.Text.Length < 3)
            {
                errorProvider1.SetError(descripcionTextBox, "Debe tener al menos 3 caracteres.");
            }
            else
            {
                errorProvider1.SetError(descripcionTextBox, "");
            }
        }

        private void AñoCalendarioTextBox_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(añoCalendarioTextBox.Text, out int año) ||
                año < 2000 || año > DateTime.Now.Year + 1)
            {
                errorProvider1.SetError(añoCalendarioTextBox, "Ingrese un año válido (>=2000 y <= año actual +1).");
            }
            else
            {
                errorProvider1.SetError(añoCalendarioTextBox, "");
            }
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
            else if (!Regex.IsMatch(descripcionTextBox.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                errorProvider1.SetError(descripcionTextBox, "Solo se permiten letras.");
                isValid = false;
            }

            if (!int.TryParse(añoCalendarioTextBox.Text, out int año) ||
                año < 2000 || año > DateTime.Now.Year + 1)
            {
                errorProvider1.SetError(añoCalendarioTextBox, "Ingrese un año calendario válido.");
                isValid = false;
            }
            if (cmbEspecialidades.SelectedIndex < 0 || cmbEspecialidades.SelectedValue == null)
            {
                errorProvider1.SetError(cmbEspecialidades, "Debe seleccionar una especialidad.");
                isValid = false;
            }
            return isValid;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidatePlan())
                return;

            try
            {
                plan.SpecialtyId = (int)cmbEspecialidades.SelectedValue;
                plan.Descripcion = descripcionTextBox.Text.Trim();
                plan.Año_calendario = int.Parse(añoCalendarioTextBox.Text);

                if (Mode == FormMode.Add)
                {
                    await PlansApiClient.AddAsync(plan);
                }
                else
                {
                    await PlansApiClient.UpdateAsync(plan);
                }

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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
