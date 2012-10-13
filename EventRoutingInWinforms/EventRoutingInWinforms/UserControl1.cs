using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        // define an event handler delegate which basically re-uses an existing
        // signature
        public delegate void EventHandler(object sender, System.EventArgs e);
        // decalre an event handler delegate
        EventHandler EventHandlerDelegate;
        // re-define the Click event
        new public event EventHandler Click
        {
            // this is the equivalent of Click += new EventHandler(...)
            add
            {
                this.EventHandlerDelegate += value;
            }
            // this is the equivalent of Click -= new EventHandler(...)
            remove
            {
                this.EventHandlerDelegate -= value;
            }
        }

        // forward the button's click event 
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.EventHandlerDelegate != null)
                this.EventHandlerDelegate(sender, e);
        }

        
    }
}
