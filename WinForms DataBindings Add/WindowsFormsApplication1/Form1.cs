using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Customer selectedCustomer;
        List<Customer> list = new List<Customer>();

        public Form1()
        {
            InitializeComponent();
            selectedCustomer = new Customer() { Id = 2, FirstName = "Jane" };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            list.Add(new Customer() { Id = 1, FirstName = "John" });
            list.Add(new Customer() { Id = 2, FirstName = "Jane" });

            comboBox1.DisplayMember = "FirstName";
            comboBox1.ValueMember = "Id";
            comboBox1.DataSource = list;
            comboBox1.DataBindings.Add("Text", selectedCustomer, "FirstName");

            label1.DataBindings.Add("Text", comboBox1, "Text");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = selectedCustomer.FirstName;
        }

    }

    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}