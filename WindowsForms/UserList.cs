using DTOs;
using API.Clients;
using System.Diagnostics;

namespace WindowsForms
{
    public partial class UserList : Form
    {
        public UserList()
        {
            InitializeComponent();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            this.GetAllAndLoad();
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            UserDTO newUser = new();

            UserDetail userDetail = new UserDetail(FormMode.Add, newUser);

            userDetail.ShowDialog();

            this.GetAllAndLoad();
        }

        private async void UpdateUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                UserDetail userDetail = new();

                int id = this.SelectedItem().Id;

                UserDTO user = await UsersApiClient.GetAsync(id);

                userDetail.Mode = FormMode.Update;
                userDetail.User = user;

                userDetail.ShowDialog();

                this.GetAllAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuario para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                int id = this.SelectedItem().Id;

                var result = MessageBox.Show($"¿Está seguro que desea eliminar el usuario con Id {id}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await UsersApiClient.DeleteAsync(id);
                    this.GetAllAndLoad();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GetAllAndLoad()
        {
            try
            {
                this.UsersDataGridView.DataSource = null;
                this.UsersDataGridView.DataSource = await UsersApiClient.GetAllAsync();

                if (this.UsersDataGridView.Rows.Count > 0)
                {
                    this.UsersDataGridView.Rows[0].Selected = true;
                    this.deleteButton.Enabled = true;
                    this.updateButton.Enabled = true;
                }
                else
                {
                    this.deleteButton.Enabled = false;
                    this.updateButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.deleteButton.Enabled = false;
                this.updateButton.Enabled = false;
            }
        }

        private UserDTO SelectedItem()
        {
            UserDTO user;

            user = (UserDTO)UsersDataGridView.SelectedRows[0].DataBoundItem;

            return user;
        }
    }
}
