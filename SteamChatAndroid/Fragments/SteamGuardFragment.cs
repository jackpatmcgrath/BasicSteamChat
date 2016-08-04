using System;
using Android.App;
using Android.OS;
using Android.Views;
using Com.Lilarcor.Cheeseknife;
using Android.Widget;
using SteamChatAndroid.Controls;
using System.Threading.Tasks;

namespace SteamChatAndroid.Fragments
{
    public class SteamGuardFragment : BaseFragment<SteamGuardFragmentListener>
    {
        [InjectView (Resource.Id.SteamGuardTextView)]
        TextView steamGuardLabel;

        [InjectView (Resource.Id.SteamGuardEditText)]
        EditText steamGuardField;

        public const string MyTag = "steam_guard_fragment";

        public static SteamGuardFragment NewInstance (string username, string password, string email)
        {
            var myFragment = new SteamGuardFragment ();
            var args = new Bundle ();
            args.PutString (Consts.SteamGuardEmailKey, email);
            args.PutString (Consts.UsernameKey, username);
            args.PutString (Consts.PasswordKey, password);
            myFragment.Arguments = args;
            return myFragment;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView (inflater, container, savedInstanceState);
            steamGuardLabel.Text = string.Format (GetString (Resource.String.SteamGuardLabelCaption), Arguments.GetString (Consts.SteamGuardEmailKey));
            steamGuardField.EditorAction += SteamGuardFieldActionHandler;
            return view;
        }

        public override void OnDestroyView ()
        {
            steamGuardField.EditorAction -= SteamGuardFieldActionHandler;
            base.OnDestroyView ();
        }

        public void ToggleField ()
        {
            steamGuardField.Enabled = !steamGuardField.Enabled;
        }

        async void SteamGuardFieldActionHandler (object sender, TextView.EditorActionEventArgs args)
        {
            if (args.ActionId == Android.Views.InputMethods.ImeAction.Go) {
                ToggleField ();
                var username = Arguments.GetString (Consts.UsernameKey);
                var password = Arguments.GetString (Consts.PasswordKey);
                await FragmentListener.SteamGuardEntered (username, password, steamGuardField.Text);
            }
        }

        protected override int LayoutResource {
            get {
                return Resource.Layout.SteamGuardFragment;
            }
        }
    }

    public interface SteamGuardFragmentListener
    {
        Task SteamGuardEntered (string username, string password, string steamGuard);
    }
}