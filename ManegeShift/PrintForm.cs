using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace ManegeShift
{
    public partial class PrintForm : Form
    {
        HiiiEntities db;
        string filee;
        public PrintForm()
        {
            db = new HiiiEntities();
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            btnReport.Enabled = false;
            List<Shift> shifts = new List<Shift>();
            string radif = "";
            string morning = "";
            string mid = "";
            string evening = "";
            string split = "";
            string rest = "";

           var  DateStart = ("13" + cmbYearStart.Text + "/" + cmbMonthStart.Text + "/" + cmbDayStart.Text).ToGeorgianDateTime();
           var  DateEnd = ("13" + cmbYearEnd.Text + "/" + cmbMonthEnd.Text + "/" + cmbDayEnd.Text).ToGeorgianDateTime();

            for (int i = 0; i < 14& DateStart.AddDays(i)<=DateEnd; i++)
            {

                 radif = (i+1).ToString();
                 morning = "";
                 mid = "";
                 evening = "";
                 split = "";
                 rest = "";

                var y = DateStart.AddDays(i);
                var date = db.ShiftDays.Where(p => p.Date ==y).ToList();
                foreach (var item in date)
                {
                    switch (item.Status_fk)
                    {
                        case 1:
                            morning = morning + "," + item.Person.NickName.Trim();
                            break;
                        case 2:
                            mid = mid + "," + item.Person.NickName.Trim() + "(" + item.mid.Trim() + ")";
                            break;
                        case 3:
                            evening = evening + "," + item.Person.NickName.Trim();
                            break;
                        case 4:
                            split = split + "," + item.Person.NickName.Trim()+"(split)";
                            break;
                        case 5:
                            rest = rest + "," + item.Person.NickName.Trim();
                            break;
                        default:
                            break;
                    }
                }
                if(date.Count()!=0)
                {
                    shifts.Add(new Shift()
                    {
                        Radif = radif,
                        Date = date.ElementAt(0).Date.ToPersianDateString(),
                        Day = date.ElementAt(0).Date.DayOfWeek.ToString(),
                        Morning = morning.TrimStart(','),
                        Mid = mid.TrimStart(','),
                        Evening = evening.TrimStart(','),
                        Split = split.TrimStart(','),
                        Rest = rest.TrimStart(',')
                    });
                }
             


            }



            var rt = Path.GetDirectoryName(Application.ExecutablePath);
            FileInfo newFile = new FileInfo(rt+ "\\Time SCHEDULE.xlsx");
            filee = Path.GetDirectoryName(Application.ExecutablePath)+"\\temp" + "\\"+DateTime.Today.ToPersianDateString().Replace("/","")+".xlsx";
            FileInfo newFilee = new FileInfo(filee);

            try
            {
                if (newFilee.Exists)
                    newFilee.Delete();
            }
            catch (Exception)
            {
                MessageBox.Show("Close File Exel");
            }


            ExcelPackage pck = new ExcelPackage(newFilee,newFile);
            //Add the Content sheet
            //  var ws = pck.Workbook.Worksheets.Add("Content");

            var ws = pck.Workbook.Worksheets.ElementAt(0);

            ws.View.ShowGridLines = true;


            for (int i = 0; i < shifts.Count(); i++)
            {
                ws.Cells["B" + (15 + i).ToString()].Value = shifts.ElementAt(i).Date;
                ws.Cells["C" + (15 + i).ToString()].Value = shifts.ElementAt(i).Day;
                ws.Cells["D"+(15+i).ToString()].Value = shifts.ElementAt(i).Morning;
                ws.Cells["E" + (15 + i).ToString()].Value = shifts.ElementAt(i).Mid+ shifts.ElementAt(i).Split;
                ws.Cells["G" + (15 + i).ToString()].Value = shifts.ElementAt(i).Evening;
                ws.Cells["H" + (15 + i).ToString()].Value = shifts.ElementAt(i).Rest;


                if (shifts.ElementAt(i).Mid == ""& shifts.ElementAt(i).Split == "")
                {
                    ws.Cells["E" + (15 + i).ToString() + ":" + "F" + (15 + i).ToString()].Merge = false;
                    ws.Cells["D" + (15 + i).ToString() + ":" + "E" + (15 + i).ToString()].Merge = true;
                    ws.Cells["F" + (15 + i).ToString() + ":" + "G" + (15 + i).ToString()].Merge = true;

                    ws.Cells["E" + (15 + i).ToString()].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    ws.Cells["F" + (15 + i).ToString()].Value = shifts.ElementAt(i).Evening;

                }


            }


          
            ws.DeleteRow(15+shifts.Count(), 14-shifts.Count(), true);
            //ws.Cells["B1:E1"].Style.Font.Bold = true;

            try
            {
                pck.Save();
                System.Diagnostics.Process.Start(filee);
            }
            catch (Exception)
            {

                MessageBox.Show("Close File Exel");
               
            }


            btnReport.Enabled = true;
        }

        private void PrintForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            int x = 1397;
            var t = db.ShiftDays.Max(p => p.Date);
            int xx =Convert.ToInt16 (t.ToPersianDateString().Substring(0, 4));
            while (x<=xx)
            {
                cmbYearStart.Items.Add(x.ToString().Substring(2,2));
                cmbYearEnd.Items.Add(x.ToString().Substring(2, 2));
                x = x + 1;
            }

            cmbDayStart.DataSource = DateTime.Today.ToPersianDateString().ReturnDaysOfMonth();
            cmbDayEnd.DataSource = DateTime.Today.ToPersianDateString().ReturnDaysOfMonth();

            cmbYearStart.Text = DateTime.Today.ToPersianDateString().Substring(2, 2);
            cmbMonthStart.Text = DateTime.Today.ToPersianDateString().Substring(5, 2);
            cmbDayStart.Text = DateTime.Today.ToPersianDateString().Substring(8 ,2);

            cmbYearEnd.Text = DateTime.Today.ToPersianDateString().Substring(2, 2);
            cmbMonthEnd.Text = DateTime.Today.ToPersianDateString().Substring(5, 2);
            cmbDayEnd.Text = DateTime.Today.ToPersianDateString().Substring(8, 2);

        }

        private void cmbMonthStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = cmbDayStart.Text;
            cmbDayStart.DataSource = ("13" + cmbYearStart.Text + "/" + cmbMonthStart.Text + "/" + cmbDayStart.Text) .ReturnDaysOfMonth();
            cmbDayStart.Text = temp;
        }

        private void cmbYearStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = cmbDayStart.Text;
            cmbDayStart.DataSource = ("13" + cmbYearStart.Text + "/" + cmbMonthStart.Text + "/" + cmbDayStart.Text).ReturnDaysOfMonth();
            cmbDayStart.Text = temp;
        }

        private void cmbMonthEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = cmbDayEnd.Text;
            cmbDayEnd.DataSource = ("13" + cmbYearEnd.Text + "/" + cmbMonthEnd.Text + "/" + cmbDayEnd.Text).ReturnDaysOfMonth();
            cmbDayEnd.Text = temp;
        }

        private void cmbYearEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = cmbDayEnd.Text;
            cmbDayEnd.DataSource = ("13" + cmbYearEnd.Text + "/" + cmbMonthEnd.Text + "/" + cmbDayEnd.Text).ReturnDaysOfMonth();
            cmbDayEnd.Text = temp;
        }





        //StiReport stiReport1 = new StiReport();

        //stiReport1.Load(@"D:\projects\ManegeShift\ManegeShift\bin\Debug\Report.mrt");
        ////stiReport1.Dictionary.Variables["date"].Value = txtDate.Text;
        ////stiReport1.Dictionary.Variables["part"].Value = cmbPart.GetItemText(cmbPart.SelectedItem);
        ////stiReport1.Dictionary.Variables["Variable1"].Value = "maryam";

        // stiReport1.RegBusinessObject("Shift", shifts);

        //stiReport1.Show();
    }
    
}
