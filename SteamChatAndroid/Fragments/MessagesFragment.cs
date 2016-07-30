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
    public class MessagesFragment : BaseFragment
    {
        public const string MyTag = "messages_fragment";

        public static MessagesFragment NewInstance ()
        {
            return new MessagesFragment ();
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView (inflater, container, savedInstanceState);
        }

        protected override int LayoutResource {
            get {
                return Resource.Layout.MessagesFragment;
            }
        }
    }
}