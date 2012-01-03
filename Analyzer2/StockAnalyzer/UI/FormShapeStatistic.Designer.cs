namespace FinanceAnalyzer.UI
{
    partial class FormShapeStatistic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShapeStatistic));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxStockId = new System.Windows.Forms.ComboBox();
            this.groupBoxStatisticType = new System.Windows.Forms.GroupBox();
            this.comboBoxStatisticType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxStatName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBoxStatisticType.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxStockId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(701, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stock Setting";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select stock:";
            // 
            // comboBoxStockId
            // 
            this.comboBoxStockId.FormattingEnabled = true;
            this.comboBoxStockId.Location = new System.Drawing.Point(124, 23);
            this.comboBoxStockId.Name = "comboBoxStockId";
            this.comboBoxStockId.Size = new System.Drawing.Size(123, 20);
            this.comboBoxStockId.TabIndex = 1;
            // 
            // groupBoxStatisticType
            // 
            this.groupBoxStatisticType.Controls.Add(this.comboBoxStatName);
            this.groupBoxStatisticType.Controls.Add(this.label3);
            this.groupBoxStatisticType.Controls.Add(this.comboBoxStatisticType);
            this.groupBoxStatisticType.Controls.Add(this.label2);
            this.groupBoxStatisticType.Location = new System.Drawing.Point(12, 76);
            this.groupBoxStatisticType.Name = "groupBoxStatisticType";
            this.groupBoxStatisticType.Size = new System.Drawing.Size(701, 58);
            this.groupBoxStatisticType.TabIndex = 2;
            this.groupBoxStatisticType.TabStop = false;
            this.groupBoxStatisticType.Text = "Statistic";
            // 
            // comboBoxStatisticType
            // 
            this.comboBoxStatisticType.FormattingEnabled = true;
            this.comboBoxStatisticType.Location = new System.Drawing.Point(124, 23);
            this.comboBoxStatisticType.Name = "comboBoxStatisticType";
            this.comboBoxStatisticType.Size = new System.Drawing.Size(123, 20);
            this.comboBoxStatisticType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Statistic Type:";
            // 
            // comboBoxStatName
            // 
            this.comboBoxStatName.FormattingEnabled = true;
            this.comboBoxStatName.Location = new System.Drawing.Point(395, 23);
            this.comboBoxStatName.Name = "comboBoxStatName";
            this.comboBoxStatName.Size = new System.Drawing.Size(123, 20);
            this.comboBoxStatName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Statistic Name:";
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(11, 140);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(109, 27);
            this.buttonRun.TabIndex = 3;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            // 
            // FormShapeStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 473);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.groupBoxStatisticType);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormShapeStatistic";
            this.Text = "Statistics";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxStatisticType.ResumeLayout(false);
            this.groupBoxStatisticType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxStockId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxStatisticType;
        private System.Windows.Forms.ComboBox comboBoxStatName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxStatisticType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRun;


    }
}