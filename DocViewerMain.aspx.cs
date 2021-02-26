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
        public string DocViewerControlName { get; set; } = @"Testing/Url_Values_Dialog.aspx";
        private const string StateKeyName = @"DocViewerControlStates";
        #endregion

        #region EVENTS
        protected void Page_Init(object sender, EventArgs e)
        {
            Logger?.Debug("Page_Init Event");
            //RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            //if (persistenceManager1 == null) return;

            //persistenceManager1.StorageProvider = new SessionStorageProvider(StateKeyName);
            //persistenceManager1.StorageProviderKey = StateKeyName;
            //persistenceManager1.LoadState();
        }

        protected override void OnLoad(EventArgs e)
        {
            Logger.Debug("OnLoad Event");
            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Logger?.Debug("DocViewerMain Page_Load Event");

            //if (UserControl1?.FindControl("UCDEMO_RadPersistenceManagerProxy1") is RadPersistenceManagerProxy pmProxy1)
            //    pmProxy1.UniqueKey = "UCDEMORRPM1";

            //if (UcDocumentTypeChooser1?.FindControl("UCDTC_RadPersistenceManagerProxy1") is RadPersistenceManagerProxy pmProxy2)
            //    pmProxy2.UniqueKey = "UCDTCPM1";

            //var rpmContext = "";
            //try
            //{
            //    switch (IsPostBack)
            //    {
            //        case false:
            //            {
            //                Logger?.Debug("NOT a PostBack");
            //                RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            //                if (persistenceManager1 == null) return;
            //                persistenceManager1.StorageProviderKey = StateKeyName;
            //                rpmContext = "LoadState()";
            //                persistenceManager1.LoadState();


            //                break;
            //            }
            //        case true:
            //            {
            //                Logger?.Debug("PostBack");
            //                RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            //                if (persistenceManager1 == null) return;
            //                persistenceManager1.StorageProviderKey = StateKeyName;
            //                rpmContext = "SaveState()";
            //                persistenceManager1.SaveState();
            //                break;
            //            }
            //    } // end switch
            //}

            //catch (Telerik.Web.UI.PersistenceFramework.PersistenceFrameworkArgumentException pfArgEx)
            //{
            //    Logger?.Error($"PersistenceFrameworkArgumentException {rpmContext}", pfArgEx);
            //    throw;
            //}

            //catch (Exception ex)
            //{
            //    //Path.GetFullPath(Path.Combine(currentDir, relPath1))
            //    Logger?.Error($"Persistence file {(Path.GetFullPath(Path.Combine("~\\App_Data", $"{StateKeyName}")))} not found in {rpmContext}", ex);
            //}
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Logger?.Debug("DocViewerMain Page_Unload Event");

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