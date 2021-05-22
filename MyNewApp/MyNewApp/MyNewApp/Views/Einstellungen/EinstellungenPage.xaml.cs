using MyNewApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;

namespace MyNewApp.Views.Einstellungen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EinstellungenPage : ContentPage
    {
        public EinstellungenPage()
        {
            InitializeComponent();

            switch (Settings.Theme)
            {
                case 0:
                    RadioButtonSystem.IsChecked = true;
                    break;
                case 1:
                    RadioButtonLight.IsChecked = true;
                    break;
                case 2:
                    RadioButtonDark.IsChecked = true;
                    break;
                default:
                    break;
            }
        }
        bool loaded;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            loaded = true;
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!loaded)
                return;

            if (!e.Value)
                return;

            var val = (sender as RadioButton)?.Value as string;
            if (string.IsNullOrWhiteSpace(val))
                return;

            switch (val)
            {
                case "System":
                    Settings.Theme = 0;
                    break;
                case "Light":
                    Settings.Theme = 1;
                    break;
                case "Dark":
                    Settings.Theme = 2;
                    break;
                default:
                    break;
            }

            TheTheme.SetTheme();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            var actions = new SnackBarActionOptions
            {
                Action = () => DisplayAlert("Test","Hallo","OK"),
                Text = "OK",
                ForegroundColor = Color.White,
                BackgroundColor = Color.Gray, 
                Padding = new Thickness(10)
            };

            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = "Willst du das ?"
                },
                BackgroundColor = Color.Gray,
                Duration = TimeSpan.FromSeconds(10),
                Actions = new[] { actions },
                IsRtl = true
            };

            await this.DisplaySnackBarAsync(options);


            //var toast = new ToastOptions
            //{
            //    BackgroundColor = Color.Green,
            //    Duration = TimeSpan.FromSeconds(10),
            //    MessageOptions = new MessageOptions
            //    {
            //        Foreground = Color.White,
            //        Message = "Hallo Welt"
            //    }
            //};

            //await this.DisplayToastAsync(toast);
        }
    }
}