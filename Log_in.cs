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

namespace WinFormsApp1
{
    public partial class Log_in : Form
    {
        public Log_in()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Initial Catalog=test; Trusted_Connection=True");
        DataSet ds = new DataSet();
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = string.Format("SELECT * FROM login WHERE Username = '" + txtUser.Text + "' AND Password = '" + txtPass.Text + "'");
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                string name = dt.Rows[0]["Username"].ToString();
                MessageBox.Show("Welcome " + name);
                main.log = "1";
                this.Close();
            }
            else
            {
                MessageBox.Show("username หรือ password ผิด");
            }
        }
    }
}
