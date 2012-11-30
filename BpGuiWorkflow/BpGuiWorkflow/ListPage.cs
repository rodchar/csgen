using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BpGuiWorkflow
{
    public partial class ListPage : UserControl
    {
        public event EventHandler AnyButtonClickedList;
        
        public ListPage()
        {
            InitializeComponent();
        }

        private void anyButton_Clicked(object sender, EventArgs e)
        {
            if (AnyButtonClickedList != null)
                AnyButtonClickedList(sender, e);
        }
    }
}