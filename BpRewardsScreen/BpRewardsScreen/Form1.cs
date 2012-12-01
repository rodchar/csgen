using System;
using System.Windows.Forms;
using System.Data;

namespace BpRewardsScreen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Add Button to C# DataGridView 
            //http://tinyurl.com/blsj6rd

            DataTable dt = GetData();

            dataGridView1.DataSource = dt;

            AddPlusMinus();
        }

        private static DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Price");
            dt.Columns.Add("Quantity");

            DataRow dr1 = dt.NewRow();
            dr1["Id"] = 1;
            dr1["Name"] = "Product 1";
            dr1["Price"] = 1000;
            dr1["Quantity"] = 1;
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["Id"] = 2;
            dr2["Name"] = "Product 2";
            dr2["Price"] = 2000;
            dr2["Quantity"] = 1;
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["Id"] = 3;
            dr3["Name"] = "Product 3";
            dr3["Price"] = 3000;
            dr3["Quantity"] = 2;
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["Id"] = 4;
            dr4["Name"] = "Product 4";
            dr4["Price"] = 4000;
            dr4["Quantity"] = 1;
            dt.Rows.Add(dr4);
            return dt;
        }

        private void AddTestButton()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Insert(4, btn);           
            btn.HeaderText = "Click Data";
            btn.Text = "Click Here";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;
        }

        private void AddPlusMinus()
        {
            DataGridViewButtonColumn btnPlus = new DataGridViewButtonColumn();
            btnPlus.Width = 35;
            dataGridView1.Columns.Add(btnPlus);
            btnPlus.HeaderText = "";
            btnPlus.Text = "+";
            btnPlus.Name = "btnPlus";
            btnPlus.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btnMinus = new DataGridViewButtonColumn();
            btnMinus.Width = 35;
            dataGridView1.Columns.Add(btnMinus);
            btnMinus.HeaderText = "";
            btnMinus.Text = "-";
            btnMinus.Name = "btnMinus";
            btnMinus.UseColumnTextForButtonValue = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView row = ((DataRowView)dataGridView1.Rows[e.RowIndex].DataBoundItem);
            int qty = 0;

            if (e.ColumnIndex == 4) //plus
            {
                int.TryParse(row["Quantity"].ToString(), out qty);

                qty++;

                row.BeginEdit();
                row["Quantity"] = qty;
                row.EndEdit();
            }

            if (e.ColumnIndex == 5) //minus
            {
                int.TryParse(row["Quantity"].ToString(), out qty);

                qty--;

                if (qty < 0)
                {
                    qty = 0;
                }
                else
                {
                    row.BeginEdit();
                    row["Quantity"] = qty;
                    row.EndEdit();
                }
            }
        }
    }
}