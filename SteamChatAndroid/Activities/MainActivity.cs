using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Com.Lilarcor.Cheeseknife;
using Java.Lang;
using SteamChatAndroid.Fragments;

namespace SteamChatAndroid.Activities
{
    [Activity (Label = "MainActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity
    {
        [InjectView (Resource.Id.TabLayout)]
        Android.Support.Design.Widget.TabLayout tabLayout;

        [InjectView (Resource.Id.ViewPager)]
        Android.Support.V4.View.ViewPager viewPager;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            var titles = new string[] { GetString (Resource.String.MessagesTabTitle), GetString (Resource.String.FriendsTabTitle) };
            viewPager.Adapter = new MainFragmentPagerAdapter (SupportFragmentManager, titles);
            tabLayout.SetupWithViewPager (viewPager);
        }

        protected override void SetupToolbar ()
        {
            base.SetupToolbar ();
            SupportActionBar.Title = GetString (Resource.String.ApplicationName);
            SupportActionBar.SetDisplayHomeAsUpEnabled (false);
        }

        protected override int LayoutResource {
            get {
                return Resource.Layout.MainActivity;
            }
        }

        //protected override int ToolbarTitleResource {
        //    get {
        //        return Resource.String.LoginToolbarTitle;
        //    }
        //}
    }

    public class MainFragmentPagerAdapter : FragmentPagerAdapter
    {
        const int PageCount = 2;
        string[] TabTitles;

        public MainFragmentPagerAdapter (Android.Support.V4.App.FragmentManager fm, string[] tabTitles)
            : base (fm)
        {
            TabTitles = tabTitles;
        }

        public override Android.Support.V4.App.Fragment GetItem (int position)
        {
            if (position == 0) {
                return MessagesFragment.NewInstance ();
            }
            return FriendsFragment.NewInstance ();
        }

        public override ICharSequence GetPageTitleFormatted (int position)
        {
            return new Java.Lang.String (TabTitles[position]);
        }

        public override int Count {
            get {
                return PageCount;
            }
        }
    }
}

