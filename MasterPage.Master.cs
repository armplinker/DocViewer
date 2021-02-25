using System;
using Telerik.Web.UI;
using Telerik.Web.Design;

namespace DocViewer
{
    public partial class MasterPage :System.Web.UI.MasterPage
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override void OnLoad(EventArgs e)
        {
            Logger.Debug("OnLoad Event");
            base.OnLoad(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.Debug("Page_Load Event");
        }
    }
}