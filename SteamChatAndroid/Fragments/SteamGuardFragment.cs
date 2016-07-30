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
            steamGuardField.EditorAction += SteamGuardFieldActionEventHandler;
            return view;
        }

        public override void OnDestroyView ()
        {
            steamGuardField.EditorAction -= SteamGuardFieldActionEventHandler;
            base.OnDestroyView ();
        }

        public void ToggleField ()
        {
            steamGuardField.Enabled = !steamGuardField.Enabled;
        }

        async void SteamGuardFieldActionEventHandler (object sender, TextView.EditorActionEventArgs args)
        {
            switch (args.ActionId) {
                case Android.Views.InputMethods.ImeAction.Done:
                    ToggleField ();
                    var username = Arguments.GetString (Consts.UsernameKey);
                    var password = Arguments.GetString (Consts.PasswordKey);
                    await FragmentListener.SteamGuardEntered (username, password, steamGuardField.Text);
                    break;
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