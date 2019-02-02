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
    class FacebookUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Picture Picture { get; set; }
        public Cover Cover { get; set; }
        public string Id { get; set; }
    }

    class DataFaceBook
    {
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }
    class Picture
    {
        public DataFaceBook Data { get; set; }
    }
    class Cover
    {
        public string Id { get; set; }
        public int OffsetY { get; set; }
        public string Source { get; set; }
    }
}