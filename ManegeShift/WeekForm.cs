using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManegeShift
{
    public partial class WeekForm : Form
    {
        public WeekForm()
        {
            InitializeComponent();
        }

        private void WeekForm_Load(object sender, EventArgs e)
        {


        }
        private void btnSaturday_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(1);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnSunday_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(2);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnMonday_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(3);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnTuesday_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(4);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnWednesday_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(5);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnThursday_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(6);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnFriday_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(7);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
