//using MyNewApp.Services;
using MyNewApp.Services;
using MyNewApp.Views;
using System;
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
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
            cultureInfo = CultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            LocalizationResourceManager.Current.SetCulture(CultureInfo);


            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
