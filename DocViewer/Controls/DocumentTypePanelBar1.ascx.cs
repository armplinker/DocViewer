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
             
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
           var persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            persistenceManager1.StorageProviderKey = "DocumentPanelBarState";
            persistenceManager1.SaveState();
        }


        protected void LoadButton_Click(object sender, EventArgs e)
        {
            var persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            persistenceManager1.StorageProviderKey = "DocumentPanelBarState";
            persistenceManager1.LoadState();
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            RadPanelBar1.ClearSelectedItems();
            RadPanelBar1.CollapseAllItems();
        }

        private void RadPanelBar1_ItemCreated(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
           SetPanelBarItemVisibility(e);
        }


        private void SetPanelBarItemVisibility(Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            var docTypeKey = (string)DataBinder.Eval(e.Item.DataItem, "DocTypeKey");
            e.Item.Visible = ((MainPage)this.Page).CheckDocTypeVisibility(docTypeKey, "");
        }

        private void RadPanelBar1_ItemDataBound(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            InitializePanelBarItem(e);
        }

        private void InitializePanelBarItem(Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            e.Item.ToolTip = (string)DataBinder.Eval(e.Item.DataItem, "ToolTip"); //"Read more about " + (string)DataBinder.Eval(e.Item.DataItem, "Text");

        }


        protected void RadPanelBar1_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            var itemClicked = e.Item; Response.Write("Server event raised -- you clicked: " + itemClicked.Text);
        }
    }
}