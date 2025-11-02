//using API.Clients;
//using DTOs;
//using System;
//using System.Drawing;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace WindowsForms
//{
//    public partial class UserProfile : Form
//    {
//        private readonly int _userId;
//        private UserDTO? _user;

//        public UserProfile(int userId)
//        {
//            _userId = userId;
//            InitializeComponent();
//            Load += UserProfile_Load;
//        }

//        private async void UserProfile_Load(object sender, EventArgs e)
//        {
//            await LoadUserAsync();
//        }

//        private async Task LoadUserAsync()
//        {
//            try
//            {
//                _user = await UsersApiClient.GetAsync(_userId);
//                if (_user == null)
//                {
//                    MessageBox.Show("Usuario no encontrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    this.Close();
//                    return;
//                }

//                lblUser.Text = _user.UserName;
//                lblName.Text = _user.LastName;
//                lblEmail.Text = _user.Email;
//                lblDni.Text = _user.Dni;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error cargando perfil: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                this.Close();
//            }
//        }

//        private void chkShowPasswords_CheckedChanged(object sender, EventArgs e)
//        {
//            bool show = chkShowPasswords.Checked;
//            txtCurrentPassword.UseSystemPasswordChar = !show;
//            txtNewPassword.UseSystemPasswordChar = !show;
//            txtConfirmPassword.UseSystemPasswordChar = !show;
//        }

//        private async void BtnChangePassword_Click(object? sender, EventArgs e)
//        {
//            if (_user == null) return;

//            var current = txtCurrentPassword.Text?.Trim() ?? string.Empty;
//            var np = txtNewPassword.Text?.Trim() ?? string.Empty;
//            var cp = txtConfirmPassword.Text?.Trim() ?? string.Empty;

//            if (string.IsNullOrEmpty(current))
//            {
//                MessageBox.Show("Ingrese la contraseña actual.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (string.IsNullOrEmpty(np))
//            {
//                MessageBox.Show("Ingrese la nueva contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (np != cp)
//            {
//                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                btnChangePassword.Enabled = false;
//                Cursor.Current = Cursors.WaitCursor;

//                await UsersApiClient.ChangePasswordAsync(_user.Id, current, np);

//                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                txtCurrentPassword.Clear();
//                txtNewPassword.Clear();
//                txtConfirmPassword.Clear();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error al cambiar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            {
//                btnChangePassword.Enabled = true;
//                Cursor.Current = Cursors.Default;
//            }
//        }
//    }
//}

//using API.Clients;
//using DTOs;
//using System;
//using System.Drawing;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace WindowsForms
//{
//    public partial class UserProfile : Form
//    {
//        private readonly int _userId;
//        private UserDTO? _user;

//        public UserProfile(int userId)
//        {
//            _userId = userId;
//            InitializeComponent();
//        }

//        // ✅ Carga asíncrona sobrescribiendo OnLoad
//        protected override async void OnLoad(EventArgs e)
//        {
//            base.OnLoad(e);
//            await LoadUserAsync();
//        }

//        // ✅ Carga los datos del usuario
//        private async Task LoadUserAsync()
//        {
//            try
//            {

//                _user = await UsersApiClient.GetAsync(_userId);

//                if (_user == null)
//                {
//                    lblUser.Text = lblName.Text = lblEmail.Text = lblDni.Text = "-";
//                    return;
//                }

//                lblUser.Text = _user.UserName ?? "-";
//                lblName.Text = $"{_user.Name} {_user.LastName}".Trim();
//                lblEmail.Text = _user.Email ?? "-";
//                lblDni.Text = _user.Dni ?? "-";
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error cargando perfil: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                this.Close();
//            }
//        }

//        // ✅ Mostrar / ocultar contraseñas
//        private void chkShowPasswords_CheckedChanged(object sender, EventArgs e)
//        {
//            bool show = chkShowPasswords.Checked;
//            txtCurrentPassword.UseSystemPasswordChar = !show;
//            txtNewPassword.UseSystemPasswordChar = !show;
//            txtConfirmPassword.UseSystemPasswordChar = !show;
//        }

//        // ✅ Cambio de contraseña con validaciones seguras
//        private async void btnChangePassword_Click(object sender, EventArgs e)
//        {
//            if (_user == null)
//            {
//                MessageBox.Show("No hay usuario cargado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            var current = txtCurrentPassword.Text?.Trim() ?? string.Empty;
//            var np = txtNewPassword.Text?.Trim() ?? string.Empty;
//            var cp = txtConfirmPassword.Text?.Trim() ?? string.Empty;

//            if (string.IsNullOrWhiteSpace(current))
//            {
//                MessageBox.Show("Ingrese la contraseña actual.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (string.IsNullOrWhiteSpace(np))
//            {
//                MessageBox.Show("Ingrese la nueva contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (np != cp)
//            {
//                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                btnChangePassword.Enabled = false;
//                //using (new WaitCursor())
//                {
//                    await UsersApiClient.ChangePasswordAsync(_user.Id, current, np);
//                }

//                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                txtCurrentPassword.Clear();
//                txtNewPassword.Clear();
//                txtConfirmPassword.Clear();
//                chkShowPasswords.Checked = false;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error al cambiar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            {
//                btnChangePassword.Enabled = true;
//            }
//        }
//    }

// ✅ Clase auxiliar para usar el cursor de espera fácilmente
//    public sealed class WaitCursor : IDisposable
//    {
//        private readonly Cursor _oldCursor;

//        public WaitCursor()
//        {
//            _oldCursor = Cursor.Current;
//            Cursor.Current = Cursors.WaitCursor;
//        }

//        public void Dispose()
//        {
//            Cursor.Current = _oldCursor;
//        }
//    }
//}

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
        // Ahora no es readonly: permitimos resolver el usuario actual si no se pasa Id.
        private int _userId;
        private UserDTO? _user;

        public UserProfile() : this(0) { }

        public UserProfile(int userId)
        {
            _userId = userId;
            InitializeComponent();

            // Asegurar comportamiento por defecto
            txtCurrentPassword.UseSystemPasswordChar = true;
            txtNewPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;

            // Wire event handler (el diseñador ya pone el CheckedChanged del checkbox).
            btnChangePassword.Click -= btnChangePassword_Click;
            btnChangePassword.Click += btnChangePassword_Click;

            // Permite enviar con Enter
            this.AcceptButton = btnChangePassword;
        }

        // OnLoad: si no se pasó Id, intentamos obtener el usuario logueado desde AuthServiceProvider.
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Si no recibimos id, intentar recuperar el usuario actual
            if (_userId <= 0)
            {
                try
                {
                    var auth = AuthServiceProvider.Instance; // puede lanzar InvalidOperationException si no está registrado
                    var current = await auth.GetCurrentUserAsync();
                    if (current == null)
                    {
                        MessageBox.Show("No se pudo determinar el usuario actual. Abra el perfil desde el menú después de iniciar sesión.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return;
                    }
                    _userId = current.Id;
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("El servicio de autenticación no está inicializado en esta aplicación. Abra el perfil desde el menú tras iniciar sesión.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // Cargar datos del usuario
            await LoadUserAsync();
        }

        private async Task LoadUserAsync()
        {
            try
            {
                btnChangePassword.Enabled = false;

                _user = await UsersApiClient.GetAsync(_userId);
                if (_user == null)
                {
                    lblUser.Text = lblName.Text = lblEmail.Text = lblDni.Text = "-";
                    MessageBox.Show("Usuario no encontrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                lblUser.Text = _user.UserName ?? "-";
                lblName.Text = $"{_user.Name} {_user.LastName}".Trim();
                lblEmail.Text = _user.Email ?? "-";
                lblDni.Text = _user.Dni ?? "-";

                // focus en campo contraseña actual para mejorar UX
                txtCurrentPassword.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando perfil: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                btnChangePassword.Enabled = true;
            }
        }

        // Mostrar / ocultar contraseñas (el diseñador ya asocia este evento)
        private void chkShowPasswords_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPasswords.Checked;
            txtCurrentPassword.UseSystemPasswordChar = !show;
            txtNewPassword.UseSystemPasswordChar = !show;
            txtConfirmPassword.UseSystemPasswordChar = !show;
        }

        // Maneja el cambio de contraseña validando contraseña actual + nueva
        private async void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (_user == null)
            {
                MessageBox.Show("No hay usuario cargado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var current = txtCurrentPassword.Text?.Trim() ?? string.Empty;
            var np = txtNewPassword.Text?.Trim() ?? string.Empty;
            var cp = txtConfirmPassword.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(current))
            {
                MessageBox.Show("Ingrese la contraseña actual.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCurrentPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(np))
            {
                MessageBox.Show("Ingrese la nueva contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (np.Length < 6)
            {
                MessageBox.Show("La nueva contraseña debe tener al menos 6 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (np != cp)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                btnChangePassword.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                // El cliente envía current + new al API; el API valida la contraseña actual
                await UsersApiClient.ChangePasswordAsync(_user.Id, current, np);

                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                chkShowPasswords.Checked = false;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Sesión inválida o expirada. Por favor inicie sesión nuevamente.", "Sesión inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Si el servidor devuelve un mensaje (por ejemplo "La contraseña actual es incorrecta"), se muestra aquí.
                MessageBox.Show($"Error al cambiar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnChangePassword.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }
    }
}

