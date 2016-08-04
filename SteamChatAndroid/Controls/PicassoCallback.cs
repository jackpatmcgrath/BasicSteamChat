using System;
using Square.Picasso;

namespace SteamChatAndroid.Controls
{
    class PicassoCallback : Java.Lang.Object, ICallback
    {
        public event EventHandler ImageLoaded;
        public event EventHandler ImageFailed;

        public void OnError ()
        {
            ImageFailed?.Invoke (this, null);
        }

        public void OnSuccess ()
        {
            ImageLoaded?.Invoke (this, null);
        }
    }
}