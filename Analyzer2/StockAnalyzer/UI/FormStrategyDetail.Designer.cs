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
            this.listViewStrategy = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listViewStrategy
            // 
            this.listViewStrategy.Location = new System.Drawing.Point(11, 18);
            this.listViewStrategy.Name = "listViewStrategy";
            this.listViewStrategy.Size = new System.Drawing.Size(538, 304);
            this.listViewStrategy.TabIndex = 0;
            this.listViewStrategy.UseCompatibleStateImageBehavior = false;
            this.listViewStrategy.View = System.Windows.Forms.View.Details;
            // 
            // FormStrategyDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 369);
            this.Controls.Add(this.listViewStrategy);
            this.Name = "FormStrategyDetail";
            this.Text = "Strategy Detail";
            this.Load += new System.EventHandler(this.FormStrategyDetail_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewStrategy;
    }
}