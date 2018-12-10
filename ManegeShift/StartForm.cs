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
using Stimulsoft.Report;

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

        private void btnReport_Click(object sender, EventArgs e)
        {

            PrintForm frm = new PrintForm();
            frm.ShowDialog();


            //db = new HiiiEntities();
            //List<Shift> shifts = new List<Shift>();
            //string radif = "1";
            //string morning = "";
            //string mid = "";
            //string evening = "";
            //string split = "";
            //string rest = "";

            //var date = db.ShiftDays.Where(p => p.Date == DateTime.Today).ToList();
            //foreach (var item in date)
            //{
            //    switch (item.Status_fk)
            //    {
            //        case 1:
            //            morning = morning + "," + item.Person.NickName.Trim();
            //            break;
            //        case 2:
            //            mid = mid + "," + item.Person.NickName.Trim()+"("+item.mid.Trim()+")";
            //            break;
            //        case 3:
            //            evening = evening + "," + item.Person.NickName.Trim();
            //            break;
            //        case 4:
            //            split = split + "," + item.Person.NickName.Trim();
            //            break;
            //        case 5:
            //            rest = rest + "," + item.Person.NickName.Trim();
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //shifts.Add(new Shift() {Radif=radif,Date=date.ElementAt(0).Date.ToPersianDateString(),Morning=morning.TrimStart(','),Mid=mid.TrimStart(','), Evening=evening.TrimStart(','),
            //    Split =split.TrimStart(','), Rest=rest.TrimStart(',') });









            ////StiReport stiReport1 = new StiReport();

            ////stiReport1.Load(@"D:\projects\ManegeShift\ManegeShift\bin\Debug\Report.mrt");
            //////stiReport1.Dictionary.Variables["date"].Value = txtDate.Text;
            //////stiReport1.Dictionary.Variables["part"].Value = cmbPart.GetItemText(cmbPart.SelectedItem);
            //////stiReport1.Dictionary.Variables["Variable1"].Value = "maryam";

            //// stiReport1.RegBusinessObject("Shift", shifts);
            
            ////stiReport1.Show();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = cmbDay.Text;
            cmbDay.DataSource = ("13" + cmbYear.Text + "/" + cmbMonth.Text + "/" + cmbDay.Text).ReturnDaysOfMonth();
            cmbDay.Text = temp;
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = cmbDay.Text;
            cmbDay.DataSource = ("13" + cmbYear.Text + "/" + cmbMonth.Text + "/" + cmbDay.Text).ReturnDaysOfMonth();
            cmbDay.Text = temp;
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
            cmbDay.DataSource = DateTime.Today.ToPersianDateString().ReturnDaysOfMonth();

            if (CheckRunSql())
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
