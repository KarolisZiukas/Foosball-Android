using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Foosball_Android
{
    class ScoreListAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        string[] masyvas;

        public ScoreListAdapter(string [] masyvas)
        {
            this.masyvas = masyvas;
        }

        public override int ItemCount
        {
            get { return masyvas.Length; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CardHolder cardHolder = holder as CardHolder;
            cardHolder.ScoreTextView.Text = "" + 0;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.scoresListItem, parent, false);
            return new CardHolder(view);
        }

        class CardHolder : RecyclerView.ViewHolder
        {
            public TextView ScoreTextView { get; private set; }

            public CardHolder(View itemView) : base(itemView)
            {
                ScoreTextView = itemView.FindViewById<TextView>(Resource.Id.test_tv);
            }
        }
    }
}