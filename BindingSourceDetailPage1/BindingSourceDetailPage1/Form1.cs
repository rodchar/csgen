using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BindingSourceDetailPage1
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
            foreach (OrgUpdate item in _toBeUpdatedList)
            {
                int index = _toBeUpdatedList.IndexOf(item);
                Label l = new Label();
                l.Text = item.OrganizationName;
                tableLayoutPanel1.Controls.Add(l, 0, index);

                TextBox l2 = new TextBox();
                l2.Text = item.WebSiteName;
                tableLayoutPanel1.Controls.Add(l2, 1, index);
                //Binding Example from Programming Visual Basic .Net book
                l2.DataBindings.Add("Text", item, "WebSiteName");
            }
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