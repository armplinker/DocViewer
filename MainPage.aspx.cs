using System;
using Telerik.Web.UI;

namespace DocViewer
{
    public partial class MainPage :System.Web.UI.Page
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.Debug("Page_Load Event");
            
        }
 
    }
}



