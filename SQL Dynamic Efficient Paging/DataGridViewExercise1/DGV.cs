using System;
using System.Windows.Forms;
using System.Data;
using DataAccessLayer;
using SQL_Dynamic_Efficient_Paging;

namespace DataGridViewExercise1
{
    public partial class DGV : UserControl
    {
        private enum SortEventSequence
        {
            Before,
            After
        }

        private DataTable _dt = new DataTable();

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
                DataRow dr = _dt.Rows[0];

                int totalPages = 1;
                int totalRecords = 0;

                if (_dt.Rows.Count > 0)
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

        private void DGV_Load(object sender, EventArgs e)
        {
            BindDataGridView();
            FirstPageSettings();
        }

        private void BindDataGridView()
        {
            dataGridView1.DataSource = DataSource;
                        
            if (_hc != null)
                dataGridView1.Columns[_hc.ColumnIndex].HeaderCell.SortGlyphDirection = _hc.SortGlyphDirection;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if ("TP|TR|Seq".IndexOf(col.Name) > -1) col.Visible = false;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        // forward the button's click event 
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

            if (EventHandlerDelegate != null)
                EventHandlerDelegate(sender, e);

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

            SetPagerStatus(false, false, (TotalPages > 1), (TotalPages > 1));
        }

        private void SetDatabaseResultsInfo()
        {
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

        // define an event handler delegate which basically re-uses an existing
        // signature
        public delegate void EventHandler(object sender, EventArgs e);
        // decalre an event handler delegate
        EventHandler EventHandlerDelegate;
        // re-define the Click event
        new public event EventHandler Click
        {
            // this is the equivalent of Click += new EventHandler(...)
            add
            {
                EventHandlerDelegate += value;
            }
            // this is the equivalent of Click -= new EventHandler(...)
            remove { if (EventHandlerDelegate != null) EventHandlerDelegate -= value; }
        }

        public DGV()
        {
            InitializeComponent();
        }        

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SortAction(sender, e, SortEventSequence.Before);

            //Before raising event 

            if (EventHandlerDelegate != null)
                EventHandlerDelegate(sender, e);

            //After raising event 

            SortAction(sender, e, SortEventSequence.After);
        }

        private void SortAction(object sender, DataGridViewCellMouseEventArgs e, SortEventSequence eventSequence)
        {
            DataGridView dg = sender as DataGridView;
            int colIndex = e.ColumnIndex;
            DataGridViewColumnHeaderCell hc = dg.Columns[colIndex].HeaderCell;
            DataGridViewColumn col = dg.Columns[colIndex];

            if (eventSequence == SortEventSequence.Before)
            {
                //Set SortBy property.
                if (hc.SortGlyphDirection == SortOrder.None)
                {
                    SortBy = col.Name;
                }

                else if (col.Name == SortBy)
                {
                    SortBy = string.Format("{0} DESC", col.Name);
                }

                else if (col.Name != SortBy && !SortBy.Contains("DESC"))
                {
                    SortBy = col.Name;
                }

                else if (SortBy.Contains("DESC"))
                {
                    SortBy = col.Name;
                }

            }

            if (eventSequence == SortEventSequence.After)
            {
                //Set sort glymph.
                if (hc.SortGlyphDirection == SortOrder.None)
                {
                    hc.SortGlyphDirection = SortOrder.Ascending;
                }

                else if (col.Name != SortBy && !SortBy.Contains("DESC"))
                {
                    hc.SortGlyphDirection = SortOrder.Ascending;
                }

                else if (col.Name == SortBy)
                {
                    hc.SortGlyphDirection = SortOrder.Ascending;
                }

                else if (SortBy.Contains("DESC"))
                {
                    hc.SortGlyphDirection = SortOrder.Descending;
                }
                
            }

            _hc = Clone(hc);
        }

        public static DataGridViewColumnHeaderCellCustom Clone(DataGridViewColumnHeaderCell hc)
        {
            var toReturn = new DataGridViewColumnHeaderCellCustom();
            toReturn.ColumnIndex = hc.ColumnIndex;
            toReturn.SortGlyphDirection = hc.SortGlyphDirection;
            return toReturn;
            
        }
    }

    public class DataGridViewColumnHeaderCellCustom 
    {
        public int ColumnIndex { get; set; }
        public SortOrder SortGlyphDirection { get; set; }

    }    
}