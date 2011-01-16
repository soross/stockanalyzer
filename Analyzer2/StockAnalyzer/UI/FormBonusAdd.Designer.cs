namespace FinanceAnalyzer.UI
{
    partial class FormBonusAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBonusAdd));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBonuscount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBonusAdded = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDividend = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePickerRegistOn = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerExex = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePickerDividend = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePickerListOn = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxStockId = new System.Windows.Forms.TextBox();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "年份：";
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Items.AddRange(new object[] {
            "2011",
            "2010",
            "2009",
            "2008",
            "2007",
            "2006",
            "2005",
            "2004",
            "2003",
            "2002",
            "2001",
            "2000",
            "1999",
            "1998",
            "1997",
            "1996",
            "1995",
            "1994",
            "1993",
            "1992",
            "1991",
            "1990"});
            this.comboBoxYear.Location = new System.Drawing.Point(89, 55);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(129, 23);
            this.comboBoxYear.TabIndex = 1;
            this.comboBoxYear.SelectedIndexChanged += new System.EventHandler(this.comboBoxYear_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "分红： 10送";
            // 
            // textBoxBonuscount
            // 
            this.textBoxBonuscount.Location = new System.Drawing.Point(128, 88);
            this.textBoxBonuscount.Name = "textBoxBonuscount";
            this.textBoxBonuscount.Size = new System.Drawing.Size(90, 25);
            this.textBoxBonuscount.TabIndex = 3;
            this.textBoxBonuscount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "分红： 10转增";
            // 
            // textBoxBonusAdded
            // 
            this.textBoxBonusAdded.Location = new System.Drawing.Point(128, 134);
            this.textBoxBonusAdded.Name = "textBoxBonusAdded";
            this.textBoxBonusAdded.Size = new System.Drawing.Size(90, 25);
            this.textBoxBonusAdded.TabIndex = 5;
            this.textBoxBonusAdded.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "分红： 10派";
            // 
            // textBoxDividend
            // 
            this.textBoxDividend.Location = new System.Drawing.Point(128, 173);
            this.textBoxDividend.Name = "textBoxDividend";
            this.textBoxDividend.Size = new System.Drawing.Size(90, 25);
            this.textBoxDividend.TabIndex = 7;
            this.textBoxDividend.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(279, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "股权登记日：";
            // 
            // dateTimePickerRegistOn
            // 
            this.dateTimePickerRegistOn.Location = new System.Drawing.Point(405, 14);
            this.dateTimePickerRegistOn.Name = "dateTimePickerRegistOn";
            this.dateTimePickerRegistOn.Size = new System.Drawing.Size(143, 25);
            this.dateTimePickerRegistOn.TabIndex = 9;
            // 
            // dateTimePickerExex
            // 
            this.dateTimePickerExex.Location = new System.Drawing.Point(405, 54);
            this.dateTimePickerExex.Name = "dateTimePickerExex";
            this.dateTimePickerExex.Size = new System.Drawing.Size(143, 25);
            this.dateTimePickerExex.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(279, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "除权除息日：";
            // 
            // dateTimePickerDividend
            // 
            this.dateTimePickerDividend.Location = new System.Drawing.Point(405, 93);
            this.dateTimePickerDividend.Name = "dateTimePickerDividend";
            this.dateTimePickerDividend.Size = new System.Drawing.Size(143, 25);
            this.dateTimePickerDividend.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(279, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "派息日：";
            // 
            // dateTimePickerListOn
            // 
            this.dateTimePickerListOn.Location = new System.Drawing.Point(405, 132);
            this.dateTimePickerListOn.Name = "dateTimePickerListOn";
            this.dateTimePickerListOn.Size = new System.Drawing.Size(143, 25);
            this.dateTimePickerListOn.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(279, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "红股上市日：";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(11, 216);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(52, 15);
            this.labelStatus.TabIndex = 16;
            this.labelStatus.Text = "状态：";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(405, 168);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(140, 35);
            this.buttonAdd.TabIndex = 18;
            this.buttonAdd.Text = "添加";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 19;
            this.label9.Text = "股票：";
            // 
            // textBoxStockId
            // 
            this.textBoxStockId.Location = new System.Drawing.Point(89, 14);
            this.textBoxStockId.Name = "textBoxStockId";
            this.textBoxStockId.Size = new System.Drawing.Size(129, 25);
            this.textBoxStockId.TabIndex = 20;
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 15;
            this.listBoxLog.Location = new System.Drawing.Point(14, 240);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(530, 109);
            this.listBoxLog.TabIndex = 21;
            // 
            // FormBonusAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 367);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.textBoxStockId);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.dateTimePickerListOn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dateTimePickerDividend);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateTimePickerExex);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dateTimePickerRegistOn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDividend);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxBonusAdded);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxBonuscount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxYear);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBonusAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Bonus";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBonuscount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBonusAdded;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDividend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerRegistOn;
        private System.Windows.Forms.DateTimePicker dateTimePickerExex;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePickerDividend;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePickerListOn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxStockId;
        private System.Windows.Forms.ListBox listBoxLog;
    }
}