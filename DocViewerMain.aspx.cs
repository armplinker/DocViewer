using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocViewer.Controls;
using Telerik.Web.UI;
using Telerik.Web.Design;
using Telerik.Web.UI.PersistenceFramework;

namespace DocViewer
{
    public partial class DocViewerMain : System.Web.UI.Page
    {
        public string DocViewerControlName { get; set; } = @"Testing/Url_Values_Dialog.aspx";

        protected void Page_Init(object sender, EventArgs e)
        {
            RadPersistenceManager1.StorageProvider = new SessionStorageProvider();
            RadPersistenceManager1.StorageProviderKey = "DocViewerMainStates";
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                SessionStorageProvider.StorageProviderKey = "DocViewerMainStates";
                RadPersistenceManager1.LoadState();
            }
        }

        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    SessionStorageProvider.StorageProviderKey = "DocViewerMainStates";
        //    RadPersistenceManager1.SaveState();
        //}

        protected void Manager_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            //handle the manager AjaxRequest event here
        }

        public bool CheckDocTypeVisibility(string docTypeKey, string userRole)
        {
            var visibleToUser = true;

            return visibleToUser;
        }

        /// <summary>
        ///  for a given user role, see if it can see the particular menu item and if so, whether it is enabled. 
        /// </summary>
        /// <param name="docTypeKey"></param>
        /// <param name="roleKey"></param>
        /// <param name="isVisible"></param>
        /// <param name="isEnabled"></param>
        public void CheckDocTypeMenuItemAccess(string docTypeKey, string roleKey, out bool isVisible, out bool isEnabled)
        {
            var visibleToUserRole = true && !string.Equals(docTypeKey, "0000");
            var enabledForUserRole = true;

            isVisible = visibleToUserRole;
            isEnabled = isVisible && enabledForUserRole; // if invisible, means disabled!

        }

        protected void RadPersistenceManager1_LoadCustomSettings(object sender, Telerik.Web.UI.PersistenceManagerLoadStateEventArgs e)
        {
            return;
            //foreach (ControlSetting setting in e.CustomSettings.ControlSettings)
            // {
            //if (setting.Name == "pos")
            //    wndStateHolder.Value = (string)setting.Value;
            //}
        }

        protected void RadPersistenceManager1_SaveCustomSettings(object sender, Telerik.Web.UI.PersistenceManagerSaveStateEventArgs e)
        {
            return;
            //e.CustomSettings.Add(new Telerik.Web.UI.ControlSetting() { Name = "pos", Value = wndStateHolder.Value  });
        }

        protected void RadPersistenceManager1_LoadSettings(object sender, PersistenceManagerLoadAllStateEventArgs e)
        {
            return;
            //var gridSetting = e.Settings.FindByUniqueId("RadGrid2");
            //if (gridSetting != null)
            //{
            //    e.Settings.RemoveByUniqueId("RadGrid2");
            //}
        }

        protected void RadPersistenceManager1_SaveSettings(object sender, PersistenceManagerSaveAllStateEventArgs e)
        {
            return;
            //var gridSetting = e.Settings.FindByUniqueId("RadGrid2");
            //if (gridSetting != null)
            //{
            //    e.Settings.RemoveByUniqueId("RadGrid2");
            //}
        }



        protected void SaveButton_Click(object sender, EventArgs e)
        {
         
            var persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            persistenceManager1.StorageProviderKey = "DocViewerMainStates";
            persistenceManager1.SaveState();
        }


        protected void LoadButton_Click(object sender, EventArgs e)
        {
            var persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            persistenceManager1.StorageProviderKey = "DocViewerMainStates";
            persistenceManager1.LoadState();
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {

            AllSubControls(this).ToList()
                .ForEach(c =>
                {
                    switch (c)
                    {
                        case RadPanelBar rpb:
                        {
                            rpb.CollapseAllItems();
                            rpb.ClearSelectedItems();
                            break;
                        }
                        case RadTextBox rtb:
                        {
                            rtb.Text = string.Empty;
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }


                });

        }

        private static IEnumerable<Control> AllSubControls(Control control)
            => Enumerable.Repeat(control, 1)
                .Union(control.Controls.OfType<Control>()
                    .SelectMany(AllSubControls)
                );

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            var persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            persistenceManager1.StorageProviderKey = "DocViewerMainStates";
            persistenceManager1.SaveState();
        }

    }
}