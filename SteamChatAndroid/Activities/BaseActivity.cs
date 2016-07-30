using Android.App;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Com.Lilarcor.Cheeseknife;
using SteamChatAndroid.Fragments;

namespace SteamChatAndroid.Activities
{
    [Activity (Label = "BaseActivity")]
    public abstract class BaseActivity : AppCompatActivity
    {
        [InjectView (Resource.Id.Toolbar)]
        protected Toolbar Toolbar;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
       
            SetContentView (LayoutResource);
            Cheeseknife.Inject (this);
            SetupToolbar ();
        }

        protected virtual void SetupToolbar ()
        {
            if (Toolbar != null) {
                SetSupportActionBar (Toolbar);
                //SupportActionBar.Title = GetString (ToolbarTitleResource);
            }
        }

        public override bool OnOptionsItemSelected (IMenuItem item)
        {
            switch (item.ItemId) {
                case (Android.Resource.Id.Home):
                    OnBackPressed ();
                    return true;
                default:
                    return base.OnOptionsItemSelected (item);
            }
        }

        public override void OnBackPressed ()
        {
            if (SupportFragmentManager.BackStackEntryCount > 0) {
                SupportFragmentManager.PopBackStack ();
            } else {
                base.OnBackPressed ();
            }
        }

        protected Android.Support.V4.App.FragmentTransaction ReplaceFragment (int containerId, Android.Support.V4.App.Fragment fragment, string tag)
        {
            return SupportFragmentManager.BeginTransaction ().Replace (containerId, fragment, tag);
        }

        protected int Navigationicon {
            set {
                Toolbar.NavigationIcon = ContextCompat.GetDrawable (this, value);
            }
        }

        protected abstract int LayoutResource { get; }

        //protected abstract int ToolbarTitleResource { get; }
    }
}