using API.Clients;
using Domain.Model;
using DTOs;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class UserList : Form
    {
        public UserList()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            UsersDataGridView.Dock = DockStyle.Fill;
            UsersDataGridView.AutoGenerateColumns = false;
            UsersDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            UsersDataGridView.MultiSelect = false;
            UsersDataGridView.AllowUserToAddRows = false; 
            UsersDataGridView.AllowUserToDeleteRows = false;
            UsersDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 80
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UserName",
                HeaderText = "Usuario",
                DataPropertyName = "UserName",
                Width = 150
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "Nombre",
                DataPropertyName = "Name",
                Width = 150
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LastName",
                HeaderText = "Apellido",
                DataPropertyName = "LastName",
                Width = 150
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                Width = 200
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TypeUser",
                HeaderText = "Tipo de Usuario",
                DataPropertyName = "TypeUser",
                Width = 120
            });
            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StudentNumber",
                HeaderText = "Legajo",
                DataPropertyName = "StudentNumber",
                Width = 100
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DateOfAdmission",
                HeaderText = "Fecha Ingreso",
                DataPropertyName = "DateOfAdmission",
                Width = 120
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "JobPosition",
                HeaderText = "Cargo Docente",
                DataPropertyName = "JobPosition",
                Width = 130
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DateOfHire",
                HeaderText = "Fecha Contrato",
                DataPropertyName = "DateOfHire",
                Width = 120
            });
        }

         private void Users_Load(object sender, EventArgs e)
        {
            this.GetAllUsers();
        }

        private async void GetAllUsers()
        {
            try
            {
                var users = await UsersApiClient.GetAllAsync();
                UsersDataGridView.Rows.Clear();

                foreach (var user in users)
                {
                    int index = UsersDataGridView.Rows.Add(
                        user.Id,
                        user.UserName,
                        user.Name,
                        user.LastName,
                        user.Email,
                        user.TypeUser,
                        user.TypeUser == UserType.Student ? user.StudentNumber : "-",
                        user.TypeUser == UserType.Student ? user.DateOfAdmission?.ToString("dd/MM/yyyy") ?? "-" : "-",
                        user.TypeUser == UserType.Teacher ? user.JobPosition?.ToString() ?? "-" : "-",
                        user.TypeUser == UserType.Teacher ? user.DateOfHire?.ToString("dd/MM/yyyy") ?? "-" : "-"
                    );

                    UsersDataGridView.Rows[index].Tag = user;
                }

                updateButton.Enabled = deleteButton.Enabled = users.Any();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los usuarios: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                updateButton.Enabled = deleteButton.Enabled = false;
            }
        }


        private void AddUserButton_Click(object sender, EventArgs e)
        {
            var newUser = new UserDTO();
            using (var userDetail = new UserDetail(FormMode.Add, newUser))
            {
                if (userDetail.ShowDialog() == DialogResult.OK)
                    GetAllUsers();
            }
        }

        private async void UpdateUserButton_Click(object sender, EventArgs e)
        {
            var seleccionado = SelectedUser();
            if (seleccionado == null) return;

            try
            {
                var user = await UsersApiClient.GetAsync(seleccionado.Id);
                using (var userDetail = new UserDetail(FormMode.Update, user))
                {
                    if (userDetail.ShowDialog() == DialogResult.OK)
                        GetAllUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuario para modificar: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            var seleccionado = SelectedUser();
            if (seleccionado == null) return;

            var result = MessageBox.Show(
                $"¿Está seguro que desea eliminar el usuario '{seleccionado.UserName}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await UsersApiClient.DeleteAsync(seleccionado.Id);
                    GetAllUsers();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    MessageBox.Show($"Error al eliminar usuario: {ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private UserDTO SelectedUser()
        {
            if (UsersDataGridView.SelectedRows.Count == 0)
                return null;

            return (UserDTO)UsersDataGridView.SelectedRows[0].Tag;
        }

    }
}
