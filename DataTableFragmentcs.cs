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

namespace Foosball_Android
{
    class DataTableFragmentcs : Fragment
    {

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {


            View view = inflater.Inflate(Resource.Layout.dataTableFragment, null);
            return view;
        }
        }
}