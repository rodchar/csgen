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
    public partial class ProductsUserControl : UserControl
    {
        public ProductsUserControl()
        {
            InitializeComponent();
        }

        private void ProductsUserControl_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = -1;
        }
    }
}
