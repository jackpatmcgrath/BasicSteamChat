using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace SteamChatAndroid.Fragments
{
    public class FriendsFragment : BaseFragment
    {
        public const string MyTag = "friends_fragment";

        public static FriendsFragment NewInstance ()
        {
            return new FriendsFragment ();
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView (inflater, container, savedInstanceState);
        }

        protected override int LayoutResource {
            get {
                return Resource.Layout.FriendsFragment;
            }
        }
    }
}