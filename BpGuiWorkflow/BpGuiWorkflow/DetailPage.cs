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
    public partial class DetailPage : UserControl
    {
        public event EventHandler AnyButtonClickedDetail;

        public DetailPage()
        {
            InitializeComponent();
        }

        private void anyButton_Clicked(object sender, EventArgs e)
        {
            if (AnyButtonClickedDetail != null)
                AnyButtonClickedDetail(sender, e);
        }
    }
}
