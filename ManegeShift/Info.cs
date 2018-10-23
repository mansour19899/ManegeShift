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
            InitializeComponent();
            db = new ManageShiftEntities1();
        }

        private void Info_Load(object sender, EventArgs e)
        {

            labelsStaff = new List<Label>
            {
                lblS1,lblS2,lblS3,lblS4,lblS5,lblS6,lblS7,lblS8,lblS9,lblS10,lblS11
                ,lblS12,lblS13,lblS14,lblS15,lblS16,lblS17,lblS18,lblS19,lblS20
            };
            SetLabelStaff();



        }

        public void SetLabelStaff()
        {
            People = db.People.ToList();
            int i = 0;
            foreach (var item in People)
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
            lblCountStaff.Text = "(" + People.Count().ToString() + ")";
        }

        public void Selected(Label lb)
        {
            int id = Convert.ToInt32(lb.Tag);
            SelectId = id;
            person = People.Where(p => p.Id == id).FirstOrDefault();
            txtName.Text = person.Name;
            txtLastName.Text = person.Lastname;
            txtNickName.Text = person.NickName;

            if (person.Level != null)
                cmbLevel.Text = person.Level.ToString();
            else
                cmbLevel.Text = "";




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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var per =db.People.Where(p => p.Id == SelectId).FirstOrDefault();
            db.People.Remove(person);

            db.SaveChanges();


        }
    }
}
