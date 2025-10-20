using API.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsForms
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void OpenOtherForm(Form secondaryForm)
        {
            primaryPanel.Controls.Clear();
            secondaryForm.TopLevel = false;
            secondaryForm.FormBorderStyle = FormBorderStyle.None;
            secondaryForm.Dock = DockStyle.Fill;
            primaryPanel.Controls.Add(secondaryForm);
            secondaryForm.Show();
        }

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            OpenOtherForm(new UserList());
        }

        private void BtnMaterias_Click(object sender, EventArgs e)
        {
            OpenOtherForm(new SubjectList());
        }

        private void BtnPlanes_Click(object sender, EventArgs e)
        {
            OpenOtherForm(new PlanesList());
        }

        private void BtnCursos_Click(object sender, EventArgs e)
        {
            OpenOtherForm(new CursosList());
        }

        private void BtnEspecialidades_Click(object sender, EventArgs e)
        {
            OpenOtherForm(new EspecialidadesList());
        }
        private async void Logout(object sender, EventArgs e)
        {
            var authService = AuthServiceProvider.Instance;
            await authService.LogoutAsync();

            this.Hide();
            using (var loginForm = new Login())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    this.Close();
                }
            }
        }



    }
}
