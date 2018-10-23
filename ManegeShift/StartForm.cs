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
    
    public partial class StartForm : Form
    {
        ManageShiftEntities1 db;
        List< DailyWeek> WeekShift;
        DateTime Today = DateTime.Today;
       // DateTime Today = new DateTime(2019, 1, 22);
        string YearStart, YearEnd, MonthStart, MonthEnd, DayStart, DayEnd = "";

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDay_Click(object sender, EventArgs e)
        {
           panel2.Visible = true;

     



        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime Date;
            Date = ("13" + cmbYear.Text + "/" + cmbMonth.Text + "/" + cmbDay.Text).ToGeorgianDateTime();

            Form1 frm = new Form1(Date);
            frm.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
        }

        private void btnSetWeek_Click(object sender, EventArgs e)
        {
            WeekForm frm = new WeekForm();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Info frm = new Info();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnWeek.Enabled = false;
            db = new ManageShiftEntities1();
            WeekShift = db.DailyWeeks.ToList();
            DateTime DateStart, DateEnd;
            DateStart=( "13"+cmbYearStart.Text + "/" + cmbMonthStart.Text + "/" + cmbDayStart.Text).ToGeorgianDateTime();
            DateEnd = ("13" + cmbYearEnd.Text + "/" + cmbMonthEnd.Text + "/" + cmbDayEnd.Text).ToGeorgianDateTime();

            bool loop = true;
            DateTime Date = DateStart;
            while (loop)
            {

                if (Date <= DateEnd)
                {
                    SetShift(Date);
                    Date = Date.AddDays(1);
                }
                else
                    loop = false;


            }
            panel1.Visible = false;
            btnDay.Visible = true;
            btnWeek.Enabled = true;
        }

        private void btnSaturday_Click(object sender, EventArgs e)
        {
           panel1.Visible = true;
        }

        public StartForm()
        {
            InitializeComponent();
            var x = Today.ToPersianDateString().Split('/');
            var xx = Today.ToPersianDateString().AddDaysToShamsiDate(30).Split('/');
            YearStart = x[0].Substring(2);
            MonthStart = x[1];
            DayStart=x[2];
            YearEnd = xx[0].Substring(2);
            MonthEnd = xx[1];
            DayEnd = xx[2];
            int y = 0;
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            lblDateShamsi.Text = Today.ToPersianDateString();
            lblDateDay.Text = Today.DayOfWeek.ToString();
            lblDateMiladi.Text = Today.Year.ToString() + "/" + Today.Month.ToString() + "/" + Today.Day.ToString();

            cmbYearStart.Items.Add(YearStart);
            cmbYearEnd.Items.Add(YearStart);
            cmbYear.Items.Add(YearStart);
            if(YearStart!=YearEnd)
            {
                cmbYearStart.Items.Add(YearEnd);
                cmbYearEnd.Items.Add(YearEnd);
                cmbYear.Items.Add(YearEnd);
            }

            cmbMonthStart.Items.Add(MonthStart);
            cmbMonthEnd.Items.Add(MonthStart);
            cmbMonth.Items.Add(MonthStart);
            if (MonthStart!=MonthEnd)
            {
                cmbMonthStart.Items.Add(MonthEnd);
                cmbMonthEnd.Items.Add(MonthEnd);
                cmbMonth.Items.Add(MonthEnd);
            }
             

            cmbYearStart.Text = YearStart;
            cmbYearEnd.Text = YearEnd;
            cmbMonthStart.Text = MonthStart;
            cmbMonthEnd.Text = MonthEnd;
            cmbDayStart.Text = DayStart;
            cmbDayEnd.Text = DayEnd;

            cmbYear.Text = YearStart;
            cmbMonth.Text = MonthStart;
            cmbDay.Text = DayStart;

        }

        public void SetShift(DateTime date)
        {
            var y =Convert.ToInt16( date.DayOfWeek);
            var yy = date.DayOfWeek;
            var yyy = date.ToPersianDateString();

            int DayOfWeek ;
            switch (y)

            {
                case 0:
                    DayOfWeek = 2;
                    break;
                case 1:
                    DayOfWeek = 3;
                    break;
                case 2:
                    DayOfWeek = 4;
                    break;
                case 3:
                    DayOfWeek = 5;
                    break;
                case 4:
                    DayOfWeek = 6;
                    break;
                case 5:
                    DayOfWeek = 7;
                    break;
                case 6:
                    DayOfWeek = 1;
                    break;

                default:
                    DayOfWeek = 0;
                    break;
            }

            var delete = db.ShiftDays.Any(p => p.Date == date);
            if(delete)
            {
                var del = db.ShiftDays.Where(p => p.Date == date).ToList();
                foreach (var item in del)
                {
                    db.ShiftDays.Remove(item);
                }
            }

            var t = WeekShift.Where(p => p.IdDay == DayOfWeek).Select(p=>new ShiftDay {Person_fk=p.Person_fk,Status_fk=p.Status_fk,mid=p.Mid,Date=date }).ToList();
            foreach (var item in t)
            {
                db.ShiftDays.Add(item);
            }


            db.SaveChanges();
            


        }
    }
}
