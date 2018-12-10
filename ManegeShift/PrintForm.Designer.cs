namespace ManegeShift
{
    partial class PrintForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReport = new System.Windows.Forms.Button();
            this.lblDateDay = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDayEnd = new System.Windows.Forms.ComboBox();
            this.cmbMonthEnd = new System.Windows.Forms.ComboBox();
            this.cmbYearEnd = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDayStart = new System.Windows.Forms.ComboBox();
            this.cmbMonthStart = new System.Windows.Forms.ComboBox();
            this.cmbYearStart = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.DarkGreen;
            this.btnReport.BackgroundImage = global::ManegeShift.Resource1.logo;
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.btnReport.Location = new System.Drawing.Point(364, 510);
            this.btnReport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(118, 117);
            this.btnReport.TabIndex = 78;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // lblDateDay
            // 
            this.lblDateDay.BackColor = System.Drawing.Color.Transparent;
            this.lblDateDay.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateDay.Location = new System.Drawing.Point(389, 574);
            this.lblDateDay.Name = "lblDateDay";
            this.lblDateDay.Size = new System.Drawing.Size(55, 32);
            this.lblDateDay.TabIndex = 79;
            this.lblDateDay.Text = "Print";
            this.lblDateDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbDayEnd);
            this.panel1.Controls.Add(this.cmbMonthEnd);
            this.panel1.Controls.Add(this.cmbYearEnd);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbDayStart);
            this.panel1.Controls.Add(this.cmbMonthStart);
            this.panel1.Controls.Add(this.cmbYearStart);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(195, 145);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 194);
            this.panel1.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(53, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 49);
            this.label3.TabIndex = 73;
            this.label3.Text = "End";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(326, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 41);
            this.label4.TabIndex = 72;
            this.label4.Text = "/";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbDayEnd
            // 
            this.cmbDayEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDayEnd.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDayEnd.FormattingEnabled = true;
            this.cmbDayEnd.Location = new System.Drawing.Point(351, 94);
            this.cmbDayEnd.Name = "cmbDayEnd";
            this.cmbDayEnd.Size = new System.Drawing.Size(56, 34);
            this.cmbDayEnd.TabIndex = 71;
            // 
            // cmbMonthEnd
            // 
            this.cmbMonthEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthEnd.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMonthEnd.FormattingEnabled = true;
            this.cmbMonthEnd.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cmbMonthEnd.Location = new System.Drawing.Point(266, 94);
            this.cmbMonthEnd.Name = "cmbMonthEnd";
            this.cmbMonthEnd.Size = new System.Drawing.Size(56, 34);
            this.cmbMonthEnd.TabIndex = 69;
            this.cmbMonthEnd.SelectedIndexChanged += new System.EventHandler(this.cmbMonthEnd_SelectedIndexChanged);
            // 
            // cmbYearEnd
            // 
            this.cmbYearEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearEnd.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYearEnd.FormattingEnabled = true;
            this.cmbYearEnd.Location = new System.Drawing.Point(173, 94);
            this.cmbYearEnd.Name = "cmbYearEnd";
            this.cmbYearEnd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbYearEnd.Size = new System.Drawing.Size(56, 34);
            this.cmbYearEnd.TabIndex = 68;
            this.cmbYearEnd.SelectedIndexChanged += new System.EventHandler(this.cmbYearEnd_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(235, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 41);
            this.label6.TabIndex = 70;
            this.label6.Text = "/";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(53, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 49);
            this.label2.TabIndex = 67;
            this.label2.Text = "Start";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(326, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 41);
            this.label1.TabIndex = 29;
            this.label1.Text = "/";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbDayStart
            // 
            this.cmbDayStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDayStart.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDayStart.FormattingEnabled = true;
            this.cmbDayStart.Location = new System.Drawing.Point(351, 31);
            this.cmbDayStart.Name = "cmbDayStart";
            this.cmbDayStart.Size = new System.Drawing.Size(56, 34);
            this.cmbDayStart.TabIndex = 27;
            // 
            // cmbMonthStart
            // 
            this.cmbMonthStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthStart.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMonthStart.FormattingEnabled = true;
            this.cmbMonthStart.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cmbMonthStart.Location = new System.Drawing.Point(266, 31);
            this.cmbMonthStart.Name = "cmbMonthStart";
            this.cmbMonthStart.Size = new System.Drawing.Size(56, 34);
            this.cmbMonthStart.TabIndex = 24;
            this.cmbMonthStart.SelectedIndexChanged += new System.EventHandler(this.cmbMonthStart_SelectedIndexChanged);
            // 
            // cmbYearStart
            // 
            this.cmbYearStart.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.cmbYearStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearStart.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYearStart.FormattingEnabled = true;
            this.cmbYearStart.Location = new System.Drawing.Point(173, 31);
            this.cmbYearStart.Name = "cmbYearStart";
            this.cmbYearStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbYearStart.Size = new System.Drawing.Size(56, 34);
            this.cmbYearStart.TabIndex = 21;
            this.cmbYearStart.SelectedIndexChanged += new System.EventHandler(this.cmbYearStart_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(235, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 41);
            this.label5.TabIndex = 25;
            this.label5.Text = "/";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ManegeShift.Resource1.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(898, 708);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblDateDay);
            this.Controls.Add(this.btnReport);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PrintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrintForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PrintForm_FormClosed);
            this.Load += new System.EventHandler(this.PrintForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label lblDateDay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDayEnd;
        private System.Windows.Forms.ComboBox cmbMonthEnd;
        private System.Windows.Forms.ComboBox cmbYearEnd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDayStart;
        private System.Windows.Forms.ComboBox cmbMonthStart;
        private System.Windows.Forms.ComboBox cmbYearStart;
        private System.Windows.Forms.Label label5;
    }
}