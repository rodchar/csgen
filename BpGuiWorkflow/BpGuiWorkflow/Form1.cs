using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BpGuiWorkflow
{
    public partial class Form1 : Form
    {
        Main m;
        ListPage l;
        DetailPage d;

        public Form1()
        {
            InitializeComponent();

            btnBack.Visible = false;
            btnSave.Visible = false;

            m = new Main();
            m.AnyButtonClickedMain += new EventHandler(m_AnyButtonClickedMain);
            this.Controls.Add(m);

            l = new ListPage();
            l.AnyButtonClickedList += new EventHandler(m_AnyButtonClickedMain);
            l.Visible = false;
            this.Controls.Add(l);

            d = new DetailPage();
            d.AnyButtonClickedDetail += new EventHandler(m_AnyButtonClickedMain);
            d.Visible = false;
            this.Controls.Add(d);

        }

        void m_AnyButtonClickedMain(object sender, EventArgs e)
        {
            var ctrl = sender as Control;

            TurnOffControls();

            switch (ctrl.Text)
            {
                case "Customers":
                case "Products":
                case "Rewards":
                case "Receipts":
                    l.Visible = true;
                    btnExit.Visible = false;
                    break;
                case "Detail":
                    d.Visible = true;
                    btnBack.Visible = true;
                    btnSave.Visible = true;
                    btnExit.Visible = false;

                    break;
                default:
                    break;
            }

            //MessageBox.Show(ctrl.Text);
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            TurnOffControls();
            m.Visible = true;
            btnExit.Visible = true;
        }

        private void TurnOffControls()
        {
            if (m != null)
                m.Visible = false;
            if (l != null)
                l.Visible = false;
            if (d != null)
                d.Visible = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            TurnOffControls();
            btnBack.Visible = false;
            btnSave.Visible = false;

            l.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TurnOffControls();
            btnExit.Visible = false;
            btnBack.Visible = false;
            btnSave.Visible = false;

            l.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
