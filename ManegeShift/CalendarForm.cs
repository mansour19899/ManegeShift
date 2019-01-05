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
    public partial class CalendarForm : Form
    {
        DateTime StartDate;
        DateTime EndDate;
        List<ShiftDay> List;
        List<Label> labelList;
        HiiiEntities db;
        string TodayPersian;

        public CalendarForm()
        {
            InitializeComponent();


        }

        private void CalendarForm_Load(object sender, EventArgs e)
        {
            db = new HiiiEntities();
            TodayPersian = DateTime.Today.ToPersianDateString();
            string month = TodayPersian.Substring(5, 2);
            cmbStartYear.Text = TodayPersian.Substring(2, 2);
            cmbStartMonth.Text = month;




            labelList = new List<Label>() { lbl1,lbl2,lbl3,lbl4,lbl5,lbl6,lbl7,lbl8,lbl9,lbl10,lbl11,lbl12,lbl13,lbl14,lbl15,lbl16,lbl17,lbl18,lbl19,lbl20,lbl21
            ,lbl22,lbl23,lbl24,lbl25,lbl26,lbl27,lbl28,lbl29,lbl30,lbl31};

            Set();
            this.cmbStartMonth.SelectedIndexChanged += new System.EventHandler(this.cmbStartMonth_SelectedIndexChanged);
            this.cmbStartYear.SelectedIndexChanged += new System.EventHandler(this.cmbStartYear_SelectedIndexChanged);
            int x = 0;

        }
        public void Set()
        {
            string startDate = "13"+cmbStartYear.Text+"/" + cmbStartMonth.Text + "/01";
            string endDate = startDate.AddDaysToShamsiDate(31);



            StartDate = startDate.ToGeorgianDateTime();
            EndDate = endDate.ToGeorgianDateTime();

            List = db.ShiftDays.Where(p => p.Date >= StartDate & p.Date <= EndDate).ToList();
            var countStaff = db.People.Where(p=>p.IsDelete==false).Count();
            int length = 30;
            bool IsSet = false;
            var month = cmbStartMonth.Text;
            if (month.CompareTo("07") < 0)
            {
                length = 31;
            }
            else
            {
                length = 30;
            }
            if (!StartDate.IsKabiseh() & month.CompareTo("12") == 0)
            {
                length = 29;
            }


            int j = 0;
            for (int i = 0; i < length; i++)
            {
                IsSet = List.Any(p => p.Date == StartDate.AddDays(i) & p.Status_fk != 0);
                if (IsSet)
                {
                    labelList.ElementAt(i).BackColor = Color.DarkViolet;
                    labelList.ElementAt(i).ForeColor = Color.White;
                    labelList.ElementAt(i).Visible = true;

                    var countt = List.Where(p => p.Date == StartDate.AddDays(i) & p.Status_fk != 0).Count();
                    if(countt>=countStaff)
                    {
                        labelList.ElementAt(i).BackColor = Color.DeepSkyBlue;
                        labelList.ElementAt(i).ForeColor = Color.Black;
                    }

                }
                else
                {
                    labelList.ElementAt(i).BackColor = Color.Orchid;
                    labelList.ElementAt(i).ForeColor = Color.Black;
                    labelList.ElementAt(i).Visible = true;
                }
                j = j + 1;
            }

            for (int i=j ; i < 31; i++)
            {
                labelList.ElementAt(i).Visible = false;
            }


        }

        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

            Set();
        }

        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            Set();
        }

        public void CreateShift(Label lbl)
        {
            DateTime Date;
            Date = ("13" + cmbStartYear.Text + "/" + cmbStartMonth.Text + "/" + lbl.Text).ToGeorgianDateTime();

            //if(Date.CompareTo(DateTime.Today.AddDays(30))==1)
            //{
            //    MessageBox.Show(" It is impossible");
            //}
            //else if(Date.CompareTo(DateTime.Today.AddDays(-7)) ==-1)
            //{
            //    MessageBox.Show(" It is impossible");
            //}
            //else
            //{
            //    Form1 frm = new Form1(Date);
            //    frm.ShowDialog();
            //    Set();
            //}

            Form1 frm = new Form1(Date);
            frm.ShowDialog();
            Set();

        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            CreateShift(lbl1);
        }

        private void lbl2_Click(object sender, EventArgs e)
        {
            CreateShift(lbl2);
        }

        private void lbl3_Click(object sender, EventArgs e)
        {
            CreateShift(lbl3);
        }

        private void lbl4_Click(object sender, EventArgs e)
        {
            CreateShift(lbl4);
        }

        private void lbl5_Click(object sender, EventArgs e)
        {
            CreateShift(lbl5);
        }

        private void lbl6_Click(object sender, EventArgs e)
        {
            CreateShift(lbl6);
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            CreateShift(lbl7);
        }

        private void lbl8_Click(object sender, EventArgs e)
        {
            CreateShift(lbl8);
        }

        private void lbl9_Click(object sender, EventArgs e)
        {
            CreateShift(lbl9);
        }

        private void lbl10_Click(object sender, EventArgs e)
        {
            CreateShift(lbl10);
        }

        private void lbl11_Click(object sender, EventArgs e)
        {
            CreateShift(lbl11);
        }

        private void lbl12_Click(object sender, EventArgs e)
        {
            CreateShift(lbl12);
        }

        private void lbl13_Click(object sender, EventArgs e)
        {
            CreateShift(lbl13);
        }

        private void lbl14_Click(object sender, EventArgs e)
        {
            CreateShift(lbl14);
        }

        private void lbl15_Click(object sender, EventArgs e)
        {
            CreateShift(lbl15);
        }

        private void lbl16_Click(object sender, EventArgs e)
        {
            CreateShift(lbl16);
        }

        private void lbl17_Click(object sender, EventArgs e)
        {
            CreateShift(lbl17);
        }

        private void lbl18_Click(object sender, EventArgs e)
        {
            CreateShift(lbl18);
        }

        private void lbl19_Click(object sender, EventArgs e)
        {
            CreateShift(lbl19);
        }

        private void lbl20_Click(object sender, EventArgs e)
        {
            CreateShift(lbl20);
        }

        private void lbl21_Click(object sender, EventArgs e)
        {
            CreateShift(lbl21);
        }

        private void lbl22_Click(object sender, EventArgs e)
        {
            CreateShift(lbl22);
        }

        private void lbl23_Click(object sender, EventArgs e)
        {
            CreateShift(lbl23);
        }

        private void lbl24_Click(object sender, EventArgs e)
        {
            CreateShift(lbl24);
        }

        private void lbl25_Click(object sender, EventArgs e)
        {
            CreateShift(lbl25);
        }

        private void lbl26_Click(object sender, EventArgs e)
        {
            CreateShift(lbl26);
        }

        private void lbl27_Click(object sender, EventArgs e)
        {
            CreateShift(lbl27);
        }

        private void lbl28_Click(object sender, EventArgs e)
        {
            CreateShift(lbl28);
        }

        private void lbl29_Click(object sender, EventArgs e)
        {
            CreateShift(lbl29);
        }

        private void lbl30_Click(object sender, EventArgs e)
        {
            CreateShift(lbl30);
        }

        private void lbl31_Click(object sender, EventArgs e)
        {
            CreateShift(lbl31);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
