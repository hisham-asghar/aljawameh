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
    class ListLocations
    {
        public string ID { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }

        public ListLocations()
        { }
    }
}