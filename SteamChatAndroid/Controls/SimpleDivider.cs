using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;

namespace SteamChatAndroid.Controls
{
    public class SimpleDivider : RecyclerView.ItemDecoration
    {
        Drawable _divider;

        public SimpleDivider (Context context)
        {
            _divider = ContextCompat.GetDrawable (context, Resource.Drawable.LineDivider);
        }

        //Fix every second row having double thickness
        public override void OnDrawOver (Canvas c, RecyclerView parent, RecyclerView.State state)
        {
            int left = parent.PaddingLeft;
            int right = parent.Width - parent.PaddingRight;
            int childCount = parent.ChildCount;
            for (int i = 0; i < childCount; i++) {
                var child = parent.GetChildAt (i);
                var layoutParams = child.LayoutParameters as RecyclerView.LayoutParams;
                var top = child.Bottom + layoutParams.BottomMargin;
                var bottom = top + _divider.IntrinsicHeight;
                _divider.SetBounds (left, top, right, bottom);
                _divider.Draw (c);
            }
        }
    }
}