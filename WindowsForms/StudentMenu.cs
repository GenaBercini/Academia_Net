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

namespace WindowsForms
{
    public partial class StudentMenu : Form
    {
        public StudentMenu()
        {
            InitializeComponent();
        }
        //private void OpenOtherForm(Form secondaryForm)
        //{
        //    primaryPanel.Controls.Clear();
        //    secondaryForm.TopLevel = false;
        //    secondaryForm.FormBorderStyle = FormBorderStyle.None;
        //    secondaryForm.Dock = DockStyle.Fill;
        //    primaryPanel.Controls.Add(secondaryForm);
        //    secondaryForm.Show();
        //}

        private async void Logout(object sender, EventArgs e)
        {
            var authService = AuthServiceProvider.Instance;
            await authService.LogoutAsync();

            Application.Restart();
        }
    }
}
