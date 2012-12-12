using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataGridViewBindingSourceComboBox1
{
    public partial class Form1 : Form
    {
        DataGridView _dg1;
        BindingSource _bs1;
        BindingSource _bsSites;
        List<OrgUpdate> _toBeUpdatedList;
        List<WebSite> _availableWebSites;

        public Form1()
        {
            InitializeComponent();

            _toBeUpdatedList = new List<OrgUpdate>();
            _toBeUpdatedList.Add(new OrgUpdate() { OrganizationName = "EOC Professional", WebSiteName = "Default Web Site", WebSiteId = "1" });
            _toBeUpdatedList.Add(new OrgUpdate() { OrganizationName = "EOC Professional 2" });

            _availableWebSites = new List<WebSite>();
            _availableWebSites.Add(new WebSite() { WebSiteName = "Default Web Site", WebSiteId = "1" });
            _availableWebSites.Add(new WebSite() { WebSiteName = "ASP1", WebSiteId = "2" });
            _availableWebSites.Add(new WebSite() { WebSiteName = "ASP2", WebSiteId = "3" });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _dg1 = new DataGridView();
            _dg1.AutoSize = true;

            _bsSites = new BindingSource();
            _bsSites.DataSource = _availableWebSites;

            _bs1 = new BindingSource();
            _bs1.DataSource = _toBeUpdatedList;
            _dg1.DataSource = _bs1;
            this.Controls.Add(_dg1);
            _dg1.Columns["WebSiteName"].Visible = false;
            _dg1.Columns["VDir"].Visible = false;
            _dg1.Columns["WebSiteId"].Visible = false;

            DataGridViewComboBoxColumn columnSites = new DataGridViewComboBoxColumn();
            columnSites.DataPropertyName = "WebSiteId";
            columnSites.HeaderText = "Available Sites";
            columnSites.Width = 120;

            columnSites.DataSource = _bsSites;
            columnSites.ValueMember = "WebSiteId";
            columnSites.DisplayMember = "WebSiteName";
            _dg1.Columns.Add(columnSites);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }

    public class WebSite
    {
        public string WebSiteName { get; set; }
        public string WebSiteId { get; set; }
    }

    public class OrgUpdate
    {
        public string OrganizationName { get; set; }
        public string VDir { get; set; }
        public string WebSiteName { get; set; }
        public string WebSiteId { get; set; }
        public string RegistryStatus { get; set; }
    }
}