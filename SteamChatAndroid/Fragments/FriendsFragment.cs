using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Com.Lilarcor.Cheeseknife;
using SteamChatAndroid.Controls;
using SteamChatAndroid.ViewHolders;
using SteamChatCore.Controllers;
using SteamChatCore.Model;
using System.Threading.Tasks;

namespace SteamChatAndroid.Fragments
{
    public class FriendsFragment : BaseFragment
    {
        [InjectView (Resource.Id.FriendsRecyclerView)]
        RecyclerView friendsRecyclerView;

        public const string MyTag = "friends_fragment";

        public static FriendsFragment NewInstance ()
        {
            return new FriendsFragment ();
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView (inflater, container, savedInstanceState);
            var task = SetupRecyclerView ();
            return view;
        }

        async Task SetupRecyclerView ()
        {
            await ChatController.Instance.UpdateFriends ();
            var adapter = new GenericAdapter<SteamUser, FriendViewHolder> (ChatController.Instance.Friends, Resource.Layout.FriendViewHolder);
            adapter.ItemClick += Adapter_ItemClick;
            friendsRecyclerView.SetAdapter (adapter);
            friendsRecyclerView.SetLayoutManager (new LinearLayoutManager (Context));
            friendsRecyclerView.AddItemDecoration (new SimpleDivider (Context));
        }

        private void Adapter_ItemClick (object sender, ItemClickEventArgs<SteamUser> e)
        {
            System.Diagnostics.Debug.WriteLine (e.Item.SteamID.ToString ());
        }

        protected override int LayoutResource {
            get {
                return Resource.Layout.FriendsFragment;
            }
        }
    }
}