﻿using System;
using System.Windows.Forms;
using System.Data;
using DataAccessLayer;
using SQL_Dynamic_Efficient_Paging;

namespace DataGridViewExercise1
{
    public partial class DGV : UserControl
    {
        //http://www.codeproject.com/Articles/11014/How-to-route-events-in-a-Windows-Forms-application
        //See comments at bottom "Sure but..."
        public event EventHandler UserControlClicked;

        private enum SortEventSequence
        {
            Before,
            After
        }

        private DataTable _dt = new DataTable();

        public DataRowView DataRowViewSelected { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public string SortBy { get; set; }
        private DataGridViewColumnHeaderCellCustom _hc;

        public string SearchEntry
        {
            get { return tbSearch.Text; }
            set { tbSearch.Text = value; }
        }

        public DataTable DataSource
        {
            get { return _dt; }
            set
            {
                _dt = value;
                //All rows will have TP and TR.
                //Total pages and total records.
                DataRow dr = null;
                if (_dt.Rows.Count > 0)
                    dr = _dt.Rows[0];

                int totalPages = 1;
                int totalRecords = 0;

                if (dr != null)
                {
                    int.TryParse(dr["TP"].ToString(), out totalPages);
                    int.TryParse(dr["TR"].ToString(), out totalRecords);
                }

                TotalPages = totalPages;
                TotalRecords = totalRecords;
                SetDatabaseResultsInfo();
                BindDataGridView();
            }
        }

        private void BindDataGridView()
        {
            dataGridView1.DataSource = DataSource;
                        
            if (_hc != null)
                dataGridView1.Columns[_hc.ColumnIndex].HeaderCell.SortGlyphDirection = _hc.SortGlyphDirection;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if ("TP|TR|Seq|Id".IndexOf(col.Name) > -1) col.Visible = false;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
                col.HeaderText = col.Name.SpacePascalCase();
            }

            //http://stackoverflow.com/a/8847025/139698
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        
        private void UserControlClick(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn != null)
            {
                if (btn.Text == "Next")
                {
                    if (PageNumber < TotalPages)
                        PageNumber++;

                    SetPagerStatus(true, true, (PageNumber < TotalPages), (PageNumber != TotalPages));
                }

                if (btn.Text == "Prev")
                {
                    if (PageNumber > 1)
                        PageNumber--;

                    SetPagerStatus((PageNumber > 1), (PageNumber > 1), true, true);
                }

                if (btn.Text == "First")
                {
                    PageNumber = 1;
                    SetPagerStatus(false, false, true, true);
                }

                if (btn.Text == "Last")
                {
                    PageNumber = TotalPages;
                    SetPagerStatus(true, true, false, false);
                }

            }

            if (btn.Text == "Search")
            {
                FirstPageSettings();
            }

            //If I put search here and valid search, next buttons don't work.

            //Before raising event 

            if (UserControlClicked != null) UserControlClicked(sender, e);

            //After raising event 

            //If I put search here and on page 3 with blank search doesn't quite work

            if (btn.Text == "Search")
            {
                FirstPageSettings();
            }

        }

        private void FirstPageSettings()
        {
            tbPage.Text = "1";
            PageNumber = 1;

            //Clear sort related settings.
            _hc = null; 
            SortBy = string.Empty;

            SetPagerStatus(false, false, (TotalPages > 1), (TotalPages > 1));
        }

        private void SetDatabaseResultsInfo()
        {
            if (PageNumber == 0) { PageNumber = 1; }
            lblPage.Text = string.Format("Page {0} of {1}  ({2} records)", PageNumber, TotalPages, TotalRecords);
            tbPage.Text = PageNumber.ToString();
        }

        private void SetPagerStatus(bool first, bool prev, bool next, bool last)
        {
            btnFirst.Enabled = first;
            btnPrev.Enabled = prev;
            btnNext.Enabled = next;
            btnLast.Enabled = last;
        }

        public DGV()
        {
            InitializeComponent();
        }        

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SortAction(sender, e, SortEventSequence.Before);

            //Before raising event 

            if (UserControlClicked != null) UserControlClicked(sender, e);

            //After raising event 

            SortAction(sender, e, SortEventSequence.After);
        }

        private void SortAction(object sender, DataGridViewCellMouseEventArgs e, SortEventSequence eventSequence)
        {
            //Manual sort is made possible by changing sort mode of 
            //each column. See BindDataGrid method to see this.
            DataGridView dg = sender as DataGridView;
            int colIndex = e.ColumnIndex;
            DataGridViewColumnHeaderCell hc = dg.Columns[colIndex].HeaderCell;
            DataGridViewColumn col = dg.Columns[colIndex];

            if (hc.SortGlyphDirection == SortOrder.None 
                || (col.Name != SortBy && !SortBy.Contains("DESC")))
            {
                if (eventSequence == SortEventSequence.Before) SortBy = col.Name;
                if (eventSequence == SortEventSequence.After) hc.SortGlyphDirection = SortOrder.Ascending;
            }
            else if (col.Name == SortBy)
            {
                if (eventSequence == SortEventSequence.Before) SortBy = string.Format("{0} DESC", col.Name);
                if (eventSequence == SortEventSequence.After) hc.SortGlyphDirection = SortOrder.Ascending;
            }            
            else if (SortBy.Contains("DESC"))
            {
                if (eventSequence == SortEventSequence.Before) SortBy = col.Name;
                if (eventSequence == SortEventSequence.After) hc.SortGlyphDirection = SortOrder.Descending;
            }

            _hc = Clone(hc);
        }

        public static DataGridViewColumnHeaderCellCustom Clone(DataGridViewColumnHeaderCell hc)
        {
            //Had to build my own because DataGridViewColumnHeaderCellCustom's
            //clone didn't work.
            var toReturn = new DataGridViewColumnHeaderCellCustom();
            toReturn.ColumnIndex = hc.ColumnIndex;
            toReturn.SortGlyphDirection = hc.SortGlyphDirection;
            return toReturn;
        }

        private void DGV_Load(object sender, EventArgs e)
        {
            FirstPageSettings();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //CellDoubleClick event better suited then CellContentDoubleClick or CellMouseDoubleClick
            //http://stackoverflow.com/a/13327791/139698
            if (e.RowIndex > -1) //Like double clicking column's auto width
            {
                DataRowViewSelected = ((DataRowView)dataGridView1.Rows[e.RowIndex].DataBoundItem); //["Name"]
                //Raise event
                if (UserControlClicked != null) UserControlClicked(sender, e);
            }
        }
    }

    public class DataGridViewColumnHeaderCellCustom 
    {
        public int ColumnIndex { get; set; }
        public SortOrder SortGlyphDirection { get; set; }

    }

    public static class CustomExtensions
    {
        public static string SpacePascalCase(this String input)
        {
            string splitString = String.Empty;

            for (int idx = 0; idx < input.Length; idx++)
            {
                char c = input[idx];

                if (Char.IsUpper(c)
                    // keeps abbreviations together like "Number HEI"

                   // instead of making it "Number H E I"

                     && ((idx < input.Length - 1
                             && !Char.IsUpper(input[idx + 1]))
                         || (idx != 0
                             && !Char.IsUpper(input[idx - 1])))
                     && splitString.Length > 0)
                {
                    splitString += " ";
                }

                splitString += c;
            }

            return splitString;
        }
    }
}