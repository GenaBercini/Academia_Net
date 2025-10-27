using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using API.Clients;
using Domain.Model;
using DTOs;
using Shared.Types;

namespace WindowsForms
{
    public partial class UserDetail : Form
    {
        private UserDTO user;
        private FormMode mode;
        
        public UserDetail()
        {
            InitializeComponent();
            ConfigurarCombos();
            mode = FormMode.Add;
            user = new UserDTO();
        }

        public UserDetail(FormMode mode, UserDTO user) : this()
        {
            CargarUsuario(mode, user);
        }


        private void ConfigurarCombos()
        {
            comboTypeUser.DataSource = Enum.GetValues(typeof(UserType));
            comboJobPosition.DataSource = Enum.GetValues(typeof(JobPositionType));
        }

        private void CargarUsuario(FormMode mode, UserDTO user)
        {
            this.mode = mode;
            this.user = user;

            txtPassword.Enabled = (mode == FormMode.Add);
            MostrarDatosUsuario();
        }

        private void MostrarDatosUsuario()
        {
            if (user == null)
            {
                LimpiarCampos();
                return;
            }

            txtUserName.Text = user.UserName;
            txtNombre.Text = user.Name;
            txtApellido.Text = user.LastName;
            txtEmail.Text = user.Email;
            txtDni.Text = user.Dni;
            txtStudentNumber.Text = user.StudentNumber;
            txtAdress.Text = user.Adress;

            comboTypeUser.SelectedItem = user.TypeUser;
            comboJobPosition.SelectedItem = user.JobPosition;

            ActualizarVisibilidadCampos();
        }

        private void LimpiarCampos()
        {
            txtUserName.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtDni.Clear();
            txtStudentNumber.Clear();
            txtAdress.Clear();

            comboTypeUser.SelectedItem = UserType.Student;
            ActualizarVisibilidadCampos();
        }

        private void ActualizarVisibilidadCampos()
        {
            var selectedType = (UserType)comboTypeUser.SelectedItem;

            bool esAlumno = selectedType == UserType.Student;
            bool esDocente = selectedType == UserType.Teacher;
            txtStudentNumber.Visible = lblStudentNumber.Visible = esAlumno;
            lblJobPosition.Visible = comboJobPosition.Visible = esDocente;
        }

        private void comboTypeUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarVisibilidadCampos();
        }


        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                MapearCamposAEntidad();

                if (mode == FormMode.Add)
                    await CrearUsuario();
                else
                    await ActualizarUsuario();

                MessageBox.Show("Usuario guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e) => Close();

        private void MapearCamposAEntidad()
        {
            user.UserName = txtUserName.Text;
            user.Name = txtNombre.Text;
            user.LastName = txtApellido.Text;
            user.Email = txtEmail.Text;
            user.Dni = txtDni.Text;
            user.StudentNumber = txtStudentNumber.Text;
            user.Adress = txtAdress.Text;
            user.TypeUser = (UserType)comboTypeUser.SelectedItem;

            user.JobPosition = (user.TypeUser == UserType.Teacher && comboJobPosition.Visible)
                ? (JobPositionType?)comboJobPosition.SelectedItem
                : null;
        }

        private async Task CrearUsuario()
        {
            var createUser = new UserCreateDTO
            {
                UserName = user.UserName,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Dni = user.Dni,
                Adress = user.Adress,
                TypeUser = user.TypeUser,
                StudentNumber = user.StudentNumber,
                JobPosition = user.JobPosition,
                Password = txtPassword.Text
            };

            await UsersApiClient.AddAsync(createUser);
        }

        private async Task ActualizarUsuario()
        {
            var updateUser = new UserUpdateDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Dni = user.Dni,
                Adress = user.Adress,
                TypeUser = user.TypeUser,
                JobPosition = user.JobPosition,
                StudentNumber = user.StudentNumber,
                DateOfAdmission = user.DateOfAdmission,
                DateOfHire = user.DateOfHire,
                Status = user.Status,
                Password = string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text
            };

            await UsersApiClient.UpdateAsync(updateUser);
        }
        private bool ValidarCampos()
        {
            bool valido = true;
            errorProvider.Clear();

            void MarcarError(Control c, string mensaje)
            {
                errorProvider.SetError(c, mensaje);
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
                MarcarError(txtUserName, "El nombre de usuario es requerido");

            if (mode == FormMode.Add && string.IsNullOrWhiteSpace(txtPassword.Text))
                MarcarError(txtPassword, "La contraseña es requerida");

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                MarcarError(txtNombre, "El nombre es requerido");

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
                MarcarError(txtApellido, "El apellido es requerido");

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                MarcarError(txtEmail, "El email es requerido");
            else if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                MarcarError(txtEmail, "Formato de email no válido");

            if (string.IsNullOrWhiteSpace(txtStudentNumber.Text) && txtStudentNumber.Visible)
                MarcarError(txtStudentNumber, "El legajo es requerido");

            return valido;
        }

        private void comboJobPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
