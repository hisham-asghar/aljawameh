using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALJAWAMEH.Helper;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace ALJAWAMEH
{
    class RecycleViewHolder : RecyclerView.ViewHolder
    {
        public TextView Address { get; set; }
        public TextView LatLng { get; set; }
        public ImageView ImgNearByMosque;
        public RecycleViewHolder(View itemView):base(itemView)
        {
            Address = itemView.FindViewById<TextView>(Resource.Id.txtAddress);
            LatLng = itemView.FindViewById<TextView>(Resource.Id.txtLatLng);
            ImgNearByMosque = itemView.FindViewById<ImageView>(Resource.Id.imgNearByMosque);
        }
    }
    class RecycleViewAdapter : RecyclerView.Adapter
    {
        private List<Data> lstData = new List<Data>();

        public RecycleViewAdapter(List<Data> lstData)
        {
            this.lstData = lstData;
        }

        public override int ItemCount
        {
            get
            {
                return lstData.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecycleViewHolder viewHolder = holder as RecycleViewHolder;
            //viewHolder.LatLng.Text = lstData[position].Lat + lstData[position].Lng;
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemview = inflater.Inflate(Resource.Layout.item, parent, false);
            return new RecycleViewHolder(itemview);
        }

        public void RoundImage(ImageView mimageView)
        {

           
        }
    }
}