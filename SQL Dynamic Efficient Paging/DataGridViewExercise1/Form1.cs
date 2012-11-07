using System;
using System.Windows.Forms;

namespace DataGridViewExercise1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgv1.Click += Dgv1Click;            
        }

        void Dgv1Click(object sender, EventArgs e)
        {
            var ctrl = sender as Control;

            if (ctrl != null)
                MessageBox.Show(
                    string.Format(
                        @"userControl11_Click
                    {0}, {1}",
                        ctrl.Text,
                        ctrl.GetType().FullName),
                    Text);
        }
    }
}
