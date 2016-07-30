using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace SteamChatAndroid.Activities
{
    [Activity (Label = "MainActivity")]
    public class MainActivity : BaseActivity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
        }

        protected override int LayoutResource {
            get {
                return Resource.Layout.MainActivity;
            }
        }

        //protected override int ToolbarTitleResource {
        //    get {
        //        return Resource.String.LoginToolbarTitle;
        //    }
        //}
    }
}

