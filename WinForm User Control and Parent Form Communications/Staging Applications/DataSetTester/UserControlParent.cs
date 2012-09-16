using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataSetTester
{
    public partial class UserControlParent : Form
    {
        public static string ReceiptProductName { get; set; }
        public static string ReceiptItemName { get; set; }

        public int ProductId { get; set; }

        public UserControlParent()
        {
            InitializeComponent();
        }

        private void ParentForm_Load(object sender, EventArgs e)
        {
            UserControl1 uc1 = new UserControl1();
            panel1.Controls.Add(uc1);
            uc1.Show();
            UserControl2 uc2 = new UserControl2();
            panel2.Controls.Add(uc2);
            uc2.Show();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            label1.Text = string.Format("The items selected in the user controls above are as follows:\n{0}\n{1}",
                ReceiptProductName,
                ReceiptItemName
                );
        }

    }
}
