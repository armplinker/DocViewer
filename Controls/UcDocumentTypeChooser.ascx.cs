using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using DocumentManagerUtil;
using DocViewer;
using Telerik.Web.UI;
using Telerik.Web.Design;
using Telerik.Web.UI.PersistenceFramework;


namespace DocViewer.Controls
{

    public partial class UcDocumentTypeChooser : System.Web.UI.UserControl
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string StateKeyName = @"UcDocumentTypeChooserState";
        protected void Page_Init()
        {
            UCDTC_RadPersistenceManagerProxy1?.PersistenceSettings.AddSetting(DTCRadPanelBar1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DTCRadPanelBar1.Enabled = true;
            currBridgeGd.Value = @"AAAA";
            currInspevntGd.Value = @"BBBB";

            Logger?.Debug("Page_Load Event");

            switch (IsPostBack)
            {
                case false:
                {
                    Logger?.Debug("NOT a PostBack");
                    RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
                    if (persistenceManager1 == null) return;
                    persistenceManager1.StorageProviderKey = StateKeyName;
                    try
                    {
                       // persistenceManager1.LoadState();
                    }
                    catch (Exception ex)
                    {
                        //Path.GetFullPath(Path.Combine(currentDir, relPath1))
                        Logger?.Error($"Persistence file {(Path.GetFullPath(Path.Combine("~\\App_Data", $"{StateKeyName}")))} not found during LoadState() ", ex);
                    }

                    break;
                }
                case true:
                {
                    Logger?.Debug("PostBack");
                    RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
                    if (persistenceManager1 == null) return;
                    persistenceManager1.StorageProviderKey = StateKeyName;

                    try
                    {
                      //  persistenceManager1.SaveState();

                    }
                    catch (Exception ex)
                    {
                        //Path.GetFullPath(Path.Combine(currentDir, relPath1))
                        Logger?.Error($"Persistence file {(Path.GetFullPath(Path.Combine("~\\App_Data", $"{StateKeyName}")))} not found during SaveState() ", ex);
                    }

                    break;
                }
            } // end switch
        }


        protected void DTRadPanelBar1_ItemCreated(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {

        }

        protected void DTCRadPanelBar1_ItemDataBound(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            e.Item.Visible = true;
            e.Item.Enabled = true;
            InitializeDTCPanelBarItem(e);
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

        private void InitializeDTCPanelBarItem(Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            var xmlElement = (XmlElement)e.Item.DataItem;

            if (xmlElement != null)

            {
                var idText = xmlElement?.Attributes["Id"]?.Value;
                if (!string.IsNullOrEmpty(idText))
                {
                    e.Item.Attributes["Id"] = idText;
                    //(string)DataBinder.Eval(e.Item.DataItem, "ToolTip"); //"Read more about " + (string)DataBinder.Eval(e.Item.DataItem, "Text");
                }

                var parentIdText = xmlElement?.Attributes["ParentId"]?.Value;
                if (!string.IsNullOrEmpty(parentIdText))
                {
                    e.Item.Attributes["ParentId"] = parentIdText;
                    //(string)DataBinder.Eval(e.Item.DataItem, "ToolTip"); //"Read more about " + (string)DataBinder.Eval(e.Item.DataItem, "Text");
                }
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
                var docTypeKey = xmlElement?.Attributes["DocTypeKey"]?.Value;
                if (!string.IsNullOrEmpty(docTypeKey))
                {
                    e.Item.Attributes["DocTypeKey"] = docTypeKey;

                }

                if (string.IsNullOrEmpty(docTypeKey))
                    return;

                var docSubTypeKey = xmlElement?.Attributes["DocSubTypeKey"]?.Value;
                if (!string.IsNullOrEmpty(docSubTypeKey))
                {
                    e.Item.Attributes["DocSubTypeKey"] = docSubTypeKey;

                }

                if (!string.IsNullOrEmpty(docTypeKey) && !string.Equals(docTypeKey, "*.*") && string.IsNullOrEmpty(docSubTypeKey))
                    return; // type root item e.g. 10

                var filterExpression = xmlElement?.Attributes["FilterExpression"]?.Value;
                if (!string.IsNullOrEmpty(filterExpression))
                {
                    e.Item.Attributes["FilterExpression"] = filterExpression;

                }

                var docClass = xmlElement?.Attributes["DocumentClass"]?.Value;
                if (!string.IsNullOrEmpty(docClass))
                {
                    e.Item.Attributes["DocumentClass"] = docClass;
                }

                docClass = docClass ?? "D"; // default to Document

                var utils = DocMgrUtils.Instance;
                string docUrl;
                var contentPage = ((DocViewerMain)Page);

                if (string.Equals(docTypeKey, "*.*"))// All documents (*.*)  // all documents
                {

                    docUrl = utils.GenDocViewerNavigateUrl(theDocTypeKey: docTypeKey, theDocSubtypeKey: docSubTypeKey,
                        theDocViewerPageName: contentPage.DocViewerControlName,
                        theBridgeGd: !string.Equals(docClass, "F", StringComparison.OrdinalIgnoreCase)
                            ? currBridgeGd.Value
                            : string.Empty,
                        theInspevntGd: !string.Equals(docClass, "F", StringComparison.OrdinalIgnoreCase)
                            ? currInspevntGd.Value
                            : string.Empty, theShowWaitDialog: false);

                }

                else // must have a subtype if not the all documents menu item
                {
                    // convert any docSubTypeKey = 1000, 2000, 3000 etc. to "" 
                    var iKey = int.TryParse(docSubTypeKey, out var result);
                    if (iKey == true)
                    {
                        if (int.Parse(docTypeKey) * 100 == result)
                        {
                            docSubTypeKey = "";
                        }
                    }

                    docUrl = utils.GenDocViewerNavigateUrl(theDocTypeKey: docTypeKey, theDocSubtypeKey: docSubTypeKey, theDocViewerPageName: contentPage.DocViewerControlName,
                        theBridgeGd: !string.Equals(docClass, "F", StringComparison.OrdinalIgnoreCase)
                            ? currBridgeGd?.Value
                            : "FORMS",
                        theInspevntGd: !string.Equals(docClass, "F", StringComparison.OrdinalIgnoreCase)
                            ? currInspevntGd?.Value
                            : "FORMS", theShowWaitDialog: false);

                }

                e.Item.NavigateUrl = docUrl;

                SetDTCPanelBarItemVisibility(e);

            }
        }

        private void SetDTCPanelBarItemVisibility(Telerik.Web.UI.RadPanelBarEventArgs e)
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

        protected void DTCRadPanelBar1_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            var itemClicked = e.Item;
            Response.Write("Server event raised -- you clicked:" + itemClicked.Text);
        }

        protected void DTCRadPanelBar1_ItemCreated(object sender, RadPanelBarEventArgs e)
        {
            return;
        }

        protected void DTCRadPanelBar1_DataBound(object sender, EventArgs e)
        {
            return;
        }

        protected void DTCRadPanelBar1_DataBinding(object sender, EventArgs e)
        {
            return;
        }

        protected void DTCRadPanelBar1_Disposed(object sender, EventArgs e)
        {
        }

        protected void DTCRadPanelBar1_Load(object sender, EventArgs e)
        {
            return;

        }

        protected void DTCRadPanelBar1_Unload(object sender, EventArgs e)
        {

            return;
        }


    }
}