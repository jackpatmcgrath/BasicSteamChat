using Android.Content;
using Android.OS;
using Android.Views;
using Com.Lilarcor.Cheeseknife;

namespace SteamChatAndroid.Fragments
{
    public abstract class BaseFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate (LayoutResource, null);
            Cheeseknife.Inject (this, view);
            return view;
        }

        public override void OnDestroyView ()
        {
            base.OnDestroyView ();
            Cheeseknife.Reset (this);
        }

        protected abstract int LayoutResource { get; }
    }

    public abstract class BaseFragment<T> : BaseFragment
    {
        protected T FragmentListener {
            get {
                return (T)(object)Context;
            }
        }
    }
}