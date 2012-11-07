using System;
using System.Windows.Forms;

namespace DataGridViewExercise1
{
    public partial class DGV : UserControl
    {
        public string SearchEntry
        {
            get { return tbSearch.Text; }
            set { tbSearch.Text = value; }
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
            //var btn = sender as Button;

            //if (btnSearch.Text=="Search")
            //{
                
            //}

            if (EventHandlerDelegate != null)
                EventHandlerDelegate(sender, e);          
        }
    }
}
