using System;
using System.IO;
using MyNewApp.Droid.Services;
using MyNewApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(PathService))]
namespace MyNewApp.Droid.Services
{
    class PathService : IPathService
    {
        public string GetImagePath()
        {
            return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
        }

        public string GetPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }
    }
}