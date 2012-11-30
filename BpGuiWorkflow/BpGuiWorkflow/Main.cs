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
    public partial class Main : UserControl
    {
        public event EventHandler AnyButtonClickedMain;

        public Main()
        {
            InitializeComponent();
        }

        private void anyButton_Clicked(object sender, EventArgs e)
        {
            if (AnyButtonClickedMain != null)
                AnyButtonClickedMain(sender, e);
        }
    }
}
