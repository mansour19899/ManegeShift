using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;

namespace ManegeShift
{
    
    public partial class StartForm : Form
    {
        HiiiEntities db;
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

            if(Date<Today)
            {
                MessageBox.Show(" It is impossible");
            }
            else
            {
                Form1 frm = new Form1(Date);
                frm.ShowDialog();
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {
          
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

        private void button4_Click(object sender, EventArgs e)
        {
            CalendarForm frm = new CalendarForm();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
   
            db = new HiiiEntities();
            WeekShift = db.DailyWeeks.ToList();
           



   
            btnDay.Visible = true;
        
        }

        private void btnSaturday_Click(object sender, EventArgs e)
        {
        
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

           if(CheckRunSql())
            {
                lblDateShamsi.Text = Today.ToPersianDateString();
                lblDateDay.Text = Today.DayOfWeek.ToString();
                lblDateMiladi.Text = Today.Year.ToString() + "/" + Today.Month.ToString() + "/" + Today.Day.ToString();


                cmbYear.Items.Add(YearStart);
                if (YearStart != YearEnd)
                {

                    cmbYear.Items.Add(YearEnd);
                }


                cmbMonth.Items.Add(MonthStart);
                if (MonthStart != MonthEnd)
                {

                    cmbMonth.Items.Add(MonthEnd);
                }




                cmbYear.Text = YearStart;
                cmbMonth.Text = MonthStart;
                cmbDay.Text = DayStart;
            }
           else
            {
                MessageBox.Show("Try again");
                this.Close();
            }
           

        }

        public bool CheckRunSql()
        {

            try
            {
                ServiceController sc = new ServiceController("MSSQL$SQLEXPRESS");

                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        return true;
                    case ServiceControllerStatus.Stopped:
                        sc.Start();
                        return true;
                    case ServiceControllerStatus.Paused:
                        sc.Stop();
                        return false;
                    case ServiceControllerStatus.StopPending:
                        return false;
                    case ServiceControllerStatus.StartPending:
                        return false;
                    default:
                        return false;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Check Database");
                return false;
            }
          
        }

 
    }
}
