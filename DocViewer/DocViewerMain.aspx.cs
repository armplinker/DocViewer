using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.Design;

namespace DocViewer
{
    public partial class DocViewerMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RadAjaxManager manager = RadAjaxManager.GetCurrent(Page);
            //manager.ClientEvents.OnRequestStart = "onRequestStart";
            //manager.ClientEvents.OnResponseEnd = "onResponseEnd";
            //manager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(manager_AjaxRequest);

            //if (UserControl1 != null)
            //{
            //    ((RadPersistenceManagerProxy) UserControl1.FindControl("RadPersistenceManagerProxy1")).UniqueKey = "1";
            //}

            if (DocumentTypePanelBar1 != null)
            {
                ((RadPersistenceManagerProxy)DocumentTypePanelBar1.FindControl("RadPersistenceManagerProxy1"))
                    .UniqueKey = "DTPB1";


            }
        }


        protected void manager_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
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

    }
}