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
    public class LoginActivity : BaseActivity, LoginFragmentListener, CaptchaFragmentListener, SteamGuardFragmentListener
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            ShowLoginFragment ();
        }

        public void HideBackButton ()
        {
            SupportActionBar.SetDisplayHomeAsUpEnabled (false);
        }

        public async Task DoLogin (string username, string password)
        {
            var result = await UserController.Instance.InitialLogin (username, password);
            LoginFragment.ToggleFields ();
            HandleLoginResponse (username, password, result);
        }

        public async Task CaptchaEntered (string username, string password, string captcha)
        {
            var result = await UserController.Instance.CaptchaLogin (username, password, captcha);
            CaptchaFragment.ToggleField ();
            HandleLoginResponse (username, password, result);
        }

        public async Task SteamGuardEntered (string username, string password, string steamGuard)
        {
            var result = await UserController.Instance.SteamGuardLogin (username, password, steamGuard);
            SteamGuardFragment.ToggleField ();
            HandleLoginResponse (username, password, result);
        }

        void HandleLoginResponse (string username, string password, Tuple<string, LoginResponse> result)
        {
            switch (result.Item2) {
                case LoginResponse.JustCaptcha:
                    ShowCaptchaFragment (username, password, result.Item1);
                    break;
                case LoginResponse.JustSteamGuard:
                    ShowSteamGuardFragment (username, password, result.Item1);
                    break;
                case LoginResponse.IncorrectDetails:
                    if (CurrentFragment is CaptchaFragment || CurrentFragment is SteamGuardFragment) {
                        RewindToLogin ();
                    }
                    Toast.MakeText (this, Resource.String.IncorrectDetailsToastText, ToastLength.Short).Show ();
                    break;
                case LoginResponse.Success:
                    StartActivity (new Android.Content.Intent (this, typeof (MainActivity)));
                    break;
                case LoginResponse.Failed:
                    Toast.MakeText (this, Resource.String.LoginFailedToastText, ToastLength.Short).Show ();
                    break;
            }
        }

        void RewindToLogin ()
        {
            for (var i = 0; i < SupportFragmentManager.BackStackEntryCount; i++) {
                SupportFragmentManager.PopBackStack ();
            }
            SupportActionBar.SetDisplayHomeAsUpEnabled (false);
        }

        void ShowLoginFragment ()
        {
            var loginFragment = LoginFragment.NewInstance ();
            ReplaceFragment (Resource.Id.LoginFragmentContainer, loginFragment, LoginFragment.MyTag).Commit ();
        }

        void ShowCaptchaFragment (string username, string password, string url)
        {
            var captchaFragment = CaptchaFragment.NewInstance (username, password, url);
            ReplaceFragment (Resource.Id.LoginFragmentContainer, captchaFragment, CaptchaFragment.MyTag).AddToBackStack (null).Commit ();
            SupportActionBar.SetDisplayHomeAsUpEnabled (true);
        }

        void ShowSteamGuardFragment (string username, string password, string email)
        {
            var steamGuardFragment = SteamGuardFragment.NewInstance (username, password, email);
            ReplaceFragment (Resource.Id.LoginFragmentContainer, steamGuardFragment, SteamGuardFragment.MyTag).AddToBackStack (null).Commit ();
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

        public SteamGuardFragment SteamGuardFragment {
            get {
                return SupportFragmentManager.FindFragmentByTag (SteamGuardFragment.MyTag) as SteamGuardFragment;
            }
        }

        public Android.Support.V4.App.Fragment CurrentFragment {
            get {
                return SupportFragmentManager.FindFragmentById (Resource.Id.LoginFragmentContainer);
            }
        }

        protected override int LayoutResource {
            get {
                return Resource.Layout.LoginActivity;
            }
        }

        //protected override int ToolbarTitleResource {
        //    get {
        //        return Resource.String.BlankToolbar;
        //    }
        //}
    }
}