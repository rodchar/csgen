using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatchingForm
{
    public partial class ReceiptItemNamesUserControl : UserControl
    {
        public ReceiptItemNamesUserControl()
        {
            InitializeComponent();
        }

        private void ReceiptItemNamesUserControl_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = -1;
        }
    }
}
