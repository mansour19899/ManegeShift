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
        DateTime DateStart, DateEnd,Today;
        string YearStart, YearEnd, MonthStart, MonthEnd, DayStart, DayEnd = "";
        List<Button> buttons;
        List<DateTime> listDate;
        public WeekForm()
        {
            InitializeComponent();
            buttons = new List<Button>() {btn1,btn2,btn3, btn4,btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12, btn13, btn14};
            listDate = new List<DateTime>();
     
        }

        private void WeekForm_Load(object sender, EventArgs e)
        {
            Today = DateTime.Today;

            var x = Today.ToPersianDateString().Split('/');
            var xx = Today.ToPersianDateString().AddDaysToShamsiDate(13).Split('/');
            YearStart = x[0].Substring(2);
            MonthStart = x[1];
            DayStart = x[2];
            YearEnd = xx[0].Substring(2);
            MonthEnd = xx[1];
            DayEnd = xx[2];

            cmbYearStart.Items.Add(YearStart);
            cmbYearEnd.Items.Add(YearStart);
            if(YearStart!=YearEnd)
            {
                cmbYearStart.Items.Add(YearEnd);
                cmbYearEnd.Items.Add(YearEnd);
            }

            cmbYearStart.Text = YearStart;
            cmbYearEnd.Text = YearEnd;

            cmbMonthStart.Items.Add(MonthStart);
            cmbMonthEnd.Items.Add(MonthStart);
            if (MonthStart != MonthEnd)
            {
                cmbMonthStart.Items.Add(MonthEnd);
                cmbMonthEnd.Items.Add(MonthEnd);
            }

            cmbMonthStart.Text =MonthStart;
            cmbMonthEnd.Text = MonthEnd;

            var list = Today.ToPersianDateString().ReturnDaysOfMonth();
            cmbDayStart.DataSource = list;

            var listt = Today.AddDays(30).ToPersianDateString().ReturnDaysOfMonth();
            cmbDayEnd.DataSource = listt;

            cmbDayStart.Text = DayStart;
            cmbDayEnd.Text = DayEnd;
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

        private void btn2_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(1), true);
            frm.ShowDialog();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(2), true);
            frm.ShowDialog();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(3), true);
            frm.ShowDialog();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(4), true);
            frm.ShowDialog();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(5), true);
            frm.ShowDialog();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(6), true);
            frm.ShowDialog();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(7), true);
            frm.ShowDialog();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(8), true);
            frm.ShowDialog();
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(9), true);
            frm.ShowDialog();
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(10), true);
            frm.ShowDialog();
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(11), true);
            frm.ShowDialog();
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(12), true);
            frm.ShowDialog();
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(13), true);
            frm.ShowDialog();
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

        private void button1_Click(object sender, EventArgs e)
        {
            listDate.Clear();


            DateStart = ("13" + cmbYearStart.Text + "/" + cmbMonthStart.Text + "/" + cmbDayStart.Text).ToGeorgianDateTime();
            DateEnd = ("13" + cmbYearEnd.Text + "/" + cmbMonthEnd.Text + "/" + cmbDayEnd.Text).ToGeorgianDateTime();
            bool loop = true;
            DateTime Date = DateStart;

            int i = 0;
            while (loop)
            {

                if (Date <= DateEnd&i<14)
                {
                    buttons.ElementAt(i).Text = Date.ToPersianDateString()+"\n"+Date.DayOfWeek;
                    buttons.ElementAt(i).Visible = true;
                    i = i + 1;
                    listDate.Add(Date);
                    Date = Date.AddDays(1);
                }
                else
                {
                    loop = false;
                    
                }

            }

            while(i<14)
            {
                buttons.ElementAt(i).Visible = false;
                i = i + 1;
            }
        }
        public void SetShift(DateTime date)
        {
          



        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1(listDate.ElementAt(0),true);
            frm.ShowDialog();
        }
    }
}
