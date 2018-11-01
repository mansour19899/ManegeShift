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
    public partial class Info : Form
    {
        List<Label> labelsStaff;
        HiiiEntities db;
        List<Person> People;
        Person person;
        int SelectId = 0;
        bool SetComboBoxDate = true;
        DateTime Today;
        string TodayPersian;
        int day;
        string Year;
        bool ActiveEvent = true;
        bool ChangePanel = true;

        public Info()
        {

            db = new HiiiEntities();

            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;


            labelsStaff = new List<Label>
            {
                lblS1,lblS2,lblS3,lblS4,lblS5,lblS6,lblS7,lblS8,lblS9,lblS10,lblS11
                ,lblS12,lblS13,lblS14,lblS15,lblS16,lblS17,lblS18,lblS19,lblS20
            };
            SetLabelStaff();

            Today = new DateTime(2018, 12, 1);
             TodayPersian = Today.ToPersianDateString();
            day = Today.ToPersianDateString().ToPersianDayOfWeek();
             Year = TodayPersian.Substring(0, 4);

          

        }

        public void SetLabelStaff()
        {
            People = db.People.Where(p => p.IsDelete == false).OrderByDescending(p => p.Level).ThenBy(p => p.Id).ToList();
            int i = 0;
            foreach (var item in People)
            {
                labelsStaff.ElementAt(i).Text = item.NickName.Trim();
                labelsStaff.ElementAt(i).Tag = item.Id.ToString();
                labelsStaff.ElementAt(i).Visible = true;
                i = i + 1;
            }

            for (int j = i; j < 20; j++)
            {
                labelsStaff.ElementAt(j).Text = "";
                labelsStaff.ElementAt(j).Tag = "";
                labelsStaff.ElementAt(j).Visible = false;
            }
            lblCountStaff.Text = "(" + People.Count().ToString() + ")";
            lblName.Text = "";
        }

        public void Selected(Label lb)
        {
            int id = Convert.ToInt32(lb.Tag);
            SelectId = id;
            person = People.Where(p => p.Id == id).FirstOrDefault();
            txtName.Text = person.Name;
            txtLastName.Text = person.Lastname;
            txtNickName.Text = person.NickName.Trim();
            lblName.ForeColor = Color.Navy;
            lblName.Text = person.Name + "  " + person.Lastname;

            cmbLevel.Text = person.Level.ToString();


            btnAdd.Visible = false;
            btnDelete.Visible = true;
            btnEdit.Visible = true;

            SetCounter();
            if(ActiveEvent)
            {
                this.cmbStartDay.SelectedIndexChanged += new System.EventHandler(this.cmbStartDay_SelectedIndexChanged);
                this.cmbEndDay.SelectedIndexChanged += new System.EventHandler(this.cmbEndDay_SelectedIndexChanged);
                this.cmbEndMonth.SelectedIndexChanged += new System.EventHandler(this.cmbEndMonth_SelectedIndexChanged);
                this.cmbEndYear.SelectedIndexChanged += new System.EventHandler(this.cmbEndYear_SelectedIndexChanged);
                this.cmbStartMonth.SelectedIndexChanged += new System.EventHandler(this.cmbStartMonth_SelectedIndexChanged);
                this.cmbStartYear.SelectedIndexChanged += new System.EventHandler(this.cmbStartYear_SelectedIndexChanged);
                ActiveEvent = false;
            }


            panel3.Visible = true;


        }
        public List<string> CountShift(string startDate, string endDate)
        {
            List<string> temp = new List<string>();
            DateTime StartDate = startDate.ToGeorgianDateTime();
            DateTime EndDate = endDate.ToGeorgianDateTime();
            var list = db.ShiftDays.Where(p => p.Date >= StartDate & p.Date <= EndDate & p.Person_fk == SelectId).ToList();
            temp.Add(list.Where(p => p.Status_fk == 1).Count().ToString());
            temp.Add(list.Where(p => p.Status_fk == 2).Count().ToString());
            temp.Add(list.Where(p => p.Status_fk == 3).Count().ToString());
            temp.Add(list.Where(p => p.Status_fk == 4).Count().ToString());
            temp.Add(list.Where(p => p.Status_fk == 5).Count().ToString());
            return temp;
        }

        public void SetCounter()
        {
            //DateTime Today = DateTime.Today;


            var t = CountShift(Today.AddDays(-day - 6).ToPersianDateString(), Today.AddDays(-day).ToPersianDateString());
            lblWeek1.Text = t.ElementAt(0);
            lblWeek2.Text = t.ElementAt(1);
            lblWeek3.Text = t.ElementAt(2);
            lblWeek4.Text = t.ElementAt(3);
            lblWeek5.Text = t.ElementAt(4);

            var monthh = Convert.ToInt16(TodayPersian.Substring(5, 2)) - 1;
            if (monthh == 0)
            {
                monthh = 12;
                Year = (Convert.ToInt16(Year) - 1).ToString();
            }


            string month = "";
            if (monthh < 10)
                month = "0" + monthh;
            else
                month = monthh.ToString();
            string StartDate = Year + "/" + month + "/01";
            string EndDate = "";
            if (monthh < 7)
            {
                EndDate = Year + "/" + month + "/31";
            }
            else
            {
                EndDate = Year + "/" + month + "/30";
                cmbStartDay.Items.Remove("31");
                cmbEndDay.Items.Remove("31");
            }
            if (!StartDate.IsKabiseh() & monthh == 12)
            {
                EndDate = Year + "/" + month + "/29";
                cmbStartDay.Items.Remove("30");
                cmbEndDay.Items.Remove("30");
            }


            var tt = CountShift(StartDate, EndDate);
            lblMonth1.Text = tt.ElementAt(0);
            lblMonth2.Text = tt.ElementAt(1);
            lblMonth3.Text = tt.ElementAt(2);
            lblMonth4.Text = tt.ElementAt(3);
            lblMonth5.Text = tt.ElementAt(4);

            lblTotal1.Text = db.ShiftDays.Where(p => p.Status_fk == 1 & p.Person_fk == SelectId).Count().ToString();
            lblTotal2.Text = db.ShiftDays.Where(p => p.Status_fk == 2 & p.Person_fk == SelectId).Count().ToString();
            lblTotal3.Text = db.ShiftDays.Where(p => p.Status_fk == 3 & p.Person_fk == SelectId).Count().ToString();
            lblTotal4.Text = db.ShiftDays.Where(p => p.Status_fk == 4 & p.Person_fk == SelectId).Count().ToString();
            lblTotal5.Text = db.ShiftDays.Where(p => p.Status_fk == 5 & p.Person_fk == SelectId).Count().ToString();

            if (SetComboBoxDate)
            {

                SetComboBoxDate = false;
                cmbStartYear.Text = Year.Substring(2,2);
                cmbStartMonth.Text = TodayPersian.Substring(5,2);
                cmbStartDay.Text = "01";

                cmbEndYear.Text= Year.Substring(2, 2);
                cmbEndMonth.Text= TodayPersian.Substring(5, 2);
                cmbEndDay.Text = TodayPersian.Substring(8,2);


            }

            CalculateDate();



        }

        public void CalculateDate()
        {
            string s = "13" + cmbStartYear.Text + "/" + cmbStartMonth.Text + "/" + cmbStartDay.Text;
            string e = "13" + cmbEndYear.Text + "/" + cmbEndMonth.Text + "/" + cmbEndDay.Text;

            var t = CountShift(s, e);

            lblDate1.Text = t.ElementAt(0);
            lblDate2.Text = t.ElementAt(1);
            lblDate3.Text = t.ElementAt(2);
            lblDate4.Text = t.ElementAt(3);
            lblDate5.Text = t.ElementAt(4);
        }

        private void lblS1_Click(object sender, EventArgs e)
        {
            Selected(lblS1);
        }

        private void lblS2_Click(object sender, EventArgs e)
        {
            Selected(lblS2);
        }

        private void lblS3_Click(object sender, EventArgs e)
        {
            Selected(lblS3);
        }

        private void lblS4_Click(object sender, EventArgs e)
        {
            Selected(lblS4);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            person.Name = txtName.Text;
            person.Lastname = txtLastName.Text;
            person.NickName = txtNickName.Text;
            person.Level = Convert.ToInt16(cmbLevel.Text);

            db.SaveChanges();
            panel1.Visible = false;

            SetLabelStaff();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var per = db.People.Where(p => p.Id == SelectId).FirstOrDefault();
            per.IsDelete = true;


            DialogResult result3 = MessageBox.Show("Do you want to delete " + per.Name + " " + per.Lastname + "?",
           "The Question",
            MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);

            if (result3 == DialogResult.Yes)
            {


                var delShiftDays = db.ShiftDays.Where(p => p.Person_fk == per.Id & p.Date >= DateTime.Today);
                foreach (var item in delShiftDays)
                {
                    db.ShiftDays.Remove(item);
                }

                var delWeekDays = db.DailyWeeks.Where(p => p.Person_fk == per.Id);
                foreach (var itemm in delWeekDays)
                {
                    db.DailyWeeks.Remove(itemm);
                }

                db.SaveChanges();
                panel1.Visible = false;
                SetLabelStaff();
            }
            else if (result3 == DialogResult.No)
            {
                lblName.Text = "";
            }




        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            lblName.Text = "";
            btnAdd.Visible = true;

            btnDelete.Visible = false;
            btnEdit.Visible = false;
            ClearTextbox();
            panel3.Visible = false;
            panel1.Visible = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Person per = new Person();
            per.Name = txtName.Text;
            per.Lastname = txtLastName.Text;
            per.NickName = txtNickName.Text;
            per.Level = Convert.ToInt16(cmbLevel.Text);
            per.IsDelete = false;


            ClearTextbox();
            int x = 0;

            if (string.IsNullOrWhiteSpace(per.NickName))
            {
                lblName.Text = "Insert NickName";
                lblName.ForeColor = Color.DarkRed;
            }
            else
            {
                db.People.Add(per);
                int saved = db.SaveChanges();

                if (saved != 0)
                {
                    SetLabelStaff();

                    panel1.Visible = false;
                    lblName.Text = per.Name + " " + per.Lastname + "Added";
                    lblName.ForeColor = Color.DarkGreen;
                }
                else
                {
                    lblName.Text = "Warning";
                    lblName.ForeColor = Color.DarkRed;
                }
            }




        }

        public void ClearTextbox()
        {
            txtName.Text = "";
            txtLastName.Text = "";
            txtNickName.Text = "";
            cmbLevel.Text = "1";
        }

        private void lblS6_Click(object sender, EventArgs e)
        {
            Selected(lblS6);
        }

        private void lblS5_Click(object sender, EventArgs e)
        {
            Selected(lblS5);
        }

        private void lblS7_Click(object sender, EventArgs e)
        {
            Selected(lblS7);
        }

        private void lblS8_Click(object sender, EventArgs e)
        {
            Selected(lblS8);
        }

        private void lblS9_Click(object sender, EventArgs e)
        {
            Selected(lblS9);
        }

        private void lblS10_Click(object sender, EventArgs e)
        {
            Selected(lblS10);
        }

        private void lblS11_Click(object sender, EventArgs e)
        {
            Selected(lblS11);
        }

        private void lblS12_Click(object sender, EventArgs e)
        {
            Selected(lblS12);
        }

        private void lblS13_Click(object sender, EventArgs e)
        {
            Selected(lblS13);
        }

        private void lblS14_Click(object sender, EventArgs e)
        {
            Selected(lblS14);
        }

        private void lblS15_Click(object sender, EventArgs e)
        {
            Selected(lblS15);
        }

        private void lblS16_Click(object sender, EventArgs e)
        {
            Selected(lblS16);
        }

        private void lblS17_Click(object sender, EventArgs e)
        {
            Selected(lblS17);
        }

        private void lblS18_Click(object sender, EventArgs e)
        {
            Selected(lblS18);
        }

        private void lblS19_Click(object sender, EventArgs e)
        {
            Selected(lblS19);
        }

        private void lblS20_Click(object sender, EventArgs e)
        {
            Selected(lblS20);
        }

        private void cmbStartDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateDate();
        }

        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateDate();
            if (cmbStartMonth.Text.CompareTo("07") < 0)
            {
                if (cmbStartDay.Items.Contains("31"))
                {

                }
                else
                {
                    if (!cmbStartDay.Items.Contains("30"))
                    {
                        cmbStartDay.Items.Add("30");
                    }
                    cmbStartDay.Items.Add("31");
                }
            }
            else
            {
                if (cmbStartDay.Items.Contains("31"))
                {
                    cmbStartDay.Items.Remove("31");
                }
                else
                {

                }
                if (cmbStartMonth.Text.CompareTo("12") == 0)
                {
                    cmbStartDay.Items.Remove("30");
                }
                else
                {

                }

            }
        }

        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateDate();
        }

        private void cmbEndDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateDate();
        }

        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateDate();
            if (cmbEndMonth.Text.CompareTo("07") < 0)
            {
                if(cmbEndDay.Items.Contains("31"))
                {

                }
                else
                {
                    if (!cmbEndDay.Items.Contains("30"))
                    {
                        cmbEndDay.Items.Add("30");
                    }
                    cmbEndDay.Items.Add("31");
                }
            }
            else
            {
                if (cmbEndDay.Items.Contains("31"))
                {
                    cmbEndDay.Items.Remove("31");
                }
                else
                {
                  
                }
                if (cmbEndMonth.Text.CompareTo("12") == 0)
                {
                    cmbEndDay.Items.Remove("30");
                }
                else
                {

                }

            }

        }

        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateDate();
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            if(ChangePanel)
            {
                panel3.Visible = false;
                panel1.Visible = true;

                ChangePanel = false;
            }
            else
            {
                panel1.Visible = false;
                panel3.Visible = true;

                ChangePanel = true;
            }
        }
    }
}
