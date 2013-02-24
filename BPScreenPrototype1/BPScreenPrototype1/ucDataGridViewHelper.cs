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
    public partial class ucDataGridViewHelper : UserControl
    {
        public ucDataGridViewHelper()
        {
            InitializeComponent();
        }

        public DataSet DataSource { get; set; }

        public void DataBind()
        {
            dataGridView1.DataSource = this.DataSource;
        }
    }
}
