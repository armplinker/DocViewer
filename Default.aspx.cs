using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.Design;
using log4net;
namespace DocViewer
{
    public partial class Default :System.Web.UI.Page
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