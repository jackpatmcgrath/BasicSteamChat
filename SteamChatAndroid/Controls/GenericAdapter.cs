using System;
using System.Collections.Generic;
using Android.Views;
using Android.Support.V7.Widget;

namespace SteamChatAndroid.Controls
{
    public class GenericAdapter<T, X> : RecyclerView.Adapter where X : GenericViewHolder<T>
    {
        public event EventHandler<ItemClickEventArgs<T>> ItemClick;

        List<T> _items;
        public List<T> Items {
            get {
                return _items;
            }
            set {
                _items = value;
                NotifyDataSetChanged ();
            }
        }

        int _resourceId;
        public int ResourceId {
            get {
                return _resourceId;
            }
            set {
                _resourceId = value;
                NotifyDataSetChanged ();
            }
        }

        public GenericAdapter (List<T> items, int resourceId)
        {
            _items = items;
            _resourceId = resourceId;
        }

        public override int ItemCount {
            get {
                return _items == null ? 0 : _items.Count;
            }
        }

        public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
        {
            (holder as X).SetItem (_items[position]);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From (parent.Context).Inflate (_resourceId, parent, false);
            var vh = Activator.CreateInstance (typeof (GenericViewHolder<T>), itemView) as GenericViewHolder<T>;
            vh.Listener = clickPosition => OnClick (clickPosition);
            return vh;
        }

        void OnClick (int position)
        {
            ItemClick?.Invoke (this, new ItemClickEventArgs<T> (_items[position], position));
        }
    }

    public class ItemClickEventArgs<T> : EventArgs
    {
        public T Item { get; private set; }

        public int Position { get; private set; }

        public ItemClickEventArgs (T item, int position)
        {
            Item = item;
            Position = position;
        }
    }

    public abstract class GenericViewHolder<T> : RecyclerView.ViewHolder
    {
        Action<int> _listener;

        public GenericViewHolder (View itemView) : base (itemView)
        {
        }

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);
            if (ItemView != null) {
                ItemView.Click -= HandleClick;
            }
            _listener = null;
        }

        void HandleClick (object sender, EventArgs e)
        {
            _listener?.Invoke (AdapterPosition);
        }

        public Action<int> Listener {
            set {
                _listener = value;
                ItemView.Click += HandleClick;
            }
        }

        public abstract void SetItem (T item);
    }
}