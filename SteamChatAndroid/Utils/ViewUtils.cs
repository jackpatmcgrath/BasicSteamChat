using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;

namespace SteamChatAndroid.Utils
{
    public static class ViewUtils
    {
        public static void HideKeyboard (this Activity activity)
        {
            activity.FindViewById (Android.Resource.Id.Content).RequestFocus ();
            var imm = activity.GetSystemService (Context.InputMethodService) as InputMethodManager;
            var result = imm.HideSoftInputFromWindow (activity.CurrentFocus.WindowToken, 0);
        }
    }
}