using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            this.userControl11.Click += new UserControl1.EventHandler(userControl11_Click);
        }

        void userControl11_Click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            MessageBox.Show(
                string.Format(
                    @"userControl11_Click
                    {0}, {1}",
                ctrl.Text,
                ctrl.GetType().FullName),
                this.Text);
        }

        private void userControl_Click1(object sender, System.EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.codeproject.com/Articles/11014/How-to-route-events-in-a-Windows-Forms-application");
        }

        
    }
}
