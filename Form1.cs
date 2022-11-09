using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Initial Catalog=test; Trusted_Connection=True");
        DataSet ds = new DataSet();
        public void clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtSurname.Text = "";
            txtTel.Text = "";
            txtAddress.Text = "";
            cbbSex.SelectedIndex = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Customer", cn);
            da.Fill(ds, "C");
            dgv.DataSource = ds.Tables["C"];

            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM SexType", cn);
            da2.Fill(ds, "S");
            cbbSex.DisplayMember = "Sex";
            cbbSex.ValueMember = "IDSex";
            cbbSex.DataSource = ds.Tables["S"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRow[] dr = ds.Tables["C"].Select("ID = '" + txtID.Text + "'");
            if (dr.Length == 0)
            {
                DataRow drd = ds.Tables["C"].NewRow();
                drd["ID"] = txtID.Text;
                drd["Name"] = txtName.Text;
                drd["Surname"] = txtSurname.Text;
                drd["Tel"] = txtTel.Text;
                drd["Address"] = txtAddress.Text;
                drd["Sex"] = cbbSex.SelectedValue;
                ds.Tables["C"].Rows.Add(drd);
            }
            else
            {
                MessageBox.Show("มี ID นี้แล้ว", "Error");
            }
            dgv.DataSource = ds.Tables["C"];
            clear();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            String ID_cus = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            String id = String.Format("ID='{0}'", ID_cus);
            DataRow[] dr = ds.Tables["C"].Select(id);
            if (dr.Length != 0)
            {
                txtID.Text = dr[0]["ID"].ToString();
                txtName.Text = dr[0]["Name"].ToString();
                txtSurname.Text = dr[0]["Surname"].ToString();
                txtTel.Text = dr[0]["Tel"].ToString();
                txtAddress.Text = dr[0]["Address"].ToString();
                cbbSex.SelectedValue = dr[0]["Sex"].ToString();
                dgv.Rows[e.RowIndex].Selected = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Customer", cn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "C");
        }

        private void button4_Click(object sender, EventArgs e)
        {

            DataRow[] dr = ds.Tables["C"].Select("ID = '" + txtID.Text + "'");
            if (dr.Length == 0)
            {
                MessageBox.Show("ไม่พบข้อมูล");
            }
            else
            {
                dr[0].Delete();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Customer", cn);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "C");
                ds.Tables["C"].AcceptChanges(); //ลบแถว
                dgv.DataSource = ds.Tables["C"];
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataRow[] dr = ds.Tables["C"].Select("ID = '" + txtID.Text + "'");
            if (dr.Length == 0)
            {
                MessageBox.Show("ไม่พบข้อมูล");
            }
            else
            {
                dr[0]["Name"] = txtName.Text;
                dr[0]["Surname"] = txtSurname.Text;
                dr[0]["Tel"] = txtTel.Text;
                dr[0]["Address"] = txtAddress.Text;
                dr[0]["Sex"] = cbbSex.SelectedValue;

                dgv.DataSource = ds.Tables["C"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ds.Tables.Contains("search"))
            {
                ds.Tables.Remove("search");
            }
            string sql = "SELECT * FROM Customer WHERE ID LIKE '%" + txtSearch.Text + "%' OR Tel LIKE '%" + txtSearch.Text + "%' OR Name LIKE '%" + txtSearch.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "search");
            dgv.DataSource = ds.Tables["search"];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "SELECT TOP 1 * FROM Customer ORDER BY ID DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            da.Fill(dt);
            int topid = Convert.ToInt32(dt.Rows[0]["ID"]) + 1;
            txtID.Text = topid.ToString();
            txtName.Text = "";
            txtSurname.Text = "";
            txtTel.Text = "";
            txtAddress.Text = "";   
            cbbSex.SelectedIndex = 0;
        }
    }
}