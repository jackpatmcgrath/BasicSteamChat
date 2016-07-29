using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using Square.Picasso;
using System.Threading.Tasks;

namespace SteamChatAndroid.Fragments
{
    public class CaptchaFragment : BaseFragment<CaptchaFragmentListener>
    {
        [InjectView (Resource.Id.CaptchaImageView)]
        ImageView captchaImageView;

        [InjectView (Resource.Id.CaptchaEditText)]
        EditText captchaField;

        public const string MyTag = "captcha_fragment";

        const string UrlKey = "url_key";

        public static CaptchaFragment NewInstance (string url)
        {
            var myFragment = new CaptchaFragment ();
            var args = new Bundle ();
            args.PutString (UrlKey, url);
            myFragment.Arguments = args;
            return myFragment;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView (inflater, container, savedInstanceState);
            Picasso.With (Context).Load (Arguments.GetString (UrlKey)).Fit ().Into (captchaImageView);
            captchaField.EditorAction += CaptchaFieldActionEventHandler;
            return view;
        }

        async void CaptchaFieldActionEventHandler (object sender, TextView.EditorActionEventArgs args)
        {
            switch (args.ActionId) {
                case Android.Views.InputMethods.ImeAction.Done:
                    await FragmentListener.CaptchaEntered (captchaField.Text);
                    break;
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
        Task CaptchaEntered (string captcha);
    }
}