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
        protected void Page_Load(object sender, EventArgs e)
        {

             

             

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
    }
}