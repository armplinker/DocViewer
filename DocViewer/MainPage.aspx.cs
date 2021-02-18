using System;
using Telerik.Web.UI;

namespace DocViewer
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (UserControl1 != null)
            //{
            //    ((RadPersistenceManagerProxy) UserControl1.FindControl("RadPersistenceManagerProxy1")).UniqueKey = "1";
            //}

            if (DocumentTypePanelBar1 != null)
            {
                ((RadPersistenceManagerProxy)DocumentTypePanelBar1.FindControl("RadPersistenceManagerProxy1")).UniqueKey = "DTPB1";
            }
        }

        public bool CheckDocTypeVisibility(string docTypeKey, string userRole)
        {
            var visibleToUser = true;

            return visibleToUser;
        }
    }
}



