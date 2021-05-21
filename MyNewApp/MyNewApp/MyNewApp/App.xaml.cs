//using MyNewApp.Services;
using MyNewApp.Helpers;
using MyNewApp.Services;
using MyNewApp.Views;
using System;
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyNewApp
{
    public partial class App : Application
    {
        static CultureInfo cultureInfo = null;
        public static CultureInfo CultureInfo
        {
            get
            {
                if (cultureInfo == null)
                {
                    var currentLocale = DependencyService.Get<ILocale>().GetCurrentLocaleId();
                    cultureInfo = new CultureInfo(currentLocale);
                }

                return cultureInfo;
            }
        }

        public App()
        {
            LocalizationResourceManager.Current.Init(MyResources.AppResources.ResourceManager);

            InitializeComponent();

            TheTheme.SetTheme();

            cultureInfo = CultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            LocalizationResourceManager.Current.SetCulture(CultureInfo);


            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
