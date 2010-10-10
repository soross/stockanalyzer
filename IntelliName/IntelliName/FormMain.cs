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
            
        }

        private void SaveLastNames()
        {
            LastNameSaver saver = new LastNameSaver();
            saver.Save("g:\\百家姓u.txt");
        }

        private void buttonExec_Click(object sender, EventArgs e)
        {
            DatabaseFactory.GetDB().Init();

            PersonNames names = new PersonNames();
            names.Init();

            names.Load("g:\\人员姓名统计U.txt");
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            DatabaseFactory.GetDB().Init();

            ICollection<CharCount> arr = DatabaseFactory.GetDB().LoadAllNameCharsByOrder();
            foreach (CharCount item in arr)
            {
                ListViewItem listItem = new ListViewItem(item.CharVal);
                //listItem.SubItems.Add(item.CharVal);
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

    }
}
