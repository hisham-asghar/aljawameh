using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALJAWAMEH.Helper;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ALJAWAMEH
{
    public class FragmentMosqueDetailsContactList : Android.Support.V4.App.Fragment
    {
        RecyclerView recycler;
        RecyclerViewContactList _adapter;
        RecyclerView.LayoutManager layoutManager;
        List<DataMyMosques> lstData = new List<DataMyMosques>();
        private CardView cardview;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            //return base.OnCreateView(inflater, container, savedInstanceState);
            var v = inflater.Inflate(Resource.Layout.FragmentMosqueDetailsContactList, container, false);
            recycler = v.FindViewById<RecyclerView>(Resource.Id.recyclerContactList);
            recycler.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(Application.Context, LinearLayoutManager.Vertical, false); ;
            recycler.SetLayoutManager(layoutManager);
            recycler.NestedScrollingEnabled = true;
            _adapter = new RecyclerViewContactList(lstData);
            for (int i = 0; i < 10; i++)
            {
                lstData.Add(new DataMyMosques() { NameofMosque = "Jon Doe", Status = "030012345678" });
            }

            recycler.SetAdapter(_adapter);
            return v;
        }
    }
}