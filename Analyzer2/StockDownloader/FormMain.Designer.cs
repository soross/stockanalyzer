namespace StockDownloader
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.textBoxStockIDs = new System.Windows.Forms.TextBox();
            this.labelStockId = new System.Windows.Forms.Label();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.labelTo = new System.Windows.Forms.Label();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxStockIDs
            // 
            this.textBoxStockIDs.Location = new System.Drawing.Point(22, 31);
            this.textBoxStockIDs.Name = "textBoxStockIDs";
            this.textBoxStockIDs.Size = new System.Drawing.Size(382, 21);
            this.textBoxStockIDs.TabIndex = 0;
            // 
            // labelStockId
            // 
            this.labelStockId.AutoSize = true;
            this.labelStockId.Location = new System.Drawing.Point(20, 11);
            this.labelStockId.Name = "labelStockId";
            this.labelStockId.Size = new System.Drawing.Size(59, 12);
            this.labelStockId.TabIndex = 1;
            this.labelStockId.Text = "Stock ID:";
            // 
            // labelPeriod
            // 
            this.labelPeriod.AutoSize = true;
            this.labelPeriod.Location = new System.Drawing.Point(20, 67);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(47, 12);
            this.labelPeriod.TabIndex = 2;
            this.labelPeriod.Text = "Period:";
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Location = new System.Drawing.Point(22, 92);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(119, 21);
            this.dateTimePickerStart.TabIndex = 3;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(157, 98);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(17, 12);
            this.labelTo.TabIndex = 4;
            this.labelTo.Text = "to";
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Location = new System.Drawing.Point(206, 92);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(119, 21);
            this.dateTimePickerEnd.TabIndex = 5;
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(242, 141);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(78, 25);
            this.buttonDownload.TabIndex = 6;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(22, 179);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(381, 172);
            this.listBoxLog.TabIndex = 7;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(326, 141);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(78, 25);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 375);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.labelPeriod);
            this.Controls.Add(this.labelStockId);
            this.Controls.Add(this.textBoxStockIDs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "StockImporter";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxStockIDs;
        private System.Windows.Forms.Label labelStockId;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button buttonClose;
    }
}

