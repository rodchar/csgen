using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        /*
        * Prerequisite:
        * Rewards table
        * -------------
        * Id, OrderDate
        * 
        * RewardRequirements table
        * ---------------------
        * Id, OrderId, ProductId, CategoryId, Quantity, ProductName*
        * 
        * ProductName is local to DataSet and doesn't exist in the table.
        * It is there for the DataGridView Order Details table.
        * 
        * Resources:
        * http://stackoverflow.com/q/508293/139698

        * 
        * */

        DataSet1 _ds;
        DataSet1TableAdapters.TableAdapterManager _dm;
        int _orderId;

        public Form1()
        {
            InitializeComponent();

            _dm = new WindowsFormsApplication1.DataSet1TableAdapters.TableAdapterManager();
            _dm.RewardsTableAdapter = new WindowsFormsApplication1.DataSet1TableAdapters.RewardsTableAdapter();
            _dm.RewardRequirementsTableAdapter = new WindowsFormsApplication1.DataSet1TableAdapters.RewardRequirementsTableAdapter();
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            UnBindFormData();

            if (_ds == null)
            {
                _ds = new DataSet1();
                AddNewOrder();
            }

            Button b = sender as Button;

            switch (b.Name)
            {
                case "btnAddProduct":
                    AddNewProduct();
                    break;
                case "btnSave":
                    SaveOrder();
                    BindFormData();
                    break;
                case "btnFind":
                    LoadOrder();
                    BindFormData();
                    break;
                case "btnNew":
                    NewOrder();
                    break;
                default:
                    break;
            }
        }

        private void AddNewOrder()
        {
            DataSet1.RewardsRow drRewards = _ds.Rewards.NewRewardsRow();
            _orderId = drRewards.Id;
            drRewards.Name = tbName.Text;
            _ds.Rewards.Rows.Add(drRewards);
        }

        private void AddNewProduct()
        {
            DataSet1.RewardRequirementsRow drRewardRequirements = _ds.RewardRequirements.NewRewardRequirementsRow();
            drRewardRequirements.RewardId = _orderId == 0 ? _orderId : _orderId;
            drRewardRequirements.Quantity = 9;
            _ds.RewardRequirements.Rows.Add(drRewardRequirements);

            dataGridView1.DataSource = _ds.RewardRequirements;
        }

        private void NewOrder()
        {
            _ds = null;
            lblId.Text = string.Empty;
            tbName.Text = string.Empty;
            dataGridView1.DataSource = null;
        }

        private void LoadOrder()
        {
            //Todo: Remove. For testing purposes
            int orderId = 0;
            int.TryParse(tbFind.Text, out orderId);
            _orderId = orderId;

            _ds = new DataSet1();

            _ds.Rewards.Merge(_dm.RewardsTableAdapter.GetDataById(orderId));
            _ds.RewardRequirements.Merge(_dm.RewardRequirementsTableAdapter.GetDataByRewardId(orderId));
            dataGridView1.DataSource = _ds.RewardRequirements;
        }

        private void SaveOrder()
        {
            if (_ds != null)
            {
                int result = 0;
                //So _ds gets refreshed after update for free
                result = _dm.UpdateAll(_ds);
                //As a result of the refresh why not bind it, right?
                //See switch statement
                //_ds = null;
                //_orderId = 0;
            }
        }

        private void BindFormData()
        {
            lblId.DataBindings.Add("Text", _ds.Rewards, "Id");
            tbName.DataBindings.Add("Text", _ds.Rewards, "Name");
        }

        private void UnBindFormData()
        {
            lblId.DataBindings.Clear();
            tbName.DataBindings.Clear();
        }
    }
}