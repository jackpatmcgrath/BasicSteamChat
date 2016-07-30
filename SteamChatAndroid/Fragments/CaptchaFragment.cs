using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using Square.Picasso;
using System.Threading.Tasks;
using SteamChatAndroid.Controls;

namespace SteamChatAndroid.Fragments
{
    public class CaptchaFragment : BaseFragment<CaptchaFragmentListener>
    {
        [InjectView (Resource.Id.CaptchaImageView)]
        ImageView captchaImageView;

        [InjectView (Resource.Id.CaptchaEditText)]
        EditText captchaField;

        public const string MyTag = "captcha_fragment";

        public static CaptchaFragment NewInstance (string username, string password, string url)
        {
            var myFragment = new CaptchaFragment ();
            var args = new Bundle ();
            args.PutString (Consts.UrlKey, url);
            args.PutString (Consts.UsernameKey, username);
            args.PutString (Consts.PasswordKey, password);
            myFragment.Arguments = args;
            return myFragment;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView (inflater, container, savedInstanceState);
            Picasso.With (Context).Load (Arguments.GetString (Consts.UrlKey)).Fit ().Into (captchaImageView);
            captchaField.EditorAction += CaptchaFieldActionEventHandler;
            return view;
        }

        public override void OnDestroyView ()
        {
            captchaField.EditorAction -= CaptchaFieldActionEventHandler;
            base.OnDestroyView ();
        }

        public void ToggleField ()
        {
            captchaField.Enabled = !captchaField.Enabled;
        }

        async void CaptchaFieldActionEventHandler (object sender, TextView.EditorActionEventArgs args)
        {
            switch (args.ActionId) {
                case Android.Views.InputMethods.ImeAction.Done:
                    ToggleField ();
                    var username = Arguments.GetString (Consts.UsernameKey);
                    var password = Arguments.GetString (Consts.PasswordKey);
                    await FragmentListener.CaptchaEntered (username, password, captchaField.Text);
                    break;
            }
        }

        public string Captcha {
            get {
                return captchaField.Text;
            }
        }

        protected override int LayoutResource {
            get {
                return Resource.Layout.CaptchaFragment;
            }
        }
    }

    public interface CaptchaFragmentListener
    {
        Task CaptchaEntered (string username, string password, string captcha);
    }
}