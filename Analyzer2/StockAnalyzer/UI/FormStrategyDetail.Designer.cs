namespace FinanceAnalyzer.UI
{
    partial class FormStrategyDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStrategyDetail));
            this.listViewStrategy = new System.Windows.Forms.ListView();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewStrategy
            // 
            this.listViewStrategy.Location = new System.Drawing.Point(11, 18);
            this.listViewStrategy.Name = "listViewStrategy";
            this.listViewStrategy.Size = new System.Drawing.Size(645, 364);
            this.listViewStrategy.TabIndex = 0;
            this.listViewStrategy.UseCompatibleStateImageBehavior = false;
            this.listViewStrategy.View = System.Windows.Forms.View.Details;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(577, 388);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(79, 25);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // FormStrategyDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 425);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.listViewStrategy);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormStrategyDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Strategy Detail";
            this.Load += new System.EventHandler(this.FormStrategyDetail_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewStrategy;
        private System.Windows.Forms.Button buttonOk;
    }
}