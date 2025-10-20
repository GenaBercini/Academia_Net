using API.Clients;
using Domain.Model;
using DTOs;
using System.Text.RegularExpressions;

namespace WindowsForms
{
    //public enum FormMode
    //{
    //    Add,
    //    Update
    //}

    public partial class UserDetail : Form
    {
        private UserDTO user;
        private FormMode mode;
        public UserDTO User
        {
            get => user;
            set
            {
                user = value;
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

        public UserDetail()
        {
            InitializeComponent();
            Mode = FormMode.Add;
            User = new UserDTO();
        }

        public UserDetail(FormMode mode, UserDTO user) : this()
        {
            Init(mode, user);
        }

        private async void Init(FormMode mode, UserDTO user)
        {
            this.Mode = mode;
            this.User = user;
        }
        private void SetFormMode(FormMode value)
        {
            mode = value;
            txtDni.Enabled = (mode == FormMode.Add);
        }

        private void SetFormFields()
        {
            comboTypeUser.DataSource = Enum.GetValues(typeof(UserType));
            comboJobPosition.DataSource = Enum.GetValues(typeof(JobPositionType));

            if (user == null)
            {
                comboTypeUser.SelectedItem = UserType.Student;

                txtUserName.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtApellido.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtDni.Text = string.Empty;
                txtStudentNumber.Text = string.Empty;
                txtAdress.Text = string.Empty;

                txtStudentNumber.Visible = true;
                lblJobPosition.Visible = false;
                lblStudentNumber.Visible = true;
            }
            else
            {
                txtUserName.Text = user.UserName;
                txtNombre.Text = user.Nombre;
                txtApellido.Text = user.Apellido;
                txtEmail.Text = user.Email;
                txtDni.Text = user.Dni;
                txtStudentNumber.Text = user.StudentNumber;
                txtAdress.Text = user.Adress;

                comboTypeUser.SelectedItem = user.TypeUser;

                txtStudentNumber.Visible = (user.TypeUser == UserType.Student);
                lblJobPosition.Visible = (user.TypeUser == UserType.Teacher);
                lblStudentNumber.Visible = (user.TypeUser == UserType.Student);
            }
        }

        private void comboTypeUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedType = (UserType)comboTypeUser.SelectedItem;

            txtStudentNumber.Visible = (selectedType == UserType.Student);
            lblStudentNumber.Visible = (selectedType == UserType.Student);
            lblJobPosition.Visible = (selectedType == UserType.Teacher);
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
                return;

            try
            {
                user.UserName = txtUserName.Text;
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                user.Email = txtEmail.Text;
                user.Dni = txtDni.Text;
                user.StudentNumber = txtStudentNumber.Text;
                user.Adress = txtAdress.Text;
                user.TypeUser = (UserType)comboTypeUser.SelectedItem;

                if (user.TypeUser == UserType.Teacher && comboJobPosition.Visible)
                    user.JobPosition = (JobPositionType?)comboJobPosition.SelectedItem;
                else
                    user.JobPosition = null;

                if (Mode == FormMode.Add)
                {
                    var createUser = new UserCreateDTO
                    {
                        UserName = user.UserName,
                        Nombre = user.Nombre,
                        Apellido = user.Apellido,
                        Email = user.Email,
                        Dni = user.Dni,
                        Adress = user.Adress,
                        TypeUser = user.TypeUser,
                        StudentNumber = user.StudentNumber,
                        JobPosition = user.JobPosition,
                        Password = txtDni.Text

                    };

                    await UsersApiClient.AddAsync(createUser);
                }
                else
                {
                    var updateUser = new UserUpdateDTO
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Nombre = user.Nombre,
                        Apellido = user.Apellido,
                        Email = user.Email,
                        Dni = user.Dni,
                        Adress = user.Adress,
                        TypeUser = user.TypeUser,
                        JobPosition = user.JobPosition,
                        StudentNumber = user.StudentNumber,
                        DateOfAdmission = user.DateOfAdmission,
                        DateOfHire = user.DateOfHire,
                        Status = user.Status,
                        Password = string.IsNullOrWhiteSpace(txtDni.Text) ? null : txtDni.Text
                    };

                    await UsersApiClient.UpdateAsync(updateUser);
                }

                MessageBox.Show("Usuario guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateFields()
        {
            bool isValid = true;

            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider.SetError(txtUserName, "El nombre de usuario es requerido");
                isValid = false;
            }

            if (Mode == FormMode.Add && string.IsNullOrWhiteSpace(txtDni.Text))
            {
                errorProvider.SetError(txtDni, "La contraseña es requerida");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider.SetError(txtNombre, "El nombre es requerido");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtStudentNumber.Text))
            {
                errorProvider.SetError(txtStudentNumber, "El legajo es requerido");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                errorProvider.SetError(txtApellido, "El apellido es requerido");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "El email es requerido");
                isValid = false;
            }
            else if (!IsValidEmail(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Formato de email no válido");
                isValid = false;
            }

            return isValid;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private void comboJobPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
