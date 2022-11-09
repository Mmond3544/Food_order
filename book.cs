using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WinFormsApp1
{
    public partial class book : Form
    {
        public book()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Initial Catalog=test; Trusted_Connection=True");
        DataSet ds = new DataSet();
        public void clear()
        {
            txtID.Text = "";
            txtBook.Text = "";
            txtAmount.Text = "";
            txtVolume.Text = "";
            cbbType.SelectedIndex = 0;
        }
        private void book_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM Book", cn);
            da1.Fill(ds, "B");
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID,Book,Volume,Amount,TypeName FROM Book,BookType WHERE BookType.TypeBook = Book.Type", cn);
            da.Fill(ds, "BV");
            dgv.DataSource = ds.Tables["BV"];

            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM BookType", cn);
            da2.Fill(ds, "BT");
            cbbType.DisplayMember = "TypeName";
            cbbType.ValueMember = "TypeBook";
            cbbType.DataSource = ds.Tables["BT"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRow[] dr = ds.Tables["B"].Select("Book = '" + txtBook.Text + "'");
            if (dr.Length == 0)
            {
                DataRow drd = ds.Tables["B"].NewRow();
                drd["ID"] = txtID.Text;
                drd["Book"] = txtBook.Text;
                drd["Volume"] = txtVolume.Text;
                drd["Amount"] = txtAmount.Text;
                drd["Type"] = cbbType.SelectedValue;
                ds.Tables["B"].Rows.Add(drd);
            }
            else
            {
                MessageBox.Show("มี ID นี้แล้ว", "Error");
            }
            dgv.DataSource = ds.Tables["B"];
            clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Book", cn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "B");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataRow[] dr = ds.Tables["B"].Select("ID = '" + txtID.Text + "'");
            if (dr.Length == 0)
            {
                MessageBox.Show("ไม่พบข้อมูล");
            }
            else
            {
                dr[0]["Book"] = txtBook.Text;
                dr[0]["Volume"] = txtVolume.Text;
                dr[0]["Amount"] = txtAmount.Text;
                dr[0]["Type"] = cbbType.SelectedValue;
                dgv.DataSource = ds.Tables["B"];
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String ID_cus = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            String id = String.Format("ID='{0}'", ID_cus);
            DataRow[] dr = ds.Tables["B"].Select(id);
            if (dr.Length != 0)
            {
                txtID.Text = dr[0]["ID"].ToString();
                txtBook.Text = dr[0]["Book"].ToString();
                txtVolume.Text = dr[0]["Volume"].ToString();
                txtAmount.Text = dr[0]["Amount"].ToString();
                cbbType.SelectedValue = dr[0]["Type"].ToString();
                dgv.Rows[e.RowIndex].Selected = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            DataRow[] dr = ds.Tables["B"].Select("ID = '" + txtID.Text + "'");
            if (dr.Length == 0)
            {
                MessageBox.Show("ไม่พบข้อมูล");
            }
            else
            {
                dr[0].Delete();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Book", cn);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "B");
                ds.Tables["B"].AcceptChanges(); //ลบแถว
                dgv.DataSource = ds.Tables["B"];
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "SELECT TOP 1 * FROM Book ORDER BY ID DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            da.Fill(dt);
            int topid = Convert.ToInt32(dt.Rows[0]["ID"]) + 1;
            txtID.Text = topid.ToString();
            txtBook.Text = "";
            txtVolume.Text = "";
            txtAmount.Text = "";
            cbbType.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ds.Tables.Contains("search"))
            {
                ds.Tables.Remove("search");
            }
            string sql = "SELECT * FROM Book WHERE ID LIKE '%" + txtSearch.Text + "%' OR Book LIKE '%" + txtSearch.Text + "%'  OR Type LIKE '%" + txtSearch.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "search");
            dgv.DataSource = ds.Tables["search"];
        }
    }
}
