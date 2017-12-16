using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace Foosball_Android
{
    class ScoreListAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {

        List<ScoreModel> result;

        public ScoreListAdapter(List<ScoreModel> result)
        {
            this.result = result;
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
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.scoresListItem, parent, false);
            return new CardHolder(view);
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