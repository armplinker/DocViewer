using System;
using System.Web;
using Telerik.Web.UI.PersistenceFramework;

namespace DocViewer 
{
    public class SessionStorageProvider :IStateStorageProvider
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
        static string storageKey;

        public SessionStorageProvider()
        {

        }

        public SessionStorageProvider(string storageKeySetting)
        {
            storageKey = storageKeySetting;
        }

        public static string StorageProviderKey
        {
            set { storageKey = value; }
        }

        public void SaveStateToStorage(string key, string serializedState)
        {
            session[storageKey] = serializedState;
        }

        public string LoadStateFromStorage(string key)
        {
            return session[storageKey]?.ToString();
        }

    }
}
