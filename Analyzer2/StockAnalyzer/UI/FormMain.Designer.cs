namespace FinanceAnalyzer
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCompare = new System.Windows.Forms.Button();
            this.buttonCalc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelMinDate = new System.Windows.Forms.Label();
            this.labelMaxDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.buttonResetStart = new System.Windows.Forms.Button();
            this.buttonResetEnd = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxStockId = new System.Windows.Forms.ComboBox();
            this.buttonAddBonus = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonChart = new System.Windows.Forms.Button();
            this.buttonAutoComp = new System.Windows.Forms.Button();
            this.buttonCalcAdjust = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.HorizontalScrollbar = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(8, 162);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(552, 292);
            this.listBoxLog.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(472, 127);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(87, 24);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCompare
            // 
            this.buttonCompare.Location = new System.Drawing.Point(472, 97);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(87, 24);
            this.buttonCompare.TabIndex = 5;
            this.buttonCompare.Text = "View...";
            this.buttonCompare.UseVisualStyleBackColor = true;
            this.buttonCompare.Click += new System.EventHandler(this.buttonCompare_Click);
            // 
            // buttonCalc
            // 
            this.buttonCalc.Location = new System.Drawing.Point(359, 128);
            this.buttonCalc.Name = "buttonCalc";
            this.buttonCalc.Size = new System.Drawing.Size(87, 24);
            this.buttonCalc.TabIndex = 8;
            this.buttonCalc.Text = "Calculate...";
            this.buttonCalc.UseVisualStyleBackColor = true;
            this.buttonCalc.Click += new System.EventHandler(this.buttonCalc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Min Date: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Max Date: ";
            // 
            // labelMinDate
            // 
            this.labelMinDate.AutoSize = true;
            this.labelMinDate.Location = new System.Drawing.Point(111, 42);
            this.labelMinDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMinDate.Name = "labelMinDate";
            this.labelMinDate.Size = new System.Drawing.Size(59, 12);
            this.labelMinDate.TabIndex = 11;
            this.labelMinDate.Text = "(Unknown)";
            // 
            // labelMaxDate
            // 
            this.labelMaxDate.AutoSize = true;
            this.labelMaxDate.Location = new System.Drawing.Point(111, 70);
            this.labelMaxDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMaxDate.Name = "labelMaxDate";
            this.labelMaxDate.Size = new System.Drawing.Size(65, 12);
            this.labelMaxDate.TabIndex = 12;
            this.labelMaxDate.Text = "(Unknown) ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "Start Date: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 130);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "End Date: ";
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Location = new System.Drawing.Point(113, 99);
            this.dateTimePickerStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(116, 21);
            this.dateTimePickerStart.TabIndex = 15;
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Location = new System.Drawing.Point(113, 128);
            this.dateTimePickerEnd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(116, 21);
            this.dateTimePickerEnd.TabIndex = 16;
            // 
            // buttonResetStart
            // 
            this.buttonResetStart.Location = new System.Drawing.Point(244, 97);
            this.buttonResetStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonResetStart.Name = "buttonResetStart";
            this.buttonResetStart.Size = new System.Drawing.Size(59, 24);
            this.buttonResetStart.TabIndex = 17;
            this.buttonResetStart.Text = "Reset";
            this.buttonResetStart.UseVisualStyleBackColor = true;
            this.buttonResetStart.Click += new System.EventHandler(this.buttonResetStart_Click);
            // 
            // buttonResetEnd
            // 
            this.buttonResetEnd.Location = new System.Drawing.Point(244, 124);
            this.buttonResetEnd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonResetEnd.Name = "buttonResetEnd";
            this.buttonResetEnd.Size = new System.Drawing.Size(59, 24);
            this.buttonResetEnd.TabIndex = 18;
            this.buttonResetEnd.Text = "Reset";
            this.buttonResetEnd.UseVisualStyleBackColor = true;
            this.buttonResetEnd.Click += new System.EventHandler(this.buttonResetEnd_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(472, 8);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(87, 24);
            this.buttonImport.TabIndex = 19;
            this.buttonImport.Text = "Import...";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 14);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "Select Stock: ";
            // 
            // comboBoxStockId
            // 
            this.comboBoxStockId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStockId.FormattingEnabled = true;
            this.comboBoxStockId.Location = new System.Drawing.Point(113, 11);
            this.comboBoxStockId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxStockId.Name = "comboBoxStockId";
            this.comboBoxStockId.Size = new System.Drawing.Size(116, 20);
            this.comboBoxStockId.TabIndex = 21;
            this.comboBoxStockId.SelectedIndexChanged += new System.EventHandler(this.comboBoxStockId_SelectedIndexChanged);
            // 
            // buttonAddBonus
            // 
            this.buttonAddBonus.Location = new System.Drawing.Point(359, 8);
            this.buttonAddBonus.Name = "buttonAddBonus";
            this.buttonAddBonus.Size = new System.Drawing.Size(87, 24);
            this.buttonAddBonus.TabIndex = 22;
            this.buttonAddBonus.Text = "Add Bonus...";
            this.buttonAddBonus.UseVisualStyleBackColor = true;
            this.buttonAddBonus.Click += new System.EventHandler(this.buttonAddBonus_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Location = new System.Drawing.Point(359, 67);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(87, 24);
            this.buttonCheck.TabIndex = 23;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // buttonChart
            // 
            this.buttonChart.Location = new System.Drawing.Point(359, 97);
            this.buttonChart.Name = "buttonChart";
            this.buttonChart.Size = new System.Drawing.Size(87, 24);
            this.buttonChart.TabIndex = 24;
            this.buttonChart.Text = "Chart...";
            this.buttonChart.UseVisualStyleBackColor = true;
            this.buttonChart.Click += new System.EventHandler(this.buttonChart_Click);
            // 
            // buttonAutoComp
            // 
            this.buttonAutoComp.Location = new System.Drawing.Point(472, 67);
            this.buttonAutoComp.Name = "buttonAutoComp";
            this.buttonAutoComp.Size = new System.Drawing.Size(87, 24);
            this.buttonAutoComp.TabIndex = 25;
            this.buttonAutoComp.Text = "Auto Compare";
            this.buttonAutoComp.UseVisualStyleBackColor = true;
            this.buttonAutoComp.Click += new System.EventHandler(this.buttonAutoComp_Click);
            // 
            // buttonCalcAdjust
            // 
            this.buttonCalcAdjust.Location = new System.Drawing.Point(472, 38);
            this.buttonCalcAdjust.Name = "buttonCalcAdjust";
            this.buttonCalcAdjust.Size = new System.Drawing.Size(87, 24);
            this.buttonCalcAdjust.TabIndex = 26;
            this.buttonCalcAdjust.Text = "Adjust Param";
            this.buttonCalcAdjust.UseVisualStyleBackColor = true;
            this.buttonCalcAdjust.Click += new System.EventHandler(this.buttonCalcAdjust_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 466);
            this.Controls.Add(this.buttonCalcAdjust);
            this.Controls.Add(this.buttonAutoComp);
            this.Controls.Add(this.buttonChart);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonAddBonus);
            this.Controls.Add(this.comboBoxStockId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonResetEnd);
            this.Controls.Add(this.buttonResetStart);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelMaxDate);
            this.Controls.Add(this.labelMinDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCalc);
            this.Controls.Add(this.buttonCompare);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.listBoxLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StockAnalyzer";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCompare;
        private System.Windows.Forms.Button buttonCalc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelMinDate;
        private System.Windows.Forms.Label labelMaxDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Button buttonResetStart;
        private System.Windows.Forms.Button buttonResetEnd;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxStockId;
        private System.Windows.Forms.Button buttonAddBonus;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonChart;
        private System.Windows.Forms.Button buttonAutoComp;
        private System.Windows.Forms.Button buttonCalcAdjust;
    }
}

