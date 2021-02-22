using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Web.UI;
using Telerik.Web.Design;

namespace DocViewer.Controls
{
    public partial class DocumentTypePanelBar1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RadPanelBar1.Enabled = true;
           
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

        protected void RadPanelBar1_ItemCreated(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {

        }

        protected void RadPanelBar1_ItemDataBound(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            e.Item.Visible = true;
            e.Item.Enabled = true;
            InitializePanelBarItem(e);
            //// EXAMPLE
            ////protected void RadPanelBar1_ItemDataBound(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
            ////{
            ////    DataRowView dataRow = (DataRowView)e.Item.DataItem;
            ////    e.Item.Attributes["Roles"] = dataRow["Roles"].ToString();
            ////    e.Item.ToolTip = e.Item.Attributes["Roles"];
            ////}

            //// if (e.Item.Level > 0)
            //set tooltip only for child items   
            ////   {
            ////  XmlElement element = (XmlElement)e.Item.DataItem;
            ////   e.Item.ToolTip = "Read more about " + element.Attributes["Text"].Value;
            //// }



        }

        private void InitializePanelBarItem(Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            var xmlElement = (XmlElement)e.Item.DataItem;

            if (xmlElement != null)

            {
                
                var menuItemIdText = xmlElement?.Attributes["MenuItemId"]?.Value;
                if (!string.IsNullOrEmpty(menuItemIdText))
                {
                    e.Item.Attributes["MenuItemId"] = menuItemIdText;
                    //(string)DataBinder.Eval(e.Item.DataItem, "ToolTip"); //"Read more about " + (string)DataBinder.Eval(e.Item.DataItem, "Text");
                }

                var toolTipText = xmlElement?.Attributes["ToolTip"]?.Value;
                if (!string.IsNullOrEmpty(toolTipText))
                {
                    e.Item.ToolTip = toolTipText;
                    //(string)DataBinder.Eval(e.Item.DataItem, "ToolTip"); //"Read more about " + (string)DataBinder.Eval(e.Item.DataItem, "Text");
                }

                var filterText = xmlElement?.Attributes["FilterExpression"]?.Value;
                if (!string.IsNullOrEmpty(filterText))
                {
                    e.Item.Attributes["FilterExpression"] = filterText;
                     
                }

                var docClass = xmlElement?.Attributes["DocumentClass"]?.Value;
                if (!string.IsNullOrEmpty(filterText))
                {
                    e.Item.Attributes["DocumentClass"] = docClass;

                }
                var docTypeKey = xmlElement?.Attributes["DocTypeKey"]?.Value;
                if (!string.IsNullOrEmpty(docTypeKey))
                {
                    e.Item.Attributes["DocTypeKey"] = docClass;

                }

                var docSubTypeKey = xmlElement?.Attributes["DocSubTypeKey"]?.Value;
                if (!string.IsNullOrEmpty(docTypeKey))
                {
                    e.Item.Attributes["DocSubTypeKey"] = docClass;

                }

                SetPanelBarItemVisibility(e);
            }
        }

        private void SetPanelBarItemVisibility(Telerik.Web.UI.RadPanelBarEventArgs e)
        {

            e.Item.Visible = false;
            e.Item.Enabled = false;

            var element = (XmlElement)e.Item.DataItem;

            if (element == null)
            {

                e.Item.Visible = true;
                e.Item.Enabled = true;
                return;
            }

            var docTypeKey =
                element.Attributes["DocTypeKey"]?.Value; //(string)DataBinder.Eval(e.Item.DataItem, "DocTypeKey");

            if (string.IsNullOrEmpty(docTypeKey)) // root menu item, must show
            {

                e.Item.Visible = true;
                e.Item.Enabled = true;
                return;
            }

            ((DocViewerMain)this.Page).CheckDocTypeMenuItemAccess(docTypeKey, "", out var isVisible,
                out var isEnabled);
            e.Item.Visible = isVisible;
            e.Item.Enabled = isVisible && isEnabled;
        }


        protected void RadPanelBar1_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            var itemClicked = e.Item;
            Response.Write("Server event raised -- you clicked: " + itemClicked.Text);
        }

        protected void RadPanelBar1_DataBound(object sender, EventArgs e)
        {
            return;
        }

        protected void RadPanelBar1_DataBinding(object sender, EventArgs e)
        {
            return;
        }

        protected void RadPanelBar1_Disposed(object sender, EventArgs e)
        {
      
        }

        protected void RadPanelBar1_Load(object sender, EventArgs e)
        {
        
           
        }

        protected void RadPanelBar1_Unload(object sender, EventArgs e)
        {

            

        }

        protected void RadPersistenceManagerProxy1_OnUnload(object sender, EventArgs e)
        {
          
        }

        protected void RadPersistenceManagerProxy1_Load(object sender, EventArgs e)
        {

        }
    }
}