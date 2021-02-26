using System;
using System.IO;
using Telerik.Web.UI;
using Telerik.Web.Design;

namespace DocViewer
{
    public partial class MasterPage :System.Web.UI.MasterPage
    {
        #region DECLARATIONS
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string DocViewerControlName { get; set; } = @"Testing/Url_Values_Dialog.aspx";
        private const string StateKeyName = @"DocViewerControlStates";
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            Logger.Debug("OnLoad Event");
            base.OnLoad(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.Debug("Page_Load Event");
            var rpmContext = "";
            try
            {
                switch (IsPostBack)
                {
                    case false:
                    {
                        Logger?.Debug("NOT a PostBack");
                        RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
                        if (persistenceManager1 == null) return;
                        persistenceManager1.StorageProviderKey = StateKeyName;
                        rpmContext = "LoadState()";
                        persistenceManager1.LoadState();


                        break;
                    }
                    case true:
                    {
                        Logger?.Debug("PostBack");
                        RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
                        if (persistenceManager1 == null) return;
                        persistenceManager1.StorageProviderKey = StateKeyName;
                        rpmContext = "SaveState()";
                        persistenceManager1.SaveState();
                        break;
                    }
                } // end switch
            }

            catch (Telerik.Web.UI.PersistenceFramework.PersistenceFrameworkArgumentException pfArgEx)
            {
                Logger?.Error($"PersistenceFrameworkArgumentException {rpmContext}", pfArgEx);
                throw;
            }

            catch (Exception ex)
            {
                //Path.GetFullPath(Path.Combine(currentDir, relPath1))
                Logger?.Error($"Persistence file {(Path.GetFullPath(Path.Combine("~\\App_Data", $"{StateKeyName}")))} not found in {rpmContext}", ex);
            }
        }

        protected void DVMRPM1_LoadCustomSettings(object sender,  PersistenceManagerLoadStateEventArgs e)
        {
            Logger?.Debug("DVMRPM1_LoadCustomSettings Event");
            return;
            //foreach (ControlSetting setting in e.CustomSettings.ControlSettings)
            // {
            //if (setting.Name == "pos")
            //    wndStateHolder.Value = (string)setting.Value;
            //}
        }

        protected void DVMRPM1_SaveCustomSettings(object sender, PersistenceManagerSaveStateEventArgs e)
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

        protected void DVM_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

            Logger?.Debug("DVM_AjaxRequest Event");
            if (e == null)
            {
                Logger?.Debug($"DVM_AjaxRequest argument is null");
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

    }
}