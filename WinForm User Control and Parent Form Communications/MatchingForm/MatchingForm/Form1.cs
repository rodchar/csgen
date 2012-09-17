using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatchingForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProductsUserControl ucProducts = new ProductsUserControl();
            ReceiptItemNamesUserControl ucReceiptItemNames = new ReceiptItemNamesUserControl();
            ProductsPanel.Controls.Add(ucProducts);
            ReceiptItemNamesPanel.Controls.Add(ucReceiptItemNames);
        }

        private void btnLinkNames_Click(object sender, EventArgs e)
        {

        }
    }
}
