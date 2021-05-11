
using MyNewApp.Droid.Services;
using MyNewApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(CloseApplication))]
namespace MyNewApp.Droid.Services
{
    public class CloseApplication : ICloseApplication
    {
        //Context context = Android.App.Application.Context;
        public void closeApplication()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            //Activity activity = context;// (Activity)Forms.Context;
            //activity.FinishAffinity();
        }
    }
}