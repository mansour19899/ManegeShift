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
    public partial class Form1 : Form
    {
        int Day = 0;
        DateTime Date;
        bool IsDate = false;
        HiiiEntities db;
        List<DailyWeek> dbb;

        public Form1()
        {
            InitializeComponent();
        }
        public Form1(int day)
        {
            Day = day;
            db = new HiiiEntities();
            InitializeComponent();
        }
        public Form1(DateTime date)
        {
            Date = date;
            IsDate = true;
            db = new HiiiEntities();
            InitializeComponent();
        }

        public Form1(DateTime date,bool clean)
        {
            Date = date;
            IsDate = !clean;
            db = new HiiiEntities();
            InitializeComponent();
        }

        List<Staff> staffs;
        List<Staff> MorningShift;
        List<Staff> MidShift;
        List<Staff> EveningShift;
        List<Staff> SpiltShift;
        List<Staff> Rest;


        List<Label> labelsStaff;
        List<Label> labelsMorning;
        List<Label> labelsMid;
        List<Label> labelsEvening;
        List<Label> labelsSplit;
        List<Label> labelsRest;


        int IdSelected = 0;
        int StatusSelected = 0;


        private void Form1_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;

            MorningShift = new List<Staff>();
            MidShift = new List<Staff>();
            EveningShift = new List<Staff>();
            SpiltShift = new List<Staff>();
            Rest = new List<Staff>();




            SetList();

            labelsStaff = new List<Label>
            {
                lblS1,lblS2,lblS3,lblS4,lblS5,lblS6,lblS7,lblS8,lblS9,lblS10,lblS11
                ,lblS12,lblS13,lblS14,lblS15,lblS16,lblS17,lblS18,lblS19,lblS20
            };

            labelsMorning = new List<Label>
            {
                lblM1,lblM2,lblM3,lblM4,lblM5,lblM6,lblM7,lblM8,lblM9
            };

            labelsMid = new List<Label>
            {
                lblMi1,lblMi2,lblMi3,lblMi4,lblMi5,lblMi6,lblMi7,lblMi8,lblMi9
            };

            labelsEvening = new List<Label>
            {
                lblE1,lblE2,lblE3,lblE4,lblE5,lblE6,lblE7,lblE8,lblE9
            };

            labelsSplit = new List<Label>
            {
                lblSp1,lblSp2,lblSp3,lblSp4,lblSp5,lblSp6,lblSp7,lblSp8,lblSp9
            };
            labelsRest = new List<Label>
            {
                lblR1,lblR2,lblR3,lblR4,lblR5,lblR6,lblR7,lblR8,lblR9
            };

            SetLabels(staffs, 0);
            SetLabels(MorningShift, 1);
            SetLabels(MidShift, 2);
            SetLabels(EveningShift, 3);
            SetLabels(SpiltShift, 4);
            SetLabels(Rest, 5);

            if (staffs.Count() < 10)
                panel2.AutoScroll = true;

            switch (Day)
            {
                case 1:
                    lblDayName.Text = "Saturday";
                    break;
                case 2:
                    lblDayName.Text = "Sunday";
                    break;
                case 3:
                    lblDayName.Text = "Monday";
                    break;
                case 4:
                    lblDayName.Text = "Tuesday";
                    break;
                case 5:
                    lblDayName.Text = "Wednesday";
                    break;
                case 6:
                    lblDayName.Text = "Thursday";
                    break;
                case 7:
                    lblDayName.Text = "Friday";
                    break;



                default:
                    lblDayName.Text = "";
                    break;

            }
  
            lblDayName.Text = Date.DayOfWeek + "\n" + Date.ToPersianDateString();
            var ISExistShift = db.ShiftDays.Any(p => p.Date == Date);
            if (ISExistShift)
                lblDayName.ForeColor = Color.DarkRed;
            else
                lblDayName.ForeColor = Color.DarkGreen;
        }

        public void SetList()
        {
            if (IsDate)
            {
                var dbb = db.ShiftDays.Where(p => p.Date == Date).ToList();
                var staffss = db.People.Where(p=>p.IsDelete==false).Select(p => new Staff { Id = p.Id, Name = p.NickName.Trim(),Level=p.Level}).ToList();
                MorningShift = dbb.Where(p => p.Status_fk == 1).Select(p => new Staff { Id = p.Person_fk, Name = p.Person.NickName.Trim(),Level=p.Person.Level}).ToList();
                MidShift = dbb.Where(p => p.Status_fk == 2).Select(p => new Staff { Id = p.Person_fk, Name = p.Person.NickName.Trim() + "(" + p.mid.Trim() + ")",Level=p.Person.Level }).ToList();
                EveningShift = dbb.Where(p => p.Status_fk == 3).Select(p => new Staff { Id = p.Person_fk, Name = p.Person.NickName.Trim(), Level = p.Person.Level }).ToList();
                SpiltShift = dbb.Where(p => p.Status_fk == 4).Select(p => new Staff { Id = p.Person_fk, Name = p.Person.NickName.Trim(),Level=p.Person.Level }).ToList();
                Rest = dbb.Where(p => p.Status_fk == 5).Select(p => new Staff { Id = p.Person_fk, Name = p.Person.NickName.Trim(),Level=p.Person.Level }).ToList();

                staffs = staffss.Where(p => !MorningShift.Any(p2 => p2.Id == p.Id) & !MidShift.Any(p2 => p2.Id == p.Id) & !EveningShift.Any(p2 => p2.Id == p.Id)
                & !SpiltShift.Any(p2 => p2.Id == p.Id) & !Rest.Any(p2 => p2.Id == p.Id)).ToList();

              
               
            }
            else
            {
                 staffs = db.People.Where(p=>p.IsDelete==false).Select(p => new Staff { Id = p.Id, Name = p.NickName.Trim(),Level=p.Level }).ToList();

                int x = 0;
                

            }




          
        }
        public void SetLabels(List<Staff> ss, int status)
        {
            var s = ss.OrderByDescending(p => p.Level).ThenBy(p=>p.Id).ToList();
            int i = 0;
            switch (status)
            {
                case 0:
                    foreach (var item in s)
                    {
                        labelsStaff.ElementAt(i).Text = item.Name;
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
                    lblCountStaff.Text = "(" + staffs.Count().ToString() + ")";
                    break;

                case 1:
                    foreach (var item in s)
                    {
                        labelsMorning.ElementAt(i).Text = item.Name;
                        labelsMorning.ElementAt(i).Tag = item.Id.ToString();
                        labelsMorning.ElementAt(i).Visible = true;
                        i = i + 1;
                    }

                    for (int j = i; j < 9; j++)
                    {
                        labelsMorning.ElementAt(j).Text = "";
                        labelsMorning.ElementAt(j).Tag = "";
                        labelsMorning.ElementAt(j).Visible = false;
                    }
                    lblCountMorning.Text = "(" + MorningShift.Count().ToString() + ")";
                    break;

                case 2:
                    foreach (var item in s)
                    {
                        labelsMid.ElementAt(i).Text = item.Name;
                        labelsMid.ElementAt(i).Tag = item.Id.ToString();
                        labelsMid.ElementAt(i).Visible = true;
                        i = i + 1;
                    }

                    for (int j = i; j < 9; j++)
                    {
                        labelsMid.ElementAt(j).Text = "";
                        labelsMid.ElementAt(j).Tag = "";
                        labelsMid.ElementAt(j).Visible = false;
                    }
                    lblCountMid.Text = "(" + MidShift.Count().ToString() + ")";
                    break;

                case 3:
                    foreach (var item in s)
                    {
                        labelsEvening.ElementAt(i).Text = item.Name;
                        labelsEvening.ElementAt(i).Tag = item.Id.ToString();
                        labelsEvening.ElementAt(i).Visible = true;
                        i = i + 1;
                    }

                    for (int j = i; j < 9; j++)
                    {
                        labelsEvening.ElementAt(j).Text = "";
                        labelsEvening.ElementAt(j).Tag = "";
                        labelsEvening.ElementAt(j).Visible = false;
                    }
                    lblCountEvening.Text = "(" + EveningShift.Count().ToString() + ")";
                    break;
                case 4:
                    foreach (var item in s)
                    {
                        labelsSplit.ElementAt(i).Text = item.Name;
                        labelsSplit.ElementAt(i).Tag = item.Id.ToString();
                        labelsSplit.ElementAt(i).Visible = true;
                        i = i + 1;
                    }

                    for (int j = i; j < 9; j++)
                    {
                        labelsSplit.ElementAt(j).Text = "";
                        labelsSplit.ElementAt(j).Tag = "";
                        labelsSplit.ElementAt(j).Visible = false;
                    }
                    lblCountSplit.Text = "(" + SpiltShift.Count().ToString() + ")";
                    break;

                case 5:
                    foreach (var item in s)
                    {
                        labelsRest.ElementAt(i).Text = item.Name;
                        labelsRest.ElementAt(i).Tag = item.Id.ToString();
                        labelsRest.ElementAt(i).Visible = true;
                        i = i + 1;
                    }

                    for (int j = i; j < 9; j++)
                    {
                        labelsRest.ElementAt(j).Text = "";
                        labelsRest.ElementAt(j).Tag = "";
                        labelsRest.ElementAt(j).Visible = false;
                    }
                    lblCountRest.Text = "(" + Rest.Count().ToString() + ")";
                    break;

                default:
                    break;
            }
        }

        private void btnMorning_Click(object sender, EventArgs e)
        {

            switch (StatusSelected)
            {
                case 0:
                    var s = staffs.Where(p => p.Id == IdSelected).FirstOrDefault();
                    s.Status = 1;
                    staffs.Remove(s);
                    MorningShift.Add(s);

                    SetLabels(staffs, 0);
                    SetLabels(MorningShift, 1);
                    break;
                case 1:
                    break;
                case 2:
                    var ss = MidShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ss.Status = 1;
                    MidShift.Remove(ss);
                    ss.Name = ss.Name.Split('(')[0];
                    MorningShift.Add(ss);

                    SetLabels(MidShift, 2);
                    SetLabels(MorningShift, 1);
                    break;
                case 3:
                    var sss = EveningShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sss.Status = 1;
                    EveningShift.Remove(sss);
                    MorningShift.Add(sss);

                    SetLabels(EveningShift, 3);
                    SetLabels(MorningShift, 1);
                    break;

                case 4:
                    var ssss = SpiltShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ssss.Status = 1;
                    SpiltShift.Remove(ssss);
                    MorningShift.Add(ssss);

                    SetLabels(SpiltShift, 4);
                    SetLabels(MorningShift, 1);
                    break;

                case 5:
                    var sssss = Rest.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sssss.Status = 1;
                    Rest.Remove(sssss);
                    MorningShift.Add(sssss);

                    SetLabels(Rest, 5);
                    SetLabels(MorningShift, 1);
                    break;

                default:
                    break;
            }


            panel3.Visible = false;

        }

        private void btnMid_Click(object sender, EventArgs e)
        {
            string FromMinte = "";
            string Tominte = "";

            if (cmbTimeFromMinte.Text == "00")
            { }
            else
                FromMinte = ":" + cmbTimeFromMinte.Text;

            if (cmbTimeToMinte.Text == "00")
            { }
            else
                Tominte = ":" + cmbTimeToMinte.Text;

            string time = "(" + cmbTimeFromHour.Text + FromMinte + "-" + cmbTimeToHour.Text + Tominte + ")";

            switch (StatusSelected)
            {
                case 0:
                    var s = staffs.Where(p => p.Id == IdSelected).FirstOrDefault();
                    s.Status = 2;
                    staffs.Remove(s);
                    s.Name = s.Name + time;
                    MidShift.Add(s);

                    SetLabels(staffs, 0);
                    SetLabels(MidShift, 2);
                    break;
                case 1:
                    var ss = MorningShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ss.Status = 2;
                    MorningShift.Remove(ss);
                    ss.Name = ss.Name + time;
                    MidShift.Add(ss);

                    SetLabels(MorningShift, 1);
                    SetLabels(MidShift, 2);
                    break;
                case 2:
                    var sa = MidShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sa.Name = sa.Name = sa.Name.Split('(')[0] + time;
                    SetLabels(MidShift, 2);

                    break;
                case 3:
                    var sss = EveningShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sss.Status = 2;
                    EveningShift.Remove(sss);
                    sss.Name = sss.Name + time;
                    MidShift.Add(sss);

                    SetLabels(EveningShift, 3);
                    SetLabels(MidShift, 2);
                    break;
                case 4:
                    var ssss = SpiltShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ssss.Status = 2;
                    SpiltShift.Remove(ssss);
                    ssss.Name = ssss.Name + time;
                    MidShift.Add(ssss);

                    SetLabels(SpiltShift, 4);
                    SetLabels(MidShift, 2);
                    break;

                case 5:
                    var sssss = Rest.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sssss.Status = 2;
                    Rest.Remove(sssss);
                    sssss.Name = sssss.Name + time;
                    MidShift.Add(sssss);

                    SetLabels(Rest, 5);
                    SetLabels(MidShift, 2);
                    break;

                default:
                    break;
            }

            panel3.Visible = false;

        }

        private void btnEvening_Click(object sender, EventArgs e)
        {
            switch (StatusSelected)
            {
                case 0:
                    var s = staffs.Where(p => p.Id == IdSelected).FirstOrDefault();
                    s.Status = 3;
                    staffs.Remove(s);
                    EveningShift.Add(s);

                    SetLabels(staffs, 0);
                    SetLabels(EveningShift, 3);
                    break;
                case 1:
                    var ss = MorningShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ss.Status = 3;
                    MorningShift.Remove(ss);
                    EveningShift.Add(ss);

                    SetLabels(MorningShift, 1);
                    SetLabels(EveningShift, 3);
                    break;
                case 2:
                    var sss = MidShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sss.Status = 3;
                    MidShift.Remove(sss);
                    sss.Name = sss.Name.Split('(')[0];
                    EveningShift.Add(sss);

                    SetLabels(MidShift, 2);
                    SetLabels(EveningShift, 3);
                    break;
                case 3:

                    break;
                case 4:
                    var ssss = SpiltShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ssss.Status = 3;
                    SpiltShift.Remove(ssss);
                    EveningShift.Add(ssss);

                    SetLabels(SpiltShift, 4);
                    SetLabels(EveningShift, 3);
                    break;

                case 5:
                    var sssss = Rest.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sssss.Status = 3;
                    Rest.Remove(sssss);
                    EveningShift.Add(sssss);

                    SetLabels(Rest, 5);
                    SetLabels(EveningShift, 3);
                    break;

                default:
                    break;
            }

            panel3.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (StatusSelected)
            {
                case 0:
                    var s = staffs.Where(p => p.Id == IdSelected).FirstOrDefault();
                    s.Status = 4;
                    staffs.Remove(s);
                    SpiltShift.Add(s);

                    SetLabels(staffs, 0);
                    SetLabels(SpiltShift, 4);
                    break;
                case 1:
                    var ss = MorningShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ss.Status = 4;
                    MorningShift.Remove(ss);
                    SpiltShift.Add(ss);

                    SetLabels(MorningShift, 1);
                    SetLabels(SpiltShift, 4);
                    break;
                case 2:
                    var sss = MidShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sss.Status = 4;
                    MidShift.Remove(sss);
                    sss.Name = sss.Name.Split('(')[0];
                    SpiltShift.Add(sss);

                    SetLabels(MidShift, 2);
                    SetLabels(SpiltShift, 4);
                    break;

                case 3:
                    var ssss = EveningShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ssss.Status = 4;
                    EveningShift.Remove(ssss);
                    SpiltShift.Add(ssss);

                    SetLabels(EveningShift, 3);
                    SetLabels(SpiltShift, 4);
                    break;

                case 4:

                    break;
                case 5:
                    var sssss = Rest.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sssss.Status = 4;
                    Rest.Remove(sssss);
                    SpiltShift.Add(sssss);

                    SetLabels(Rest, 5);
                    SetLabels(SpiltShift, 4);
                    break;

                default:
                    break;
            }
            panel3.Visible = false;
        }

        private void btnRest_Click(object sender, EventArgs e)
        {
            switch (StatusSelected)
            {
                case 0:
                    var s = staffs.Where(p => p.Id == IdSelected).FirstOrDefault();
                    s.Status = 5;
                    staffs.Remove(s);
                    Rest.Add(s);

                    SetLabels(staffs, 0);
                    SetLabels(Rest, 5);
                    break;

                case 1:
                    var ss = MorningShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ss.Status = 5;
                    MorningShift.Remove(ss);
                    Rest.Add(ss);

                    SetLabels(MorningShift, 1);
                    SetLabels(Rest, 5);
                    break;
                case 2:
                    var sss = MidShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sss.Status = 5;
                    MidShift.Remove(sss);
                    sss.Name = sss.Name.Split('(')[0];
                    Rest.Add(sss);

                    SetLabels(MidShift, 2);
                    SetLabels(Rest, 5);
                    break;

                case 3:
                    var ssss = EveningShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ssss.Status = 5;
                    EveningShift.Remove(ssss);
                    Rest.Add(ssss);

                    SetLabels(EveningShift, 3);
                    SetLabels(Rest, 5);
                    break;
                case 4:
                    var sa = SpiltShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sa.Status = 5;
                    SpiltShift.Remove(sa);
                    Rest.Add(sa);

                    SetLabels(SpiltShift, 4);
                    SetLabels(Rest, 5);
                    break;

                case 5:

                    break;


                default:
                    break;
            }
            panel3.Visible = false;
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            switch (StatusSelected)
            {
                case 0:

                    break;

                case 1:
                    var ss = MorningShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ss.Status = 0;
                    MorningShift.Remove(ss);
                    staffs.Add(ss);

                    SetLabels(MorningShift, 1);
                    SetLabels(staffs, 0);
                    break;
                case 2:
                    var sss = MidShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sss.Status = 0;
                    MidShift.Remove(sss);
                    sss.Name = sss.Name.Split('(')[0];
                    staffs.Add(sss);

                    SetLabels(MidShift, 2);
                    SetLabels(staffs, 0);
                    break;

                case 3:
                    var ssss = EveningShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    ssss.Status = 0;
                    EveningShift.Remove(ssss);
                    staffs.Add(ssss);

                    SetLabels(EveningShift, 3);
                    SetLabels(staffs, 0);
                    break;
                case 4:
                    var sa = SpiltShift.Where(p => p.Id == IdSelected).FirstOrDefault();
                    sa.Status =0;
                    SpiltShift.Remove(sa);
                    staffs.Add(sa);

                    SetLabels(SpiltShift, 4);
                    SetLabels(staffs, 0);
                    break;

                case 5:
                    var s = staffs.Where(p => p.Id == IdSelected).FirstOrDefault();
                    s.Status = 0;
                    Rest.Remove(s);
                    staffs.Add(s);

                  
                    SetLabels(Rest, 5);
                      SetLabels(staffs, 0);
                    break;


                default:
                    break;
            }
            panel3.Visible = false;
        }

        public void SelectStaff(Label label, int Status)
        {

            lblSelectedStaff.Text = label.Text.Split('(')[0];
            IdSelected = Convert.ToInt16(label.Tag.ToString());
            StatusSelected = Status;
            //if(!IsDate)
            //{

            //    panel1.Visible = false;
            //}
            panel3.Visible = true;

        }

        private void lblS1_Click(object sender, EventArgs e)
        {
            SelectStaff(lblS1, 0);
        }

        private void lblS2_Click(object sender, EventArgs e)
        {
            SelectStaff(lblS2, 0);
        }

        private void lblS3_Click(object sender, EventArgs e)
        {
            SelectStaff(lblS3, 0);
        }

        private void lblS4_Click(object sender, EventArgs e)
        {
            SelectStaff(lblS4, 0);
        }

        private void lblS5_Click(object sender, EventArgs e)
        {
            SelectStaff(lblS5, 0);
        }

        private void lblS6_Click(object sender, EventArgs e)
        {
            SelectStaff(lblS6, 0);
        }

        private void lblS7_Click(object sender, EventArgs e)
        {
            SelectStaff(lblS7, 0);
        }

        private void lblS8_Click(object sender, EventArgs e)
        {
            SelectStaff(lblS8, 0);
        }

        private void lblS9_Click(object sender, EventArgs e)
        {
            SelectStaff(lblS9, 0);
        }

        private void lblM1_Click(object sender, EventArgs e)
        {
            SelectStaff(lblM1, 1);
        }

        private void lblM2_Click(object sender, EventArgs e)
        {
            SelectStaff(lblM2, 1);
        }

        private void lblM3_Click(object sender, EventArgs e)
        {
            SelectStaff(lblM3, 1);
        }

        private void lblM4_Click(object sender, EventArgs e)
        {
            SelectStaff(lblM4, 1);
        }

        private void lblM5_Click(object sender, EventArgs e)
        {
            SelectStaff(lblM5, 1);
        }

        private void lblM6_Click(object sender, EventArgs e)
        {
            SelectStaff(lblM6, 1);
        }

        private void lblM7_Click(object sender, EventArgs e)
        {
            SelectStaff(lblM7, 1);
        }

        private void lblM8_Click(object sender, EventArgs e)
        {
            SelectStaff(lblM8, 1);
        }

        private void lblM9_Click(object sender, EventArgs e)
        {
            SelectStaff(lblM9, 1);
        }

        private void lblMi1_Click(object sender, EventArgs e)
        {
            SelectStaff(lblMi1, 2);
        }
        private void lblMi2_Click(object sender, EventArgs e)
        {
            SelectStaff(lblMi2, 2);
        }

        private void lblMi3_Click(object sender, EventArgs e)
        {
            SelectStaff(lblMi3, 2);
        }

        private void lblMi4_Click(object sender, EventArgs e)
        {
            SelectStaff(lblMi4, 2);
        }

        private void lblMi5_Click(object sender, EventArgs e)
        {
            SelectStaff(lblMi5, 2);
        }

        private void lblMi6_Click(object sender, EventArgs e)
        {
            SelectStaff(lblMi6, 2);
        }

        private void lblMi7_Click(object sender, EventArgs e)
        {
            SelectStaff(lblMi7, 2);
        }

        private void lblMi8_Click(object sender, EventArgs e)
        {
            SelectStaff(lblMi8, 2);
        }

        private void lblMi9_Click(object sender, EventArgs e)
        {
            SelectStaff(lblMi9, 2);
        }

        private void lblE1_Click(object sender, EventArgs e)
        {
            SelectStaff(lblE1, 3);
        }

        private void lblE2_Click(object sender, EventArgs e)
        {
            SelectStaff(lblE2, 3);
        }

        private void lblE3_Click(object sender, EventArgs e)
        {
            SelectStaff(lblE3, 3);
        }

        private void lblE4_Click(object sender, EventArgs e)
        {
            SelectStaff(lblE4, 3);
        }

        private void lblE5_Click(object sender, EventArgs e)
        {
            SelectStaff(lblE5, 3);
        }

        private void lblE6_Click(object sender, EventArgs e)
        {
            SelectStaff(lblE6, 3);
        }

        private void lblE7_Click(object sender, EventArgs e)
        {
            SelectStaff(lblE7, 3);
        }

        private void lblE8_Click(object sender, EventArgs e)
        {
            SelectStaff(lblE8, 3);
        }

        private void lblE9_Click(object sender, EventArgs e)
        {
            SelectStaff(lblE9, 3);
        }

        private void lblSp1_Click(object sender, EventArgs e)
        {
            SelectStaff(lblSp1, 4);
        }

        private void lblSp2_Click(object sender, EventArgs e)
        {
            SelectStaff(lblSp2, 4);
        }

        private void lblSp3_Click(object sender, EventArgs e)
        {
            SelectStaff(lblSp3, 4);
        }

        private void lblSp4_Click(object sender, EventArgs e)
        {
            SelectStaff(lblSp4, 4);
        }

        private void lblSp5_Click(object sender, EventArgs e)
        {
            SelectStaff(lblSp5, 4);
        }

        private void lblSp6_Click(object sender, EventArgs e)
        {
            SelectStaff(lblSp6, 4);
        }

        private void lblSp7_Click(object sender, EventArgs e)
        {
            SelectStaff(lblSp7, 4);
        }

        private void lblSp8_Click(object sender, EventArgs e)
        {
            SelectStaff(lblSp8, 4);
        }

        private void lblSp9_Click(object sender, EventArgs e)
        {
            SelectStaff(lblSp9, 4);
        }

        private void lblR1_Click(object sender, EventArgs e)
        {
            SelectStaff(lblR1, 5);
        }

        private void lblR2_Click(object sender, EventArgs e)
        {
            SelectStaff(lblR2, 5);
        }

        private void lblR3_Click(object sender, EventArgs e)
        {
            SelectStaff(lblR3, 5);
        }

        private void lblR4_Click(object sender, EventArgs e)
        {
            SelectStaff(lblR4, 5);
        }

        private void lblR5_Click(object sender, EventArgs e)
        {
            SelectStaff(lblR5, 5);
        }

        private void lblR6_Click(object sender, EventArgs e)
        {
            SelectStaff(lblR6, 5);
        }

        private void lblR7_Click(object sender, EventArgs e)
        {
            SelectStaff(lblR7, 5);
        }

        private void lblR8_Click(object sender, EventArgs e)
        {
            SelectStaff(lblR8, 5);
        }

        private void lblR9_Click(object sender, EventArgs e)
        {
            SelectStaff(lblR9, 5);
        }

        private void lblSelectedStaff_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void lblDayName_Click(object sender, EventArgs e)
        {
            string mid = "";
            lblDayName.Enabled = false;
            var eee = new DailyWeek();
            var fff = new ShiftDay();
            if(true)
            {
                foreach (var item in staffs)
                {
                    fff = db.ShiftDays.Where(u => u.Person_fk == item.Id & u.Date == Date).FirstOrDefault();
                    if (fff == null)
                    {
                     
                    }
                    else
                        db.ShiftDays.Remove(fff);
                }
                foreach (var item in MorningShift)
                {
                    fff = db.ShiftDays.Where(u => u.Person_fk == item.Id & u.Date == Date).FirstOrDefault();
                    if (fff == null)
                    {
                        db.ShiftDays.Add(new ShiftDay { Date = Date, Person_fk = item.Id, Status_fk = 1 });
                    }
                    else
                        fff.Status_fk = 1;
                }

                foreach (var item in MidShift)
                {
                    fff = db.ShiftDays.Where(u => u.Person_fk == item.Id & u.Date == Date).FirstOrDefault();
                    if (fff == null)
                    {
                        mid = item.Name.Split('(')[1].Split(')')[0];
                        db.ShiftDays.Add(new ShiftDay { Date = Date, Person_fk = item.Id, Status_fk = 2, mid = mid });
                    }
                    else
                    {
                        mid = item.Name.Split('(')[1].Split(')')[0];
                        fff.Status_fk = 2;
                        fff.mid = mid;
                    }





                }

                foreach (var item in EveningShift)
                {
                    fff = db.ShiftDays.Where(u => u.Person_fk == item.Id & u.Date == Date).FirstOrDefault();
                    if (fff == null)
                    {
                        db.ShiftDays.Add(new ShiftDay { Date = Date, Person_fk = item.Id, Status_fk = 3 });
                    }
                    else
                        fff.Status_fk = 3;

                }
                foreach (var item in SpiltShift)
                {
                    fff = db.ShiftDays.Where(u => u.Person_fk == item.Id & u.Date == Date).FirstOrDefault();
                    if (fff == null)
                    {
                        db.ShiftDays.Add(new ShiftDay { Date = Date, Person_fk = item.Id, Status_fk = 4 });
                    }
                    else
                        fff.Status_fk = 4;

                }
                foreach (var item in Rest)
                {
                    fff = db.ShiftDays.Where(u => u.Person_fk == item.Id & u.Date == Date).FirstOrDefault();
                    if (fff == null)
                    {
                        db.ShiftDays.Add(new ShiftDay { Date = Date, Person_fk = item.Id, Status_fk = 5 });
                    }
                    else
                        fff.Status_fk = 5;

                }

            }



            db.SaveChanges();
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
