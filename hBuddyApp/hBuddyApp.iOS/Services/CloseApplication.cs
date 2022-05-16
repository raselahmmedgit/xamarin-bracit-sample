using hBuddyApp.Services;
using System.Threading;

namespace hBuddyApp.iOS.Services
{
    public class CloseApplication : ICloseApplication
    {
        public void Close()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
