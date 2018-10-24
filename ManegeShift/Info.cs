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
        ManageShiftEntities1 db;
        List<Person> People;
        Person person;
        int SelectId = 0;
        public Info()
        {
        
            db = new ManageShiftEntities1();
  
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



        }

        public void SetLabelStaff()
        {
            People = db.People.Where(p=>p.IsDelete==false).OrderByDescending(p => p.Level).ThenBy(p=>p.Id).ToList();
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
        }

        public void Selected(Label lb)
        {
            int id = Convert.ToInt32(lb.Tag);
            SelectId = id;
            person = People.Where(p => p.Id == id).FirstOrDefault();
            txtName.Text = person.Name;
            txtLastName.Text = person.Lastname;
            txtNickName.Text = person.NickName.Trim();

          
                cmbLevel.Text = person.Level.ToString();


            btnAdd.Visible = false;
            btnDelete.Visible = true;
            btnEdit.Visible = true;

            panel1.Visible = true;


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
            var per =db.People.Where(p => p.Id == SelectId).FirstOrDefault();
            per.IsDelete = true;

            var delShiftDays = db.ShiftDays.Where(p => p.Person_fk == per.Id & p.Date >= DateTime.Today);
            foreach (var item in delShiftDays)
            {
                db.ShiftDays.Remove(item);
            }

            var delWeekDays = db.DailyWeeks.Where(p => p.Person_fk == per.Id );
            foreach (var itemm in delWeekDays)
            {
                db.DailyWeeks.Remove(itemm);
            }

            db.SaveChanges();
            panel1.Visible = false;
            SetLabelStaff();


        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

            btnAdd.Visible = true;

            btnDelete.Visible = false;
            btnEdit.Visible = false;
            ClearTextbox();

            panel1.Visible = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Person per = new Person();
            per.Name = txtName.Text;
            per.Lastname = txtLastName.Text;
            per.NickName = txtNickName.Text;
            per.Level =Convert.ToInt16( cmbLevel.Text);
            per.IsDelete = false;


            ClearTextbox();

            db.People.Add(per);
            db.SaveChanges();

            SetLabelStaff();

            panel1.Visible = false;

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
    }
}
