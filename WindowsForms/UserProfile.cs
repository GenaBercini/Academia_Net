using API.Clients;
using DTOs;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class UserProfile : Form
    {
        private int _userId;
        private UserDTO? _user;

        public UserProfile(int userId)
        {
            _userId = userId;
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_userId <= 0)
            {
                try
                {
                    var auth = AuthServiceProvider.Instance; 
                    var current = await auth.GetCurrentUserAsync();
                    if (current == null)
                    {
                        MessageBox.Show("No se pudo determinar el usuario actual.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return;
                    }
                    _userId = current.Id;
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("El servicio de autenticación no esta funcionando.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener usuario actual: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }

            await LoadUserAsync();
        }

        private async Task LoadUserAsync()
        {
            try
            {
                _user = await UsersApiClient.GetAsync(_userId);
                if (_user == null)
                {
                    MessageBox.Show("Usuario no encontrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                lblUser.Text = _user.UserName ?? "-";
                lblName.Text = $"{_user.Name} {_user.LastName}";
                lblEmail.Text = _user.Email ?? "-";
                lblDni.Text = _user.Dni ?? "-";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando perfil: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void chkShowPasswords_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPasswords.Checked;
            txtCurrentPassword.UseSystemPasswordChar = !show;
            txtNewPassword.UseSystemPasswordChar = !show;
            txtConfirmPassword.UseSystemPasswordChar = !show;
        }
        private async void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (_user == null)
            {
                MessageBox.Show("No hay usuario cargado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var currentPassowrd = txtCurrentPassword.Text?.Trim() ?? string.Empty;
            var newPassword = txtNewPassword.Text?.Trim() ?? string.Empty;
            var confirmPassword = txtConfirmPassword.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(currentPassowrd))
            {
                MessageBox.Show("Ingrese la contraseña actual.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCurrentPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Ingrese la nueva contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("La nueva contraseña debe tener al menos 6 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                await UsersApiClient.ChangePasswordAsync(_user.Id, currentPassowrd, newPassword);

                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                chkShowPasswords.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

