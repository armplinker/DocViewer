using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DocViewer
{
    public partial class ListView : System.Web.UI.Page
    {
        #region DECLARATIONS
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string StateKeyName = @"DocViewerControlStates";

        public List<Image> Images
        {
            get
            {
                List<Image> data = Session["DataImages"] as List<Image>;

                if (data == null)
                {
                    data = GetImages();
                    Session["DataImages"] = data;
                }

                return data;
            }
        }

        public List<Image> GetImages()
        {
            return new List<Image>() {
            new Image() { ID=1, Name="Chrysanthemum", ImageUrl="~/images/Chrysanthemum.jpg", ThumbnailUrl="~/images/Thumbnails/Chrysanthemum.jpg", Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"},
            new Image() { ID=2, Name="Desert", ImageUrl="~/images/Desert.jpg", ThumbnailUrl="~/images/Thumbnails/Desert.jpg", Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"},
            new Image() { ID=3, Name="Hydrangeas", ImageUrl="~/images/Hydrangeas.jpg", ThumbnailUrl="~/images/Thumbnails/Hydrangeas.jpg", Description="The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English."},
            new Image() { ID=4, Name="Jellyfish", ImageUrl="~/images/Jellyfish.jpg", ThumbnailUrl="~/images/Thumbnails/Jellyfish.jpg", Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"},
            new Image() { ID=5, Name="Koala", ImageUrl="~/images/Koala.jpg", ThumbnailUrl="~/images/Thumbnails/Koala.jpg", Description="The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English"},
            new Image() { ID=6, Name="Lighthouse", ImageUrl="~/images/Lighthouse.jpg", ThumbnailUrl="~/images/Thumbnails/Lighthouse.jpg", Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"},
            new Image() { ID=7, Name="Penguins", ImageUrl="~/images/Penguins.jpg", ThumbnailUrl="~/images/Thumbnails/Penguins.jpg", Description="The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English"},
            new Image() { ID=8, Name="Tulips", ImageUrl="~/images/Tulips.jpg", ThumbnailUrl="~/images/Thumbnails/Tulips.jpg", Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"}
        };
        }

        public List<Article> Articles
        {
            get
            {
                List<Article> data = Session["DataArticles"] as List<Article>;

                if (data == null)
                {
                    data = GetArticles();
                    Session["DataArticles"] = data;
                }

                return data;
            }
        }

        public List<Article> GetArticles()
        {
            return new List<Article>() {
            new Article(){ ID=1, Title="Phasellus auctor nisi dolor", Description="Donec aliquam turpis sed nisl mattis sagittis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Ut vitae sapien metus. In hac habitasse platea dictumst. Aenean velit mauris, lobortis eu lacinia sed, imperdiet quis dui. Vestibulum ut metus sagittis dui lacinia mollis ornare eget urna. Mauris vehicula scelerisque sagittis"},
            new Article(){ ID=1, Title="In magna mi, dapibus ut tortor", Description="Nullam facilisis neque ut aliquet imperdiet. Mauris ut odio augue. Curabitur in mi ac odio vestibulum lobortis. Donec sed mollis nibh, vitae varius lorem. Fusce ac neque lacinia dui facilisis posuere. Fusce pharetra rhoncus vulputate"},
            new Article(){ ID=1, Title="Aenean ut lacus enim", Description="Aenean euismod est tortor, et pharetra mauris ultricies ut. Vivamus fringilla libero nec est tincidunt, sit amet venenatis felis accumsan. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae"},
            new Article(){ ID=1, Title="Aenean luctus bibendum", Description="Mauris blandit sit amet diam eget dictum. Ut sit amet tortor sit amet nibh elementum scelerisque. Nullam felis turpis, suscipit ut nunc at, euismod condimentum massa. Ut finibus odio vitae euismod dapibus. Fusce luctus elit leo, at ultrices orci imperdiet quis"},
            new Article(){ ID=1, Title="Lorem ipsum dolor sit amet", Description="Etiam nisi felis, ullamcorper et sagittis sed, posuere et lorem. Mauris non rutrum tortor. Suspendisse eu leo nec justo facilisis imperdiet sed vel felis. Nullam eros urna, efficitur in eros at, interdum iaculis massa"}
        };
        }
        #endregion

        #region EVENTS
        protected void RadListViewImages_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            Logger?.Debug("RadListViewImages_NeedDataSource Event");
            RadListViewImages.DataSource = Images;
        }

        protected override void OnLoad(EventArgs e)
        {
            Logger.Debug("OnLoad Event");
            base.OnLoad(e);

            RadLightBoxImageDetails.DataSource = Images;
            RadLightBoxImageDetails.DataBind();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Logger?.Debug("Page_Init Event");
            RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
            if (persistenceManager1 == null) return;

            persistenceManager1.StorageProvider = new SessionStorageProvider(StateKeyName);
            persistenceManager1.StorageProviderKey = StateKeyName;
            persistenceManager1.LoadState();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (IsPostBack)
            {
                case false:
                    {
                        Logger?.Debug("NOT a PostBack");
                        RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
                        if (persistenceManager1 == null) return;
                        persistenceManager1.StorageProviderKey = StateKeyName;
                        try
                        {
                            persistenceManager1.LoadState();
                        }
                        catch (Exception ex)
                        {
                            //Path.GetFullPath(Path.Combine(currentDir, relPath1))
                            Logger?.Error(
                                $"Persistence file {(Path.GetFullPath(Path.Combine("~\\App_Data", $"{StateKeyName}")))} not found during LoadState() ",
                                ex);
                        }

                        break;
                    }
                case true:
                    {
                        Logger?.Debug("PostBack");
                        RadPersistenceManager persistenceManager1 = RadPersistenceManager.GetCurrent(Page);
                        if (persistenceManager1 == null) return;
                        persistenceManager1.StorageProviderKey = StateKeyName;

                        try
                        {
                            persistenceManager1.SaveState();

                        }
                        catch (Exception ex)
                        {
                            //Path.GetFullPath(Path.Combine(currentDir, relPath1))
                            Logger?.Error(
                                $"Persistence file {(Path.GetFullPath(Path.Combine("~\\App_Data", $"{StateKeyName}")))} not found during SaveState() ",
                                ex);
                        }

                        break;
                    }
            } // end switch
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Logger?.Debug("Page_Unload Event");
        }

        protected void RadListViewImages_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
        {
            RadListViewDataItem item = e.Item as RadListViewDataItem;
            string description = (item?.DataItem as Image)?.Description ?? string.Empty;
            if (description.Length <= 100) return;
            description = description.Substring(0, 97) + "...";
            if (item?.FindControl("LabelShortDescription") is Literal tgt)
                tgt.Text = description;
        }

        protected void RadListViewArticles_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            RadListViewArticles.DataSource = Articles;
        }

        protected void LVRPM1_LoadCustomSettings(object sender, Telerik.Web.UI.PersistenceManagerLoadStateEventArgs e)
        {
            Logger?.Debug("LVRPM1_LoadCustomSettings Event");
            return;
            //foreach (ControlSetting setting in e.CustomSettings.ControlSettings)
            // {
            //if (setting.Name == "pos")
            //    wndStateHolder.Value = (string)setting.Value;
            //}
        }

        protected void LVRPM1_SaveCustomSettings(object sender, Telerik.Web.UI.PersistenceManagerSaveStateEventArgs e)
        {
            Logger?.Debug("LVRPM1_SaveCustomSettings Event");
            return;
            //e.CustomSettings.Add(new Telerik.Web.UI.ControlSetting() { Name = "pos", Value = wndStateHolder.Value  });
        }

        protected void LVRPM1_LoadSettings(object sender, PersistenceManagerLoadAllStateEventArgs e)
        {
            Logger?.Debug("LVRPM1_LoadSettings Event");
            return;
            //var gridSetting = e.Settings.FindByUniqueId("RadGrid2");
            //if (gridSetting != null)
            //{
            //    e.Settings.RemoveByUniqueId("RadGrid2");
            //}
        }

        protected void LVRPM1_SaveSettings(object sender, PersistenceManagerSaveAllStateEventArgs e)
        {
            Logger?.Debug("LVRPM1_SaveSettings Event");
            return;
            //var gridSetting = e.Settings.FindByUniqueId("RadGrid2");
            //if (gridSetting != null)
            //{
            //    e.Settings.RemoveByUniqueId("RadGrid2");
            //}
        }

        protected void LV_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            Logger?.Debug("RadAjaxManager1_AjaxRequest Event");
            if (e == null)
            {
                Logger?.Debug($"RadAjaxManager1_AjaxRequest argument is null");
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

            if (string.Equals(action, @"action"))
            {

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
        #endregion
    }
}