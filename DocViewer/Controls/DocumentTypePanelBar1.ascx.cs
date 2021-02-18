using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.Design;

namespace DocViewer.Controls
{
    public partial class DocumentTypePanelBar1 :  System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Object.Equals(Session["PanelBarPersistenceKey"], null))
            {
                LoadButton.Enabled = false;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Session["PanelBarPersistenceKey"] = this.Session.SessionID;
            string fileId = Session["PanelBarPersistenceKey"].ToString();
            LoadButton.Enabled = true;
            RadPersistenceManager1.StorageProviderKey = fileId;
            RadPersistenceManager1.SaveState();
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            RadPanelBar1.ClearSelectedItems();
            RadPanelBar1.CollapseAllItems();
        }

        protected void LoadButton_Click(object sender, EventArgs e)
        {
            string fileId = Session["PanelBarPersistenceKey"].ToString();
            RadPersistenceManager1.StorageProviderKey = fileId;
            RadPersistenceManager1.LoadState();
        }
    }
}