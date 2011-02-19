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
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxStockId = new System.Windows.Forms.ComboBox();
            this.buttonAutoComp = new System.Windows.Forms.Button();
            this.buttonCalcAdjust = new System.Windows.Forms.Button();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importYahooToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBounusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkConsistencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.menuStripMain.SuspendLayout();
            this.SettingGroupBox.SuspendLayout();
            this.groupBoxAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.HorizontalScrollbar = true;
            this.listBoxLog.ItemHeight = 15;
            this.listBoxLog.Location = new System.Drawing.Point(15, 337);
            this.listBoxLog.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(697, 229);
            this.listBoxLog.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(574, 65);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(116, 30);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save Log";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCompare
            // 
            this.buttonCompare.Location = new System.Drawing.Point(16, 63);
            this.buttonCompare.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(130, 30);
            this.buttonCompare.TabIndex = 5;
            this.buttonCompare.Text = "View Result...";
            this.buttonCompare.UseVisualStyleBackColor = true;
            this.buttonCompare.Click += new System.EventHandler(this.buttonCompare_Click);
            // 
            // buttonCalc
            // 
            this.buttonCalc.Location = new System.Drawing.Point(16, 25);
            this.buttonCalc.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCalc.Name = "buttonCalc";
            this.buttonCalc.Size = new System.Drawing.Size(130, 30);
            this.buttonCalc.TabIndex = 8;
            this.buttonCalc.Text = "Calculate...";
            this.buttonCalc.UseVisualStyleBackColor = true;
            this.buttonCalc.Click += new System.EventHandler(this.buttonCalc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(322, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Min Date: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(500, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Max Date: ";
            // 
            // labelMinDate
            // 
            this.labelMinDate.AutoSize = true;
            this.labelMinDate.Location = new System.Drawing.Point(415, 61);
            this.labelMinDate.Name = "labelMinDate";
            this.labelMinDate.Size = new System.Drawing.Size(79, 15);
            this.labelMinDate.TabIndex = 11;
            this.labelMinDate.Text = "(Unknown)";
            // 
            // labelMaxDate
            // 
            this.labelMaxDate.AutoSize = true;
            this.labelMaxDate.Location = new System.Drawing.Point(593, 60);
            this.labelMaxDate.Name = "labelMaxDate";
            this.labelMaxDate.Size = new System.Drawing.Size(87, 15);
            this.labelMaxDate.TabIndex = 12;
            this.labelMaxDate.Text = "(Unknown) ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Start Date: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "End Date: ";
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Location = new System.Drawing.Point(136, 28);
            this.dateTimePickerStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(153, 25);
            this.dateTimePickerStart.TabIndex = 15;
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Location = new System.Drawing.Point(136, 65);
            this.dateTimePickerEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(153, 25);
            this.dateTimePickerEnd.TabIndex = 16;
            // 
            // buttonResetStart
            // 
            this.buttonResetStart.Location = new System.Drawing.Point(310, 28);
            this.buttonResetStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonResetStart.Name = "buttonResetStart";
            this.buttonResetStart.Size = new System.Drawing.Size(79, 25);
            this.buttonResetStart.TabIndex = 17;
            this.buttonResetStart.Text = "Reset";
            this.buttonResetStart.UseVisualStyleBackColor = true;
            this.buttonResetStart.Click += new System.EventHandler(this.buttonResetStart_Click);
            // 
            // buttonResetEnd
            // 
            this.buttonResetEnd.Location = new System.Drawing.Point(310, 62);
            this.buttonResetEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonResetEnd.Name = "buttonResetEnd";
            this.buttonResetEnd.Size = new System.Drawing.Size(79, 28);
            this.buttonResetEnd.TabIndex = 18;
            this.buttonResetEnd.Text = "Reset";
            this.buttonResetEnd.UseVisualStyleBackColor = true;
            this.buttonResetEnd.Click += new System.EventHandler(this.buttonResetEnd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 15);
            this.label5.TabIndex = 20;
            this.label5.Text = "Select Stock: ";
            // 
            // comboBoxStockId
            // 
            this.comboBoxStockId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStockId.FormattingEnabled = true;
            this.comboBoxStockId.Location = new System.Drawing.Point(151, 57);
            this.comboBoxStockId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxStockId.Name = "comboBoxStockId";
            this.comboBoxStockId.Size = new System.Drawing.Size(153, 23);
            this.comboBoxStockId.TabIndex = 21;
            this.comboBoxStockId.SelectedIndexChanged += new System.EventHandler(this.comboBoxStockId_SelectedIndexChanged);
            // 
            // buttonAutoComp
            // 
            this.buttonAutoComp.Location = new System.Drawing.Point(154, 63);
            this.buttonAutoComp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAutoComp.Name = "buttonAutoComp";
            this.buttonAutoComp.Size = new System.Drawing.Size(116, 30);
            this.buttonAutoComp.TabIndex = 25;
            this.buttonAutoComp.Text = "Auto Compare";
            this.buttonAutoComp.UseVisualStyleBackColor = true;
            this.buttonAutoComp.Click += new System.EventHandler(this.buttonAutoComp_Click);
            // 
            // buttonCalcAdjust
            // 
            this.buttonCalcAdjust.Location = new System.Drawing.Point(154, 25);
            this.buttonCalcAdjust.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCalcAdjust.Name = "buttonCalcAdjust";
            this.buttonCalcAdjust.Size = new System.Drawing.Size(116, 30);
            this.buttonCalcAdjust.TabIndex = 26;
            this.buttonCalcAdjust.Text = "Adjust Param";
            this.buttonCalcAdjust.UseVisualStyleBackColor = true;
            this.buttonCalcAdjust.Click += new System.EventHandler(this.buttonCalcAdjust_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(724, 28);
            this.menuStripMain.TabIndex = 27;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importYahooToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.addBounusToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importYahooToolStripMenuItem
            // 
            this.importYahooToolStripMenuItem.Name = "importYahooToolStripMenuItem";
            this.importYahooToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.importYahooToolStripMenuItem.Text = "&Import Yahoo...";
            this.importYahooToolStripMenuItem.Click += new System.EventHandler(this.importYahooToolStripMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.downloadToolStripMenuItem.Text = "&Download...";
            // 
            // addBounusToolStripMenuItem
            // 
            this.addBounusToolStripMenuItem.Name = "addBounusToolStripMenuItem";
            this.addBounusToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.addBounusToolStripMenuItem.Text = "&Add Bonus...";
            this.addBounusToolStripMenuItem.Click += new System.EventHandler(this.addBounusToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkConsistencyToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.dataToolStripMenuItem.Text = "&Data";
            // 
            // checkConsistencyToolStripMenuItem
            // 
            this.checkConsistencyToolStripMenuItem.Name = "checkConsistencyToolStripMenuItem";
            this.checkConsistencyToolStripMenuItem.Size = new System.Drawing.Size(213, 24);
            this.checkConsistencyToolStripMenuItem.Text = "&Check Consistency";
            this.checkConsistencyToolStripMenuItem.Click += new System.EventHandler(this.checkConsistencyToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stockChartToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // stockChartToolStripMenuItem
            // 
            this.stockChartToolStripMenuItem.Name = "stockChartToolStripMenuItem";
            this.stockChartToolStripMenuItem.Size = new System.Drawing.Size(174, 24);
            this.stockChartToolStripMenuItem.Text = "&Stock Chart...";
            this.stockChartToolStripMenuItem.Click += new System.EventHandler(this.stockChartToolStripMenuItem_Click);
            // 
            // SettingGroupBox
            // 
            this.SettingGroupBox.Controls.Add(this.label3);
            this.SettingGroupBox.Controls.Add(this.dateTimePickerStart);
            this.SettingGroupBox.Controls.Add(this.buttonResetStart);
            this.SettingGroupBox.Controls.Add(this.label4);
            this.SettingGroupBox.Controls.Add(this.dateTimePickerEnd);
            this.SettingGroupBox.Controls.Add(this.buttonResetEnd);
            this.SettingGroupBox.Location = new System.Drawing.Point(15, 106);
            this.SettingGroupBox.Name = "SettingGroupBox";
            this.SettingGroupBox.Size = new System.Drawing.Size(697, 102);
            this.SettingGroupBox.TabIndex = 28;
            this.SettingGroupBox.TabStop = false;
            this.SettingGroupBox.Text = "Setting";
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.buttonAutoComp);
            this.groupBoxAction.Controls.Add(this.buttonCalcAdjust);
            this.groupBoxAction.Controls.Add(this.buttonCalc);
            this.groupBoxAction.Controls.Add(this.buttonCompare);
            this.groupBoxAction.Controls.Add(this.buttonSave);
            this.groupBoxAction.Location = new System.Drawing.Point(15, 224);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(697, 102);
            this.groupBoxAction.TabIndex = 29;
            this.groupBoxAction.TabStop = false;
            this.groupBoxAction.Text = "Action";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 582);
            this.Controls.Add(this.groupBoxAction);
            this.Controls.Add(this.SettingGroupBox);
            this.Controls.Add(this.comboBoxStockId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelMaxDate);
            this.Controls.Add(this.labelMinDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StockAnalyzer";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.SettingGroupBox.ResumeLayout(false);
            this.SettingGroupBox.PerformLayout();
            this.groupBoxAction.ResumeLayout(false);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxStockId;
        private System.Windows.Forms.Button buttonAutoComp;
        private System.Windows.Forms.Button buttonCalcAdjust;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importYahooToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBounusToolStripMenuItem;
        private System.Windows.Forms.GroupBox SettingGroupBox;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkConsistencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockChartToolStripMenuItem;
    }
}

