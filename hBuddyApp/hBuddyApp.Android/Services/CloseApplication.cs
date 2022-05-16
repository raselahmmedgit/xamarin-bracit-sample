using Android.App;
using hBuddyApp.Services;

namespace hBuddyApp.Droid.Services
{
    public class CloseApplication : ICloseApplication
    {
        public void Close()
        {
            var activity = (Activity)Xamarin.Forms.Forms.Context;
            activity.FinishAffinity();
        }
    }
}
