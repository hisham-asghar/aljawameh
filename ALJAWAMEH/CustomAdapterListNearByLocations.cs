using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ALJAWAMEH
{
    class CustomAdapterListNearByLocations : ArrayAdapter
    {
        private Context c;
        HolderListNearByLocations holder;
        ListLocations item;
        private List<ListLocations> data;
        private LayoutInflater inflater;
        private int resource;

        public CustomAdapterListNearByLocations(Context context, int resource, List<ListLocations> data) : base(context, resource, data)
        {
            this.c = context;
            this.resource = resource;
            this.data = data;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (inflater == null)
            {
                inflater = (LayoutInflater)c.GetSystemService(Context.LayoutInflaterService);

            }
            if (convertView == null)
            {
                convertView = inflater.Inflate(resource, parent, false);

            }
            //holder = new HolderListNearByLocations(convertView)
            //{
            //    Col2 = { Text = data[position].From == null ? "" : data[position].From.Split('\n')[0] },
            //    coldate = { Text = data[position].ShiftDate == null ? "" : DateTime.Parse(data[position].ShiftDate.Split('T')[0]).ToString("dd-MMM-yyyy") },
            //    Col3 = { Text = data[position].From == null ? "" : getRemarks(data[position].From).Trim() },
            //};


            convertView.SetBackgroundColor(Color.White);
            if (position % 2 == 0)
            {
                convertView.SetBackgroundColor(Color.LightGray);
            }

            convertView.SetBackgroundResource(Resource.Drawable.borderRecord);
            return convertView;

        }

    }
}