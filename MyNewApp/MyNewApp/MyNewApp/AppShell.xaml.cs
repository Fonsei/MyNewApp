using MyNewApp.Services;
using MyNewApp.ViewModels;
using MyNewApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyNewApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//LoginPage");

            DependencyService.Get<ICloseApplication>().closeApplication();
        }
    }
}
