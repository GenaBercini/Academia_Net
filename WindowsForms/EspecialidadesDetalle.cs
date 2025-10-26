using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using API.Clients;
using Domain.Model;
using DTOs;
using static WindowsForms.CursosDetalle;

namespace WindowsForms
{
    public partial class EspecialidadesDetalle : Form
    {
        private SpecialtyDTO specialty = null!;
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
                SetSpecialty();
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

        private void SetFormMode(FormMode value)
        {
            mode = value;


            if (Mode == FormMode.Add)
            {
                this.Text = "Agregar Especialidad";
            }
            else if (Mode == FormMode.Update)
            {
                this.Text = "Editar Especialidad";
            }
        }

        private void SetSpecialty()
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

            var descripcion = descripcionTextBox.Text.Trim();
            var duracionTexto = duracionTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                errorProvider1.SetError(descripcionTextBox, "La descripción es obligatoria.");
                isValid = false;
            }
            else if (descripcion.Length < 3)
            {
                errorProvider1.SetError(descripcionTextBox, "La descripción debe tener al menos 3 caracteres.");
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(descripcion, @"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                errorProvider1.SetError(descripcionTextBox, "La descripción solo puede contener letras, números y espacios.");
                isValid = false;
            }

            if (!int.TryParse(duracionTexto, out int duracion) || duracion <= 0)
            {
                errorProvider1.SetError(duracionTextBox, "Ingrese una duración válida en años.");
                isValid = false;
            }
            else if (duracion > 100)
            {
                errorProvider1.SetError(duracionTextBox, "La duración no puede ser mayor a 100 años.");
                isValid = false;
            }

            return isValid;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateSpecialty())
                return;

            Specialty.DescEspecialidad = descripcionTextBox.Text.Trim();
            Specialty.DuracionAnios = int.Parse(duracionTextBox.Text.Trim());

            try
            {
                if (Mode == FormMode.Update)
                    await SpecialtiesApiClient.UpdateAsync(Specialty);
                else
                    await SpecialtiesApiClient.AddAsync(Specialty);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Validación de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void duracionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
