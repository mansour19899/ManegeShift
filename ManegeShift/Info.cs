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
        bool IsShowShift = false;

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

            Today = DateTime.Today;
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

            IsShowShift = true;
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
            string s = ("13" + cmbStartYear.Text + "/" + cmbStartMonth.Text + "/" + cmbStartDay.Text);
            string e = ("13" + cmbEndYear.Text + "/" + cmbEndMonth.Text + "/" + cmbEndDay.Text);

            var t = CountShift(s, e);

            lblDate1.Text = t.ElementAt(0);
            lblDate2.Text = t.ElementAt(1);
            lblDate3.Text = t.ElementAt(2);
            lblDate4.Text = t.ElementAt(3);
            lblDate5.Text = t.ElementAt(4);
        }

        public void ShowShift()
        {
          

            List<Label> FlistStaff = new List<Label>() {lbl1,lbl2,lbl3,lbl4,lbl5,lbl6,lbl7,lbl8,lbl9,lbl10,lbl11,lbl12,lbl13,lbl14,lbl15,lbl16,lbl17,lbl18,lbl19,lbl20 };
            List<Label> FlistMorning= new List<Label>() { lblM1, lblM2, lblM3, lblM4, lblM5, lblM6, lblM7, lblM8, lblM9, lblM10, lblM11, lblM12, lblM13, lblM14, lblM15, lblM16, lblM17, lblM18, lblM19, lblM20 };
            List<Label> FlistMid= new List<Label>() { lblMi1, lblMi2, lblMi3, lblMi4, lblMi5, lblMi6, lblMi7, lblMi8, lblMi9, lblMi10, lblMi11, lblMi12, lblMi13, lblMi14, lblMi15, lblMi16, lblMi17, lblMi18, lblMi19, lblMi20 };
            List<Label> FlistEvening = new List<Label>() { lblE1, lblE2, lblE3, lblE4, lblE5, lblE6, lblE7, lblE8, lblE9, lblE10, lblE11, lblE12, lblE13, lblE14, lblE15, lblE16, lblE17, lblE18, lblE19, lblE20 };
            List<Label> FlistSpilt= new List<Label>() { lblSp1, lblSp2, lblSp3, lblSp4, lblSp5, lblSp6, lblSp7, lblSp8, lblSp9, lblSp10, lblSp11, lblSp12, lblSp13, lblSp14, lblSp15, lblSp16, lblSp17, lblSp18, lblSp19, lblSp20 };
            List<Label> FlistRest= new List<Label>() { lblR1, lblR2, lblR3, lblR4, lblR5, lblR6, lblR7, lblR8, lblR9, lblR10, lblR11, lblR12, lblR13, lblR14, lblR15, lblR16, lblR17, lblR18, lblR19, lblR20 };

            string s = "13" + cmbStartYear.Text + "/" + cmbStartMonth.Text + "/" + cmbStartDay.Text;
            string e = "13" + cmbEndYear.Text + "/" + cmbEndMonth.Text + "/" + cmbEndDay.Text;

            List<string> t=new List<string>();
            int i = 0;
            foreach (var item in People)
            {
                SelectId = item.Id;
                t.Clear();
                t = CountShift(s, e);

                FlistStaff.ElementAt(i).Text = item.NickName.Trim();
                FlistMorning.ElementAt(i).Text = t.ElementAt(0);
                FlistMid.ElementAt(i).Text = t.ElementAt(1);
                FlistEvening.ElementAt(i).Text = t.ElementAt(2);
                FlistSpilt.ElementAt(i).Text = t.ElementAt(3);
                FlistRest.ElementAt(i).Text = t.ElementAt(4);
                i = i + 1;
            }

            for (int j= i; i < 20; i++)
            {
                FlistStaff.ElementAt(i).Visible = false;
                FlistMorning.ElementAt(i).Visible = false;
                FlistMid.ElementAt(i).Visible = false;
                FlistEvening.ElementAt(i).Visible = false;
                FlistSpilt.ElementAt(i).Visible = false;
                FlistRest.ElementAt(i).Visible = false;

            }

            lblDateToDate.Text = s + "---------" + e;
            panel4.Visible = true;

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

        private void label10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if(IsShowShift)
            {
                lblNameStatus.Text = "";
                listView1.Visible = false;
                ShowShift();
            }

        }

        private void label22_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void label23_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        public void ShowDetailsShift(int id,int status)
        {
            var Id = People.ElementAt(id).Id;
            DateTime s = lblDateToDate.Text.Split('-')[0].ToGeorgianDateTime();
            DateTime e = lblDateToDate.Text.Split('-')[9].ToGeorgianDateTime();
            string statusname = "";
            switch (status)
            {
                case 1:
                    statusname = "(Morning)";
                    break;
                case 2:
                    statusname = "(Mid)";
                    break;
                case 3:
                    statusname = "(Evening)";
                    break;
                case 4:
                    statusname = "(Split)";
                    break;
                case 5:
                    statusname = "(Rest)";
                    break;

                default:
                    break;
            }

            lblNameStatus.Text = People.ElementAt(id).NickName.Trim()+"\n"+statusname;
            var list = db.ShiftDays.Where(p => p.Person_fk == Id & p.Date >= s & p.Date <= e & p.Status_fk == status).ToList();
            listView1.Clear();
            foreach (var item in list)
            {
                listView1.Items.Add(item.Date.ToPersianDateString());
            }

            listView1.Visible = true;

        }



        private void lblM1_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(0,1);
        }

        private void lblMi1_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(0, 2);
        }

        private void lblE1_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(0, 3);
        }

        private void lblSp1_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(0, 4);
        }

        private void lblR1_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(0, 5);
        }

        private void lblNameStatus_Click(object sender, EventArgs e)
        {
            lblNameStatus.Text = "";
            listView1.Visible = false;
        }

        private void lblM2_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(1, 1);
        }

        private void lblMi2_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(1, 2);
        }

        private void lblE2_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(1, 3);
        }

        private void lblSp2_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(1, 4);
        }

        private void lblR2_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(1, 5);
        }

        private void lblM3_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(2, 1);
        }

        private void lblMi3_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(2,2 );
        }

        private void lblE3_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(2,3 );
        }

        private void lblSp3_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(2,4 );
        }

        private void lblR3_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(2,5 );
        }

        private void lblM4_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(3, 1);
        }

        private void lblMi4_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(3, 2);
        }

        private void lblE4_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(3, 3);
        }

        private void lblSp4_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(3, 4);
        }

        private void lblR4_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(3, 5);
        }

        private void lblM5_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(4, 1);
        }

        private void lblMi5_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(4, 2);
        }

        private void lblE5_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(4,3 );
        }

        private void lblSp5_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(4,4 );
        }

        private void lblR5_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(4, 5);
        }

        private void lblM6_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(5,1 );
        }

        private void lblMi6_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(5, 2);
        }

        private void lblE6_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(5, 3);
        }

        private void lblSp6_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(5, 4);
        }

        private void lblR6_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(5, 5);
        }

        private void lblR6_Click_1(object sender, EventArgs e)
        {
            ShowDetailsShift(5, 5);
        }

        private void lblM7_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(6, 1);
        }

        private void lblMi7_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(6, 2);
        }

        private void lblE7_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(6, 3);
        }

        private void lblSp7_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(6, 4);
        }

        private void lblR7_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(6, 5);
        }

        private void lblM8_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(7, 1);
        }

        private void lblMi8_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(7, 2);
        }

        private void lblE8_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(7, 3);
        }

        private void lblSp8_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(7, 4);
        }

        private void lblR8_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(7, 5);
        }

        private void lblM9_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(8, 1);
        }

        private void lblMi9_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(8, 2);
        }

        private void lblE9_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(8, 3);
        }

        private void lblSp9_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(8, 4);
        }

        private void lblR9_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(8, 5);
        }

        private void lblM10_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(9, 1);
        }

        private void lblMi10_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(9, 2);
        }

        private void lblE10_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(9, 3);
        }

        private void lblSp10_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(9, 4);
        }

        private void lblR10_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(9, 5);
        }

        private void lblM11_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(10, 1);
        }

        private void lblMi11_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(10, 2);
        }

        private void lblE11_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(10, 3);
        }

        private void lblSp11_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(10, 4);
        }

        private void lblR11_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(10, 5);
        }

        private void lblM12_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(11, 1);
        }

        private void lblMi12_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(11, 2);
        }

        private void lblE12_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(11, 3);
        }

        private void lblSp12_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(11, 4);
        }

        private void lblR12_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(11, 5);
        }

        private void lblM13_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(12, 1);
        }

        private void lblMi13_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(12, 2);
        }

        private void lblE13_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(12, 3);
        }

        private void lblSp13_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(12, 4);
        }

        private void lblR13_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(12, 5);
        }

        private void lblM14_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(13, 1);
        }

        private void lblMi14_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(13, 2);
        }

        private void lblE14_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(13, 3);
        }

        private void lblSp14_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(13, 4);
        }

        private void lblR14_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(13, 5);
        }

        private void lblM15_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(14, 1);
        }

        private void lblMi15_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(14, 2);
        }

        private void lblE15_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(14, 3);
        }

        private void lblSp15_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(14, 4);
        }

        private void lblR15_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(14, 5);
        }

        private void lblM16_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(15, 1);
        }

        private void lblMi16_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(15, 2);
        }

        private void lblE16_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(15, 3);
        }

        private void lblSp16_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(15, 4);
        }

        private void lblR16_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(15, 5);
        }

        private void lblM17_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(16, 1);
        }

        private void lblMi17_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(16, 2);
        }

        private void lblE17_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(16,3);
        }

        private void lblSp17_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(16, 4);
        }

        private void lblR17_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(16, 5);
        }

        private void lblM18_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(17, 1);
        }

        private void lblMi18_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(17, 2);
        }

        private void lblE18_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(17, 3);
        }

        private void lblSp18_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(17, 4);
        }

        private void lblR18_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(17, 5);
        }

        private void lblM19_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(18, 1);
        }

        private void lblMi19_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(18, 2);
        }

        private void lblE19_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(18, 3);
        }

        private void lblSp19_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(18, 4);
        }

        private void lblR19_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(18, 5);
        }

        private void lblM20_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(19, 1);
        }

        private void lblMi20_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(19, 2);
        }

        private void lblE20_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(19, 3);
        }

        private void lblSp20_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(19, 4);
        }

        private void lblR20_Click(object sender, EventArgs e)
        {
            ShowDetailsShift(19, 5);
        }

     
    }
}
