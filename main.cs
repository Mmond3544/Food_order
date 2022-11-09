using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        public static string log = "";
        private void main_Load(object sender, EventArgs e)
        {
            Log_in l = new Log_in();
            l.ShowDialog();

            if (log.Equals(""))
            {
                this.Close();
            }

        }
        private void close(Form frm)
        {
            foreach (Form c in this.MdiChildren)
            {
                if (c.Name != frm.Name) //ชื่อไม่ตรงกับformที่กำลังเปิด
                {
                    c.Close();
                }
                else
                {
                    return;
                }

            }
        }
        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 c = new Form1();
            c.MdiParent = this;
            c.WindowState = FormWindowState.Maximized;
            c.Show();
            
            close(c);
        }

        private void bookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            book b = new book();
            b.MdiParent = this;
            b.WindowState = FormWindowState.Maximized;
            b.Show();

            close(b);
        }
    }
}
