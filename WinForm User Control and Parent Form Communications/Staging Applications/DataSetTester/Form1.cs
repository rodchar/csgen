﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicSpinner;

namespace DataSetTester
{
    public partial class Form1 : Form
    {
        BpDS ds = new BpDS();
        BpDS.ProductsDataTable dtProducts;
        BpDS.ReceiptItemNamesDataTable dtReceiptItemNames;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtProducts = Business.Products();
            dtProducts.CopyToDataTable(this.bpDS1.Products, LoadOption.PreserveChanges);

            lbProducts.SelectedIndex = -1;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbProducts.Text==string.Empty)
            {
                tbProducts.Focus();
                return;
            }

            var row = bpDS1.Products.NewProductsRow();
            row.Name = tbProducts.Text;
            bpDS1.Products.Rows.Add(row);

            tbProducts.Clear();
        }

        private void tbProducts_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int selVal = 0;
            int.TryParse(lbProducts.SelectedValue.ToString(), out selVal);

            if (selVal > 0)
            {
                var editRow = bpDS1.Products.FindById(selVal);
                editRow.Name = tbProducts.Text;
            }

            tbProducts.Clear();
            tbProducts.Focus();
        }

        private void lbProducts_Click(object sender, EventArgs e)
        {
            if (lbProducts.SelectedIndex > -1)
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnCancel.Enabled = true;                

                tbProducts.Text = (lbProducts.SelectedItem as DataRowView)["Name"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.Update(bpDS1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tbProducts.Clear();
            btnUpdate.Enabled = false;
            btnCancel.Enabled = false;
            btnAdd.Enabled = true;
            lbProducts.SelectedIndex = -1;
            tbProducts.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (tbProducts.Text == string.Empty)
            {
                tbProducts.Focus();
                return;
            }
            int idx = lbProducts.FindString(tbProducts.Text);

            if (idx != -1)
            {
                lbProducts.SelectedIndex = idx;
                tbProducts.Text = (lbProducts.SelectedItem as DataRowView)["Name"].ToString();

                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnCancel.Enabled = true;

            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void lbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnRemove.Enabled = lbProducts.SelectedIndex != -1;
            
        }

        private void btnBindTest_Click(object sender, EventArgs e)
        {
            Business.Products().CopyToDataTable(this.bpDS1.Products, LoadOption.PreserveChanges);
        }



    }
}