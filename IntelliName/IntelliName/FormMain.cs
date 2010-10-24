using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IntelliName.DB;
using IntelliName.Business;
using IntelliName.UI;
using System.IO;
using IntelliName.Log;

namespace IntelliName
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            lvwColumnSorter = new ListViewColumnSorter();
            this.listViewNames.ListViewItemSorter = lvwColumnSorter;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseFactory.GetDB().Init();

            UiLog log = new UiLog();
            log.SetListBox(listBoxName);

            LogFactory.Instance().Log = log;
        }

        private void SaveLastNames()
        {
            LastNameSaver saver = new LastNameSaver();
            saver.Save("g:\\百家姓u.txt");
        }

        private void buttonExec_Click(object sender, EventArgs e)
        {
            PersonNames names = new PersonNames();
            names.Init();

            names.Load("g:\\人员姓名统计U.txt");
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            ICollection<CharCount> arr = DatabaseFactory.GetDB().LoadAllNameCharsByOrder();
            foreach (CharCount item in arr)
            {
                ListViewItem listItem = new ListViewItem(item.CharVal);
                listItem.SubItems.Add(item.Count.ToString());

                listViewNames.Items.Add(listItem);
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseFactory.GetDB().Close();
        }

        private void listViewNames_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listViewNames.Sort();

        }

        private ListViewColumnSorter lvwColumnSorter;

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "txt";
            dlg.Filter = "Text files (*.txt)|*.txt";
            DialogResult result = dlg.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            if (listViewNames.Items.Count <= 0)
            {
                return;
            }

            SaveNameChars2(dlg.FileName);
        }

        private void SaveNameChars(string fileName)
        {
            using (StreamWriter sr = new StreamWriter(fileName))
            {
                foreach (ListViewItem item in listViewNames.Items)
                {
                    string line = string.Format("{0} {1}", item.SubItems[0].Text, item.SubItems[1].Text);
                    sr.WriteLine(line);
                }

                sr.Close();
            }
        }

        private void SaveNameChars2(string fileName)
        {
            using (StreamWriter sr = new StreamWriter(fileName))
            {
                string line = "";
                string line2 = "";
                string line3 = "";
                string line4 = "";

                foreach (ListViewItem item in listViewNames.Items)
                {
                    int count = int.Parse(item.SubItems[1].Text);
                    if (count >= 50)
                    {
                        line += item.SubItems[0].Text;
                    }
                    else if (count >= 10)
                    {
                        line2 += item.SubItems[0].Text;
                    }
                    else if (count > 1)
                    {
                        line3 += item.SubItems[0].Text;
                    }
                    else
                    {
                        line4 += item.SubItems[0].Text;
                    }
                }

                sr.WriteLine("Very often used (Count >= 50): ");
                sr.WriteLine(line);
                sr.WriteLine("Often used (Count >= 10): ");
                sr.WriteLine(line2);
                sr.WriteLine("Rarely used (Count < 10): ");
                sr.WriteLine(line3);
                sr.WriteLine("Very rarely used (Count = 1): ");
                sr.WriteLine(line4);

                sr.Close();
            }
        }

        private void buttonGen_Click(object sender, EventArgs e)
        {
            listBoxName.Items.Clear();

            NameGenerator gen = new NameGenerator();

            ICollection<string> arr = gen.Generate(10);

            foreach (string item in arr)
            {
                listBoxName.Items.Add(item);
            }
        }
    }
}
