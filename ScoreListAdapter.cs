using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace Foosball_Android
{
    class ScoreListAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {

        List<ScoreModel> result;
        OnCardClickListener onCardClickListener;
        public ScoreListAdapter(List<ScoreModel> result, OnCardClickListener onCardClickListener)
        {
            this.result = result;
            this.onCardClickListener = onCardClickListener;
        }

        public override int ItemCount
        {
            get { return result.Count; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CardHolder cardHolder = holder as CardHolder;
            cardHolder.BlueTeamScoreTextView.Text = "" + result[position].blueTeamScore;
            cardHolder.RedTeamScoreTextView.Text = "" + result[position].redTeamScore;
            cardHolder.BlueTeamScoreTextView.Click += delegate
            {
                Toast.MakeText(Android.App.Application.Context, "aloha", ToastLength.Short);

            };
            cardHolder.ItemView.Click += delegate
            {
               onCardClickListener.OnItemClick(result[cardHolder.AdapterPosition].redTeamScore, result[cardHolder.AdapterPosition].blueTeamScore, result[cardHolder.AdapterPosition].id);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.scoresListItem, parent, false);
            return new CardHolder(view);
        }

        public interface OnCardClickListener
        {
            void OnItemClick(int redTeam, int blueTeam, long id);
        }

        class CardHolder : RecyclerView.ViewHolder
        {
            public TextView RedTeamScoreTextView { get; private set; }
            public TextView BlueTeamScoreTextView { get; private set; }

            public CardHolder(View itemView) : base(itemView)
            {
                RedTeamScoreTextView = itemView.FindViewById<TextView>(Resource.Id.blue_team_history_tv);
                BlueTeamScoreTextView = itemView.FindViewById<TextView>(Resource.Id.red_team_history_tv);
            }
        }
    }
}