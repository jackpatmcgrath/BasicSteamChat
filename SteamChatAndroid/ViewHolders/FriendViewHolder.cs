using SteamChatCore.Model;
using SteamChatAndroid.Controls;
using Android.Views;
using System;
using Com.Lilarcor.Cheeseknife;
using Android.Widget;

namespace SteamChatAndroid.ViewHolders
{
    class FriendViewHolder : GenericViewHolder<SteamUser>
    {
        [InjectView (Resource.Id.FriendSteamIDLabel)]
        TextView steamIdLabel;

        public FriendViewHolder (View itemView) : base (itemView)
        {
        }

        public override void SetItem (SteamUser item)
        {
            steamIdLabel.Text = item.SteamID.ToString ();
        }
    }
}