using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using System.Threading.Tasks;

namespace SteamChatAndroid.Fragments
{
    public class LoginFragment : BaseFragment<LoginFragmentListener>
    {
        [InjectView (Resource.Id.UsernameEditText)]
        EditText usernameField;

        [InjectView (Resource.Id.PasswordEditText)]
        EditText passwordField;

        [InjectView (Resource.Id.LoginButton)]
        Button loginButton;

        public const string MyTag = "login_fragment";

        public static LoginFragment NewInstance ()
        {
            return new LoginFragment ();
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView (inflater, container, savedInstanceState);
            FragmentListener.HideBackButton ();
            return view;
        }

        public override void OnViewStateRestored (Bundle savedInstanceState)
        {
            base.OnViewStateRestored (savedInstanceState);
            passwordField.Text = "";
        }

        public void ToggleFields ()
        {
            loginButton.Enabled = passwordField.Enabled = usernameField.Enabled = !usernameField.Enabled;
        }

        [InjectOnClick (Resource.Id.LoginButton)]
        async void LoginButtonHandler (object sender, EventArgs e)
        {
            ToggleFields ();
            await FragmentListener.DoLogin (usernameField.Text, passwordField.Text);
        }

        protected override int LayoutResource {
            get {
                return Resource.Layout.LoginFragment;
            }
        }
    }

    public interface LoginFragmentListener
    {
        Task DoLogin (string username, string password);

        void HideBackButton ();
    }
}