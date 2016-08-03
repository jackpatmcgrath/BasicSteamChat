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
        [InjectView (Resource.Id.FriendProfileImage)]
        Refractored.Controls.CircleImageView avatarImage;

        [InjectView (Resource.Id.FriendPrimaryLabel)]
        TextView primarylabel;

        [InjectView (Resource.Id.FriendLastMessageLabel)]
        TextView lastMessageLabel;

        [InjectView (Resource.Id.FriendStatusLabel)]
        TextView friendStatusLabel;

        public FriendViewHolder (View itemView) : base (itemView)
        {
        }

        public override void SetItem (SteamUser item)
        {
            primarylabel.Text = BuildPrimaryLabel (item);
            lastMessageLabel.Text = "todo";
            friendStatusLabel.Text = item.PersonaState.ToString ();
        }

        string BuildPrimaryLabel (SteamUser item)
        {
            var baseString = item.PersonaName + " \u25CF ";

            if (item.PersonaState == PersonaState.Online && item.GameExtraInfo != null) {
                return baseString + item.GameExtraInfo;
            }

            if (item.PersonaState == PersonaState.Offline) {
                return baseString + item.LastLogOff.ToLongTimeString ();
            }

            return item.PersonaName;
        }
    }
}