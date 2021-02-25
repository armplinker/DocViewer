#define TRACE
//#define DUMPGLOBALS
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;
using log4net;

namespace DocViewer
{
    public class Global : System.Web.HttpApplication
    {
        #region DECLARATIONS
        private static readonly log4net.ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        #endregion

        #region EVENTS
        public override void Init()
        {
            base.Init();
            this.Error += WebApiApplication_Error;
            this.BeginRequest += WebApiApplication_BeginRequest;
            this.EndRequest += WebApiApplication_EndRequest;

        }
        void WebApiApplication_BeginRequest(object sender, EventArgs e)
        {
#if TRACE
            Logger.Debug("WebApiApplication_BeginRequest");
#endif
        }
        void WebApiApplication_Error(object sender, EventArgs e)
        {
#if TRACE
            Logger.Debug("WebApiApplication_Error");
#endif
            Exception ex = Server.GetLastError();
            if (ex is ThreadAbortException)
            {
                Logger.Debug(ex);
                return;
            }
            Logger.Error(ex);

        }
        void WebApiApplication_EndRequest(object sender, EventArgs e)
        {
#if TRACE
            Logger.Debug("WebApiApplication_EndRequest");
#endif
        }

        protected void Application_Start(object sender, EventArgs e)
        {
#if TRACE
            Logger?.Debug("Application_Start Event");
#endif
#if DUMPGLOBALS

            Logger?.Debug("Application_Start Event");
            Logger?.Debug($"{new string('=', 80)}");
            Logger?.Debug($"{new string('=', 80)}");
            Logger?.Debug($"Dump Globals");
            Logger?.Debug($"{new string('=', 80)}");
            Logger?.Debug($"{new string('=', 80)}");
            Logger?.Debug($"{Environment.NewLine}");

            Globals.Dump();

            Logger?.Debug($"{Environment.NewLine}");
            Logger?.Debug($"{new string('=', 80)}");
            Logger?.Debug($"{new string('=', 80)}");
            Logger?.Debug($"End Globals");
            Logger?.Debug($"{new string('=', 80)}");
            Logger?.Debug($"{new string('=', 80)}");
            Logger?.Debug($"{Environment.NewLine}");
#endif

        }

        protected void Session_Start(object sender, EventArgs e)
        {
#if TRACE
            Logger?.Debug("Session_Start Event");
#endif
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
#if DEBUG
            //set the property to our new object
            // ReSharper disable once StringLiteralTypo
            log4net.LogicalThreadContext.Properties[@"activityid"] = new ActivityIdHelper();
            // ReSharper disable once StringLiteralTypo
            log4net.LogicalThreadContext.Properties["requestinfo"] = new WebRequestInfo();
#endif
#if TRACE
            Logger?.Debug("Application_BeginRequest Event");
#endif
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
#if TRACE
            Logger?.Debug("Application_AuthenticateRequest Event");
#endif
        }

        protected void Application_Error(object sender, EventArgs e)
        {
#if TRACE
            Logger?.Debug("Application_Error Event");
#endif
        }

        protected void Session_End(object sender, EventArgs e)
        {
#if TRACE
            Logger?.Debug("Session_End Event");
#endif
        }

        protected void Application_End(object sender, EventArgs e)
        {
#if TRACE
            Logger?.Debug("Application_End Event");
#endif
        }

        #endregion
    }
    //Create our little helper class
    public class ActivityIdHelper
    {
        public override string ToString()
        {
            if (Trace.CorrelationManager.ActivityId == Guid.Empty)
            {
                Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            }

            return Trace.CorrelationManager.ActivityId.ToString();
        }
    }
    public class WebRequestInfo
    {
        public override string ToString()
        {
            return $"request url: {HttpContext.Current?.Request?.RawUrl} / agent: {HttpContext.Current?.Request?.UserAgent}";
        }
    }
}