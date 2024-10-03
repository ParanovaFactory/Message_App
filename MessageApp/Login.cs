using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SQLConnection conn = new SQLConnection();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select userId, userNo, userPassword from TblUsers", conn.connection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (mskNo.Text == reader.GetString(1) && txtPassword.Text == reader.GetString(2))
                {
                    Message message = new Message();
                    message.id = reader[0].ToString();
                    message.Show();
                    this.Hide();
                }
            }
            conn.connection().Close();
        }

        private void lnkRagister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
