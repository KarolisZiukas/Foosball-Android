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

namespace Foosball_Android
{
    class ScoreListFragment : Fragment
    {
        string[] masyvas = { "labas", "kebabas", "arabas", "krabas" };
        RecyclerView recyclerView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ScoresList, null);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.scores_list_fragment);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(new ScoreListAdapter(masyvas));
            return view;
        }
    }
}