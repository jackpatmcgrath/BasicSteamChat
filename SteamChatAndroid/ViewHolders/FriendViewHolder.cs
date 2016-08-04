using SteamChatCore.Model;
using SteamChatAndroid.Controls;
using Android.Views;
using System;
using Com.Lilarcor.Cheeseknife;
using Android.Widget;
using Square.Picasso;

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
            Picasso.With (ItemView.Context).Load (item.AvatarMediumURL).NoFade ().CenterCrop ().Fit ().Into (avatarImage);
            primarylabel.Text = BuildPrimaryLabel (item);
            lastMessageLabel.Text = "todo";
            friendStatusLabel.Text = item.PersonaState.PersonaStateToString ();
        }

        string BuildPrimaryLabel (SteamUser item)
        {
            var baseString = item.PersonaName + (string.IsNullOrEmpty (item.RealName) ? "" : string.Format (" ({0})", item.RealName)) + " \u25CF ";

            if (item.PersonaState == PersonaState.Online && string.IsNullOrEmpty (item.GameExtraInfo)) {
                return baseString + item.GameExtraInfo;
            }

            if (item.PersonaState == PersonaState.Offline) {
                return baseString + item.LastLogOff.ToLongTimeString ();
            }

            return item.PersonaName;
        }
    }
}