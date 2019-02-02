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
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace ALJAWAMEH
{
    [Activity(Label = "Activity1")]
    public class Activity1 : Activity
    {
        RecyclerView recycler;
        RecycleViewAdapter _adapter;
        RecyclerView.LayoutManager layoutManager;
        List<Data> lstData = new List<Data>();

        public string LAtLngData = "[{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984330,\"Lat\":\"31.435758\",\"Lng\":\"74.267403\"},{\"ID\":984327,\"Lat\":\"31.434384\",\"Lng\":\"74.263605\"},{\"ID\":984316,\"Lat\":\"31.431766\",\"Lng\":\"74.264893\"},{\"ID\":984326,\"Lat\":\"31.432755\",\"Lng\":\"74.266760\"},{\"ID\":984322,\"Lat\":\"31.430045\",\"Lng\":\"74.261266\"}]";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Activity);
            recycler = FindViewById<RecyclerView>(Resource.Id.recycler);
            recycler.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false); ;
            recycler.SetLayoutManager(layoutManager);
            recycler.NestedScrollingEnabled = true;
            _adapter = new RecycleViewAdapter(lstData);
            var GridList = JsonConvert.DeserializeObject<List<ListLocations>>(LAtLngData);
            for(int i=0;i<GridList.Count;i++)
            {
                lstData.Add(new Data() { Lat = GridList[i].Lat, Lng = GridList[i].Lng });
            }

            recycler.SetAdapter(_adapter);
            //else
            //{
            //    LayoutCheckRecord.Visibility = ViewStates.Visible;
            //    ListNearByLocation.Adapter = null;
            //}
        }

        private void ListNearByLocation_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
        }
    }
}