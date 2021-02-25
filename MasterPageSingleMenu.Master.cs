//ACTIVE_IN_MASTERPAGE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocViewer.Controls;
using Telerik.Web.UI;
using Telerik.Web.Design;

namespace DocViewer
{
    public partial class MasterPageSingleMenu : System.Web.UI.MasterPage
    {
        private static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnLoad(EventArgs e)
        {
            Logger.Debug("OnLoad Event");
            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.Debug("Page_Load Event");

        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Logger.Debug("Page_Unload Event");

        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
        }

#if ACTIVE_IN_MASTERPAGE
        protected void RadPersistenceManager1_SaveCustomSettings(object sender, Telerik.Web.UI.PersistenceManagerSaveStateEventArgs e)
        {
            Logger?.Debug("RadPersistenceManager1_SaveCustomSettings Event");
            return;
            //foreach (ControlSetting setting in e.CustomSettings.ControlSettings)
            // {
            //if (setting.Name == "pos")
            //    wndStateHolder.Value = (string)setting.Value;
            //}
        }

        protected void RadPersistenceManager1_LoadCustomSettings(object sender, Telerik.Web.UI.PersistenceManagerLoadStateEventArgs e)
        {
            Logger?.Debug("RadPersistenceManager1_LoadCustomSettings Event");
            return;
            //foreach (ControlSetting setting in e.CustomSettings.ControlSettings)
            // {
            //if (setting.Name == "pos")
            //    wndStateHolder.Value = (string)setting.Value;
            //}
        }

        protected void RadPersistenceManager1_LoadSettings(object sender, PersistenceManagerLoadAllStateEventArgs e)
        {
            Logger?.Debug("RadPersistenceManager1_LoadSettings Event");
            return;
            //var gridSetting = e.Settings.FindByUniqueId("RadGrid2");
            //if (gridSetting != null)
            //{
            //    e.Settings.RemoveByUniqueId("RadGrid2");
            //}
        }

        protected void RadPersistenceManager1_SaveSettings(object sender, PersistenceManagerSaveAllStateEventArgs e)
        {
            Logger?.Debug("RadPersistenceManager1_SaveSettings Event");
            return;
            //var gridSetting = e.Settings.FindByUniqueId("RadGrid2");
            //if (gridSetting != null)
            //{
            //    e.Settings.RemoveByUniqueId("RadGrid2");
            //}
        }
#endif
    }
}