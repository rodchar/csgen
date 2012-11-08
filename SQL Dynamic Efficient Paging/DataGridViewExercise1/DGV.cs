using System;
using System.Windows.Forms;
using System.Data;
using DataAccessLayer;

namespace DataGridViewExercise1
{
    public partial class DGV : UserControl
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public string SearchEntry
        {
            get { return tbSearch.Text; }
            set { tbSearch.Text = value; }
        }

        public DataTable DataSource 
        { 
            get { return (DataTable)dataGridView1.DataSource; } 
            set 
            {
                int totalPages = 1;
                int totalRecords = 0;
                if (value.Rows.Count > 0)
                {
                    int.TryParse(value.Rows[0]["TP"].ToString(), out totalPages);
                    int.TryParse(value.Rows[0]["TR"].ToString(), out totalRecords);
                    TotalPages = totalPages;
                    TotalRecords = totalRecords;
                }
                else
                {
                    //No recordds were returned.
                    TotalPages = 1;
                    TotalRecords = 0;
                }
                
                UpdatePageStatus();
                dataGridView1.DataSource = value; 
            }
        }

        public DGV()
        {
            InitializeComponent();
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

                    btnNext.Enabled = (PageNumber < TotalPages);
                    btnPrev.Enabled = true;
                    btnFirst.Enabled = true;
                    btnLast.Enabled = (PageNumber != TotalPages);
                    
                }
                
                if (btn.Text == "Prev")
                {

                    if (PageNumber > 1)
                        PageNumber--;

                    btnPrev.Enabled = (PageNumber > 1);
                    btnFirst.Enabled = (PageNumber > 1);
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }

                if (btn.Text == "First")
                {
                    PageNumber = 1;

                    btnPrev.Enabled = false;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = true;
                    btnNext.Enabled = true;
                }

                if (btn.Text == "Last")
                {
                    PageNumber = TotalPages;

                    btnLast.Enabled = false;
                    btnNext.Enabled = false;
                    btnPrev.Enabled = true;
                    btnFirst.Enabled = true;
                }

            }

            if (btn.Text == "Search")
            {
                FirstPageSettings();
            }


            //Before raising event 

            if (EventHandlerDelegate != null)
                EventHandlerDelegate(sender, e);

            //After raising event 


        }

        private void FirstPageSettings()
        {
            tbPage.Text = "1";
            PageNumber = 1;

            btnPrev.Enabled = false;
            btnFirst.Enabled = false;

            btnNext.Enabled = (TotalPages > 1);
            btnLast.Enabled = (TotalPages > 1);

            btnPrev.Enabled = (PageNumber > 1);
            btnFirst.Enabled = (PageNumber > 1);
        }

        private void DGV_Load(object sender, EventArgs e)
        {            
            dataGridView1.DataSource = DataSource;
            FirstPageSettings();
        }

        private void UpdatePageStatus()
        {
            lblPage.Text = string.Format("Page {0} of {1}  ({2} records)", PageNumber, TotalPages, TotalRecords);
            tbPage.Text = PageNumber.ToString();
        }
    }
}
