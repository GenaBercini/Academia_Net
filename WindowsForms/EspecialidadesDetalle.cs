using System;
using System.Windows.Forms;
using API.Clients;
using DTOs;
using static WindowsForms.CursosDetalle;

namespace WindowsForms
{
    public partial class EspecialidadesDetalle : Form
    {
        private SpecialtyDTO specialty;
        private FormMode mode;

        public EspecialidadesDetalle()
        {
            InitializeComponent();
        }

        public EspecialidadesDetalle(FormMode mode, SpecialtyDTO specialty) : this()
        {
            this.Mode = mode;
            this.Specialty = specialty;
        }

        public SpecialtyDTO Specialty
        {
            get => specialty;
            set
            {
                specialty = value;
                LoadFormData();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set
            {
                mode = value;
                ApplyFormMode();
            }
        }

        private void ApplyFormMode()
        {

            if (Mode == FormMode.Add)
            {
                this.Text = "Agregar Especialidad";
            }
            else if (Mode == FormMode.Update)
            {
                this.Text = "Editar Especialidad";
            }
        }

        private void LoadFormData()
        {
            if (Specialty != null)
            {
                descripcionTextBox.Text = Specialty.DescEspecialidad;
                duracionTextBox.Text = Specialty.DuracionAnios.ToString();
            }
        }

        private bool ValidateSpecialty()
        {
            bool isValid = true;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                errorProvider1.SetError(descripcionTextBox, "La descripción es obligatoria.");
                isValid = false;
            }

            if (!int.TryParse(duracionTextBox.Text, out int duracion) || duracion <= 0)
            {
                errorProvider1.SetError(duracionTextBox, "Ingrese una duración válida en años.");
                isValid = false;
            }

            return isValid;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (ValidateSpecialty())
            {
                Specialty.DescEspecialidad = descripcionTextBox.Text;
                Specialty.DuracionAnios = int.Parse(duracionTextBox.Text);

                if (Mode == FormMode.Update)
                {
                    await SpecialtiesApiClient.UpdateAsync(Specialty);
                }
                else
                {
                    await SpecialtiesApiClient.AddAsync(Specialty);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
