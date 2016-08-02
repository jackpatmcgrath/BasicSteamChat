using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using SteamChatAndroid.Fragments;
using SteamChatAndroid.Utils;
using SteamChatCore;
using SteamChatCore.Controllers;
using SteamChatCore.Model;
using System;
using System.Threading.Tasks;

namespace SteamChatAndroid.Activities
{
    [Activity (Label = "@string/ApplicationName", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true)]
    public class LoginActivity : BaseActivity, LoginFragmentListener, CaptchaFragmentListener, SteamGuardFragmentListener
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            ShowLoginFragment ();
        }

        protected override void SetupToolbar ()
        {
            base.SetupToolbar ();
            SupportActionBar.Title = "";
        }

        public void HideBackButton ()
        {
            SupportActionBar.SetDisplayHomeAsUpEnabled (false);
        }

        public async Task DoLogin (string username, string password)
        {
            this.HideKeyboard ();
            var result = await LoginController.Instance.InitialLogin (username, password);
            await HandleLoginResponse (username, password, result);
            LoginFragment.ToggleFields ();
        }

        public async Task CaptchaEntered (string username, string password, string captcha)
        {
            var result = await LoginController.Instance.CaptchaLogin (username, password, captcha);
            await HandleLoginResponse (username, password, result);
            CaptchaFragment.ToggleField ();
        }

        public async Task SteamGuardEntered (string username, string password, string steamGuard)
        {
            var result = await LoginController.Instance.SteamGuardLogin (username, password, steamGuard);
            await HandleLoginResponse (username, password, result);
            SteamGuardFragment.ToggleField ();
        }

        async Task HandleLoginResponse (string username, string password, Tuple<string, LoginResponse> result)
        {
            switch (result.Item2) {
                case LoginResponse.JustCaptcha:
                    ShowCaptchaFragment (username, password, result.Item1);
                    break;
                case LoginResponse.JustSteamGuard:
                    ShowSteamGuardFragment (username, password, result.Item1);
                    break;
                case LoginResponse.IncorrectDetails:
                    RewindToLoginIfNecessary ();
                    ShowErrorToast (Resource.String.IncorrectDetailsToastText);
                    break;
                case LoginResponse.Success:
                    LoginController.Instance.Reset ();
                    if (await ChatController.Instance.LogOntoChat ()) {
                        StartActivity (new Android.Content.Intent (this, typeof (MainActivity)));
                    } else {
                        RewindToLoginIfNecessary ();
                        ShowErrorToast (Resource.String.LoginFailedToastText);
                    }
                    break;
                case LoginResponse.Failed:
                    ShowErrorToast (Resource.String.LoginFailedToastText);
                    break;
            }
        }

        void ShowErrorToast (int messageId)
        {
            Toast.MakeText (this, messageId, ToastLength.Short).Show ();
        }

        void RewindToLoginIfNecessary ()
        {
            if (CurrentFragment is CaptchaFragment || CurrentFragment is SteamGuardFragment) {
                for (var i = 0; i < SupportFragmentManager.BackStackEntryCount; i++) {
                    SupportFragmentManager.PopBackStack ();
                }
                SupportActionBar.SetDisplayHomeAsUpEnabled (false);
            }
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