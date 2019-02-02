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
    public class MyMosqueFragmentAll : Android.Support.V4.App.Fragment
    {
        RecyclerView recycler;
        RecyclerViewAdapterMyMosques _adapter;
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
            var v = inflater.Inflate(Resource.Layout.MyMosquesFragmentAll, container, false);
            // Create your application here
            recycler = v.FindViewById<RecyclerView>(Resource.Id.recycler);
            recycler.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(Application.Context, LinearLayoutManager.Vertical, false); ;
            recycler.SetLayoutManager(layoutManager);
            recycler.NestedScrollingEnabled = true;
            _adapter = new RecyclerViewAdapterMyMosques(lstData);
            for (int i = 0; i < 10; i++)
            {
                lstData.Add(new DataMyMosques() { NameofMosque = "Jamia Masjid Anware Madina", Status = "Verified/Non Verified" });
            }

            _adapter.ItemClick += _adapter_ItemClick;
            recycler.SetAdapter(_adapter);
            return v;
        }

        private void _adapter_ItemClick(object sender, int e)
        {
            
        }
    }
}