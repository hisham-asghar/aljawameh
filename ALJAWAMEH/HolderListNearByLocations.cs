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

namespace ALJAWAMEH
{
    class HolderListNearByLocations
    {
        public TextView Address;
        public TextView LatLng;
        public HolderListNearByLocations(View v)
        {

            // this.Col1 = v.FindViewById<TextView>(Resource.Id.txtlistcol1);
            this.Address = v.FindViewById<TextView>(Resource.Id.txtAddress);
            this.LatLng = v.FindViewById<TextView>(Resource.Id.txtLatLng);
        }

    }
}