using Android.App;
using Android.OS;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using SteamChatCore;
using SteamChatCore.Model;
using System;

namespace SteamChatAndroid
{
    [Activity (Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        [InjectView (Resource.Id.UsernameEditText)]
        EditText usernameField;

        [InjectView (Resource.Id.PasswordEditText)]
        EditText passwordField;

        [InjectView (Resource.Id.LoginButton)]
        Button loginButton;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.LoginActivity);
            Cheeseknife.Inject (this);
        }

        void ToggleFields ()
        {
            loginButton.Enabled = passwordField.Enabled = usernameField.Enabled = !usernameField.Enabled;
        }

        [InjectOnClick (Resource.Id.LoginButton)]
        async void LoginButtonHandler (object sender, EventArgs e)
        {
            ToggleFields ();
            var result = await UserController.Instance.Login (usernameField.Text, passwordField.Text);
            ToggleFields ();

            switch (result.Item2) {
                case LoginResponse.CaptchaThenSteamGuard:
                    System.Diagnostics.Debug.WriteLine (result.Item1[0]);
                    break;
                case LoginResponse.JustCaptcha:
                    System.Diagnostics.Debug.WriteLine (result.Item1[0]);
                    break;
                case LoginResponse.JustSteamGuard:
                    System.Diagnostics.Debug.WriteLine ("steam guard");
                    break;
                case LoginResponse.IncorrectDetails:
                    Toast.MakeText (this, Resource.String.IncorrectDetailsToastText, ToastLength.Short);
                    break;
                case LoginResponse.Success:
                    StartActivity (new Android.Content.Intent (this, typeof (MainActivity)));
                    break;
            }
        }
    }
}