namespace IntelliName
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
            this.buttonExec = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.listViewNames = new System.Windows.Forms.ListView();
            this.columnHeaderChar = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCount = new System.Windows.Forms.ColumnHeader();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonGen = new System.Windows.Forms.Button();
            this.listBoxName = new System.Windows.Forms.ListBox();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonExec
            // 
            this.buttonExec.Location = new System.Drawing.Point(393, 21);
            this.buttonExec.Name = "buttonExec";
            this.buttonExec.Size = new System.Drawing.Size(83, 27);
            this.buttonExec.TabIndex = 0;
            this.buttonExec.Text = "Execute";
            this.buttonExec.UseVisualStyleBackColor = true;
            this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(393, 64);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(83, 27);
            this.buttonLoad.TabIndex = 1;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // listViewNames
            // 
            this.listViewNames.AutoArrange = false;
            this.listViewNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderChar,
            this.columnHeaderCount});
            this.listViewNames.Location = new System.Drawing.Point(17, 21);
            this.listViewNames.Name = "listViewNames";
            this.listViewNames.Size = new System.Drawing.Size(351, 160);
            this.listViewNames.TabIndex = 2;
            this.listViewNames.UseCompatibleStateImageBehavior = false;
            this.listViewNames.View = System.Windows.Forms.View.Details;
            this.listViewNames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewNames_ColumnClick);
            // 
            // columnHeaderChar
            // 
            this.columnHeaderChar.Text = "Char";
            // 
            // columnHeaderCount
            // 
            this.columnHeaderCount.Text = "Count";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(393, 108);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(83, 27);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save..";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonGen
            // 
            this.buttonGen.Location = new System.Drawing.Point(393, 154);
            this.buttonGen.Name = "buttonGen";
            this.buttonGen.Size = new System.Drawing.Size(83, 27);
            this.buttonGen.TabIndex = 4;
            this.buttonGen.Text = "Generate";
            this.buttonGen.UseVisualStyleBackColor = true;
            this.buttonGen.Click += new System.EventHandler(this.buttonGen_Click);
            // 
            // listBoxName
            // 
            this.listBoxName.FormattingEnabled = true;
            this.listBoxName.ItemHeight = 12;
            this.listBoxName.Location = new System.Drawing.Point(17, 201);
            this.listBoxName.Name = "listBoxName";
            this.listBoxName.Size = new System.Drawing.Size(350, 136);
            this.listBoxName.TabIndex = 5;
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(17, 361);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(457, 52);
            this.listBoxLog.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 343);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Status:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 416);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.listBoxName);
            this.Controls.Add(this.buttonGen);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.listViewNames);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonExec);
            this.Name = "FormMain";
            this.Text = "IntelliName";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonExec;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ListView listViewNames;
        private System.Windows.Forms.ColumnHeader columnHeaderChar;
        private System.Windows.Forms.ColumnHeader columnHeaderCount;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonGen;
        private System.Windows.Forms.ListBox listBoxName;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Label label1;
    }
}

