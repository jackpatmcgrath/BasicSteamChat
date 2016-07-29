using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SteamChatAndroid.Fragments;
using SteamChatCore;
using SteamChatCore.Model;
using System;
using System.Threading.Tasks;

namespace SteamChatAndroid.Activities
{
    [Activity (Label = "@string/ApplicationName", MainLauncher = true)]
    public class LoginActivity : BaseActivity, LoginFragmentListener, CaptchaFragmentListener
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            ShowLoginFragment ();
        }

        public override void OnBackPressed ()
        {
            var frag = SupportFragmentManager.FindFragmentById (Resource.Id.LoginFragmentContainer);
            if (frag is CaptchaFragment) {
                SupportActionBar.SetDisplayHomeAsUpEnabled (false);
            }
            base.OnBackPressed ();
        }

        public async Task DoLogin (string username, string password)
        {
            var result = await UserController.Instance.Login (username, password);
            LoginFragment.ToggleFields ();

            switch (result.Item2) {
                case LoginResponse.CaptchaThenSteamGuard:
                    break;
                case LoginResponse.JustCaptcha:
                    ShowCaptchaFragment ((result.Item1 as CaptchaReturn).CaptchaURL);
                    break;
                case LoginResponse.JustSteamGuard:
                    System.Diagnostics.Debug.WriteLine ("steam guard");
                    break;
                case LoginResponse.IncorrectDetails:
                    Toast.MakeText (this, Resource.String.IncorrectDetailsToastText, ToastLength.Short).Show ();
                    break;
                case LoginResponse.Success:
                    StartActivity (new Android.Content.Intent (this, typeof (MainActivity)));
                    break;
                case LoginResponse.Failed:
                    System.Diagnostics.Debug.WriteLine ("failed");
                    break;
            }
        }

        public async Task CaptchaEntered (string captcha)
        {
            System.Diagnostics.Debug.WriteLine (captcha);
        }

        void ShowLoginFragment ()
        {
            var loginFragment = LoginFragment.NewInstance ();
            ReplaceFragment (Resource.Id.LoginFragmentContainer, loginFragment, LoginFragment.MyTag).Commit ();
        }

        void ShowCaptchaFragment (string url)
        {
            var captchaFragment = CaptchaFragment.NewInstance (url);
            ReplaceFragment (Resource.Id.LoginFragmentContainer, captchaFragment, CaptchaFragment.MyTag).AddToBackStack (null).Commit ();
            SupportActionBar.SetDisplayHomeAsUpEnabled (true);
        }

        public LoginFragment LoginFragment {
            get {
                return SupportFragmentManager.FindFragmentByTag (LoginFragment.MyTag) as LoginFragment;
            }
        }

        public CaptchaFragment CaptchaFragment {
            get {
                return SupportFragmentManager.FindFragmentByTag (CaptchaFragment.MyTag) as CaptchaFragment;
            }
        }

        protected override int LayoutResource {
            get {
                return Resource.Layout.LoginActivity;
            }
        }

        protected override int ToolbarTitleResource {
            get {
                return Resource.String.LoginToolbarTitle;
            }
        }
    }
}