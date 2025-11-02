using API.Clients;
using Domain.Model;
using DTOs;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Shared.Types;


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
        private async void ExportReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                var pdfBytes = await UsersApiClient.GetUsersGradesReportAsync(true);
                if (pdfBytes == null || pdfBytes.Length == 0)
                    throw new Exception("El servidor devolvió un documento vacío.");

                string selectedPath = null;
                this.Invoke(() =>
                {
                    using var sfd = new SaveFileDialog
                    {
                        Filter = "PDF (*.pdf)|*.pdf",
                        FileName = $"ReporteUsuariosNotas_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                        Title = "Guardar reporte de usuarios como",
                        OverwritePrompt = true,
                        RestoreDirectory = true,
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                    };

                    if (sfd.ShowDialog(this) == DialogResult.OK)
                        selectedPath = sfd.FileName;
                });

                if (string.IsNullOrEmpty(selectedPath))
                    return;

                await Task.Run(() => File.WriteAllBytes(selectedPath, pdfBytes));

                MessageBox.Show($"Reporte guardado en:\n{selectedPath}", "Reporte generado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try { Process.Start(new ProcessStartInfo { FileName = selectedPath, UseShellExecute = true }); }
                catch { }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Su sesión ha expirado. Inicie sesión de nuevo.", "Sesión inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al descargar el reporte:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private async void PieChartButton_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    pieChartButton.Enabled = false;
            //    Cursor.Current = Cursors.WaitCursor;

            //    var bytes = await UsersApiClient.GetGradesPieChartAsync();
            //    if (bytes == null || bytes.Length == 0)
            //        throw new Exception("El servidor devolvió un gráfico vacío.");

            //    string selectedPath = null;
            //    this.Invoke(() =>
            //    {
            //        using var sfd = new SaveFileDialog
            //        {
            //            Filter = "PNG (*.png)|*.png",
            //            FileName = $"GraficoNotas_{DateTime.Now:yyyyMMdd_HHmmss}.png",
            //            Title = "Guardar gráfico de notas como",
            //            OverwritePrompt = true,
            //            RestoreDirectory = true,
            //            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            //        };

            //        if (sfd.ShowDialog(this) == DialogResult.OK)
            //            selectedPath = sfd.FileName;
            //    });

            //    if (string.IsNullOrEmpty(selectedPath))
            //        return;

            //    await Task.Run(() => File.WriteAllBytes(selectedPath, bytes));

            //    MessageBox.Show($"Gráfico guardado en:\n{selectedPath}", "Gráfico generado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    try { Process.Start(new ProcessStartInfo { FileName = selectedPath, UseShellExecute = true }); }
            //    catch { }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error al descargar el gráfico:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    pieChartButton.Enabled = true;
            //    Cursor.Current = Cursors.Default;
            //}
        }

    }
}
