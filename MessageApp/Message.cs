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
    public partial class Message : Form
    {
        public Message()
        {
            InitializeComponent();
        }

        SQLConnection conn = new SQLConnection();

        public string id;

        void list()
        {
            SqlCommand cmdReceive = new SqlCommand("select messageId as 'Id',userName + userSurname as 'Sender',messageTitle as 'Title',messageContent as 'Content',messageSender as 'Sender id' from TblMessages inner join TblUsers on TblUsers.userId = TblMessages.messageSender where messageReceiver = @p1", conn.connection());
            cmdReceive.Parameters.AddWithValue("@p1", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmdReceive);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.connection().Close();

            SqlCommand cmdSend = new SqlCommand("select messageId as 'Id',userName + ' ' + userSurname as 'Receiver',messageTitle as 'Title',messageContent as 'Content', messageReceiver as 'Receiver Id' from TblMessages inner join TblUsers on TblUsers.userId = TblMessages.messageReceiver where messageSender = @p2", conn.connection());
            cmdSend.Parameters.AddWithValue("@p2", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmdSend);
            DataTable dt2 = new DataTable();
            sqlDataAdapter.Fill(dt2);
            dataGridView2.DataSource = dt2;
            conn.connection().Close();
        }

        private void Message_Load(object sender, EventArgs e)
        {
            list();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("insert into TblMessages (messageSender,messageReceiver,MessageTitle,messageContent) values (@p1,@p2,@p3,@p4)", conn.connection());
            sqlCommand.Parameters.AddWithValue("@p1", id.ToString());
            sqlCommand.Parameters.AddWithValue("@p2", lblReceiverId.Text);
            sqlCommand.Parameters.AddWithValue("@p3", txtTitle.Text);
            sqlCommand.Parameters.AddWithValue("@p4", rchContent.Text);
            sqlCommand.ExecuteNonQuery();
            conn.connection().Close();
            list();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmdDelete = new SqlCommand("delete from TblMessages where messageId = @p1", conn.connection());
            cmdDelete.Parameters.AddWithValue("@p1", lblMsgId.Text);
            cmdDelete.ExecuteNonQuery();
            conn.connection().Close();
            list();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblMsgId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtSender.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTitle.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            rchContent.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            lblReceiverId.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblMsgId.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtSender.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTitle.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            rchContent.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            lblReceiverId.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
