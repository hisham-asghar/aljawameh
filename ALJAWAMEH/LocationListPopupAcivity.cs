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
using Newtonsoft.Json;

namespace ALJAWAMEH
{
    [Activity(Label = "LocationListPopupAcivity", Theme = "@android:style/Theme.Dialog")]
    public class LocationListPopupAcivity : Activity
    {
        ImageButton ImgBtnClose;
        ListView ListNearByLocation;
        RelativeLayout LayoutCheckRecord;
        public string LAtLngData = "[{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984330,\"Lat\":\"31.435758\",\"Lng\":\"74.267403\"},{\"ID\":984327,\"Lat\":\"31.434384\",\"Lng\":\"74.263605\"},{\"ID\":984316,\"Lat\":\"31.431766\",\"Lng\":\"74.264893\"},{\"ID\":984326,\"Lat\":\"31.432755\",\"Lng\":\"74.266760\"},{\"ID\":984322,\"Lat\":\"31.430045\",\"Lng\":\"74.261266\"}]";
        CustomAdapterListNearByLocations _adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            this.SetFinishOnTouchOutside(false);
            // Create your application here
            SetContentView(Resource.Layout.LocationListPopUp);
            ListNearByLocation = FindViewById<ListView>(Resource.Id.lvNearByLocations);
            LayoutCheckRecord = FindViewById<RelativeLayout>(Resource.Id.checkrecord);
            ImgBtnClose = FindViewById<ImageButton>(Resource.Id.imgBtnClose);
            var GridList = JsonConvert.DeserializeObject<List<ListLocations>>(LAtLngData);
            if (GridList.Count > 0)
            {
                LayoutCheckRecord.Visibility = ViewStates.Gone;
                var abc = GridList;
                _adapter = new CustomAdapterListNearByLocations(this, Resource.Layout.ModelNearByLocation, GridList);
                ListNearByLocation.Adapter = _adapter;
                for (int i = 0; i < GridList.Count; i++)
                {
                    //line.Add(latLng);
                    //GMap.AddPolyline(line);
                }
            }
            //else
            //{
            //    LayoutCheckRecord.Visibility = ViewStates.Visible;
            //    ListNearByLocation.Adapter = null;
            //}
            ImgBtnClose.Click += ImgBtnClose_Click;
            ListNearByLocation.ItemClick += ListNearByLocation_ItemClick;
        }

        private void ListNearByLocation_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent i = new Intent(this, typeof(MosqueDetailsActivity));
            StartActivity(i);
        }


        private void ImgBtnClose_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
    }
}