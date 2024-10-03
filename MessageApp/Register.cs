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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        SQLConnection conn = new SQLConnection();

        private void lnkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("select userNo from TblUsers", conn.connection());
            SqlDataReader reader = sqlCommand.ExecuteReader();
            SqlCommand cmd = new SqlCommand("insert into TblUsers (userName,userSurname,userNo,userPassword) values (@p1,@p2,@p3,@p4)", conn.connection());
            cmd.Parameters.AddWithValue("@p1", txtName.Text);
            cmd.Parameters.AddWithValue("@p2", txtSurname.Text);
            cmd.Parameters.AddWithValue("@p3", mskNo.Text);
            cmd.Parameters.AddWithValue("@p4", txtPassword.Text);
            while (reader.Read())
            {
                if (reader.GetString(0) != mskNo.Text)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registered succesfully");
                }
                else
                {
                    MessageBox.Show("Chose different User No");
                }
            }
        }
    }
}
