using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangMyPham.DAL;

namespace QuanLyCuaHangMyPham
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool Login(string username, string password)
        {
            return AccountBL.Instance.Login(username, password);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbxUsername.Text;
            string password = tbxPassword.Text;
            if (Login(username, password))
            {
                AccountBL loginAccount = AccountBL.Instance.GetAccountByUsername(username);
                FormAdmin f = new FormAdmin(loginAccount);
                this.Hide();
                try
                {
                    f.ShowDialog();
                    tbxUsername.Clear();
                    tbxPassword.Clear();
                    this.Show();
                }
                catch { }
                
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
            }
        }
    }
}
