using System;

using Android.App;
using Android.Runtime;

namespace SteamChatAndroid.Controls
{
    [Application (Label = "MainApplication", Theme = "@style/AppTheme", Icon = "@drawable/Icon")]
    public class MainApplication : Application
    {
        public MainApplication (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
        {
        }

        public override void OnCreate ()
        {
            base.OnCreate ();
        }
    }
}