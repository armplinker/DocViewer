using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocViewer.Controls;
using Telerik.Web.UI;
using Telerik.Web.Design;
using Telerik.Web.UI.PersistenceFramework;
using log4net;

namespace DocViewer
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public partial class DocViewerMain : System.Web.UI.Page
    {
        #region DECLARATIONS
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public  string DocViewerControlName  { get; set; } = @"Testing/Url_Values_Dialog.aspx";
        private const string StateKeyName = @"DocViewerMainStates";
        #endregion

        #region EVENTS
        protected void Page_Init(object sender, EventArgs e)
        {
            Logger?.Debug("Page_Init Event");
            RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            if (persistenceManager1 == null) return;

            persistenceManager1.StorageProvider = new SessionStorageProvider(StateKeyName);
            persistenceManager1.StorageProviderKey = StateKeyName;
            persistenceManager1.LoadState();
        }

        protected override void OnLoad(EventArgs e)
        {
            Logger.Debug("OnLoad Event");
            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Logger?.Debug("Page_Load Event");

            if(UcDocumentTypeChooser1?.FindControl("UCDTC_RadPersistenceManagerProxy1") is RadPersistenceManagerProxy pmProxy)
                    pmProxy.UniqueKey = "UCDTCPM1";

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
                        persistenceManager1.LoadState();
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
                        persistenceManager1.SaveState();

                    }
                    catch (Exception ex)
                    {
                        //Path.GetFullPath(Path.Combine(currentDir, relPath1))
                        Logger?.Error($"Persistence file {(Path.GetFullPath(Path.Combine("~\\App_Data", $"{StateKeyName}")))} not found during SaveState() ",ex);
                    }

                    break;
                }
            } // end switch
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Logger?.Debug("Page_Unload Event");

        }

        protected void DVMRPM1_LoadCustomSettings(object sender, Telerik.Web.UI.PersistenceManagerLoadStateEventArgs e)
        {
            Logger?.Debug("DVMRPM1_LoadCustomSettings Event");
            return;
            //foreach (ControlSetting setting in e.CustomSettings.ControlSettings)
            // {
            //if (setting.Name == "pos")
            //    wndStateHolder.Value = (string)setting.Value;
            //}
        }

        protected void DVMRPM1_SaveCustomSettings(object sender, Telerik.Web.UI.PersistenceManagerSaveStateEventArgs e)
        {
            Logger?.Debug("DVMRPM1_SaveCustomSettings Event");
            return;
            //e.CustomSettings.Add(new Telerik.Web.UI.ControlSetting() { Name = "pos", Value = wndStateHolder.Value  });
        }

        protected void DVMRPM1_LoadSettings(object sender, PersistenceManagerLoadAllStateEventArgs e)
        {
            Logger?.Debug("DVMRPM1_LoadSettings Event");
            return;
            //var gridSetting = e.Settings.FindByUniqueId("RadGrid2");
            //if (gridSetting != null)
            //{
            //    e.Settings.RemoveByUniqueId("RadGrid2");
            //}
        }

        protected void DVMRPM1_SaveSettings(object sender, PersistenceManagerSaveAllStateEventArgs e)
        {
            Logger?.Debug("DVMRPM1_SaveSettings Event");
            return;
            //var gridSetting = e.Settings.FindByUniqueId("RadGrid2");
            //if (gridSetting != null)
            //{
            //    e.Settings.RemoveByUniqueId("RadGrid2");
            //}
        }



        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Logger?.Debug("SaveButton_Click Event");
            var persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            persistenceManager1.StorageProviderKey = StateKeyName;
            persistenceManager1.SaveState();
        }


        protected void LoadButton_Click(object sender, EventArgs e)
        {
            Logger?.Debug("LoadButton_Click Event");
            var persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            persistenceManager1.StorageProviderKey = StateKeyName;
            persistenceManager1.LoadState();
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            Logger?.Debug("ResetButton_Click Event");

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

        protected void DVM_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

            Logger?.Debug("DVMRPM1_AjaxRequest Event");
            if (e == null)
            {
                Logger?.Debug($"DVMRPM1_AjaxRequest argument is null");
                return;
            }

            var context = e.Argument?.Split('*')[0]?.ToLower();
            var action = e.Argument?.Split('*')[1]?.ToLower();
            var cargo = e.Argument?.Split('*')[2];

            Logger?.Debug($"ajaxRequest Context: {context}");
            Logger?.Debug($"ajaxRequest Action: {action}");
            Logger?.Debug($"ajaxRequest Cargo: {(!string.IsNullOrEmpty(cargo) ? cargo : "no cargo")}");



            // ReSharper disable once StringLiteralTypo
            if (string.Equals(action, @"loginfo"))
            {
                Logger?.Info($"Context: {context} - In ajaxRequest {action} item - cargo {(!string.IsNullOrEmpty(cargo) ? cargo : "no cargo")}");
                if (!string.IsNullOrEmpty(cargo))
                    Logger?.Info(cargo.ToString());
                return;
            };

            // ReSharper disable once StringLiteralTypo
            if (string.Equals(action, @"logdebug"))
            {
                Logger?.Debug($"Context: {context} - In ajaxRequest {action} item - cargo {(!string.IsNullOrEmpty(cargo) ? cargo : "no cargo")}");
                if (!string.IsNullOrEmpty(cargo))
                    Logger?.Debug(cargo.ToString());
                return;
            };

            // ReSharper disable once StringLiteralTypo
            if (string.Equals(action, @"logwarn"))
            {
                Logger?.Warn($"Context: {context} - In ajaxRequest {action} item - cargo {(!string.IsNullOrEmpty(cargo) ? cargo : "no cargo")}");
                if (!string.IsNullOrEmpty(cargo))
                    Logger?.Warn(cargo.ToString());
                return;
            };

            // ReSharper disable once StringLiteralTypo
            if (string.Equals(action, @"logerror"))
            {
                Logger?.Error($"Context: {context} - In ajaxRequest {action} item - cargo {(!string.IsNullOrEmpty(cargo) ? cargo : "no cargo")}");

                if (!string.IsNullOrEmpty(cargo))
                    Logger?.Error(cargo.ToString());
                return;
            };

            if (!string.Equals(action, @"action")) return;
            if (string.Equals(cargo, @"savestate"))
            {
                Logger?.Debug($"{context} - In ajaxRequest {action} item - cargo (action) {cargo}");
                var persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
                persistenceManager1.StorageProviderKey = StateKeyName;
                persistenceManager1.SaveState();
                return;
            }

            if (!string.Equals(cargo, @"loadstate")) return;
            {
                Logger?.Debug($"{context} - In ajaxRequest item {action} -  cargo (action) {cargo}");
                var persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
                persistenceManager1.StorageProviderKey = StateKeyName;
                persistenceManager1.LoadState();
                return;
            }
        }

        #endregion
        #region METHODS
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


        private static IEnumerable<Control> AllSubControls(Control control)
            => Enumerable.Repeat(control, 1)
                .Union(control.Controls.OfType<Control>()
                    .SelectMany(AllSubControls)
                );

        [WebMethod]
        public static void LogFromJavaScript(string errorMessage)
        {
            Logger.Error($"JS: {errorMessage}");
        }

        #endregion


    }
}