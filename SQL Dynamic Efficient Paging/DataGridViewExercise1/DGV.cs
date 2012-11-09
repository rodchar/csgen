﻿using System;
using System.Windows.Forms;
using System.Data;
using DataAccessLayer;
using SQL_Dynamic_Efficient_Paging;

namespace DataGridViewExercise1
{
    public partial class DGV : UserControl
    {
        private DataSet1 _ds = new DataSet1();

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public string SearchEntry
        {
            get { return tbSearch.Text; }
            set { tbSearch.Text = value; }
        }

        public string DataMember { get; set; }
        
        public DataSet1 DataSource
        {
            get { return _ds; }
            set
            {
                _ds = value;
                int totalPages = 1;
                int totalRecords = 0;

                if (_ds.Tables[DataMember].Rows.Count > 0)
                {
                    int.TryParse(_ds.Tables[DataMember].Rows[0]["TP"].ToString(), out totalPages);
                    int.TryParse(_ds.Tables[DataMember].Rows[0]["TR"].ToString(), out totalRecords);
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
            dataGridView1.DataSource = DataSource.Tables[DataMember];
        }        

        private void FirstPageSettings()
        {
            tbPage.Text = "1";
            PageNumber = 1;

            SetPagerStatus(false, false, (TotalPages > 1), (TotalPages > 1));
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
    }
}