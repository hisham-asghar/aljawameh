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

namespace ALJAWAMEH
{

    class RecycleViewHolderContactList : RecyclerView.ViewHolder
    {
        public TextView NameofMosques { get; set; }
        public TextView Status { get; set; }
        private CardView cardView;
        private RelativeLayout RelativeItem;

        public RecycleViewHolderContactList(View itemView, Action<int> listener) : base(itemView)
        {
            //NameofMosques = itemView.FindViewById<TextView>(Resource.Id.txtNameofMosque);
            //Status = itemView.FindViewById<TextView>(Resource.Id.txtStatus);
            //cardView = itemView.FindViewById<CardView>(Resource.Id.ItemCardView);
            //RelativeItem = itemView.FindViewById<RelativeLayout>(Resource.Id.ItemMyMosque);
            //itemView.Click += (sender, e) => listener(base.Position);
            //cardView.Click += CardView_Click;
            //RelativeItem.Click += RelativeItem_Click;

        }

        private void RelativeItem_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(Application.Context, typeof(MosqueDetailsActivity));
            Application.Context.StartActivity(i);
        }

        private void CardView_Click(object sender, EventArgs e)
        {

        }
    }





    class RecyclerViewContactList : RecyclerView.Adapter
    {
        private List<DataMyMosques> lstData = new List<DataMyMosques>();
        public event EventHandler<int> ItemClick;
        public RecyclerViewContactList(List<DataMyMosques> lstData)
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
            RecycleViewHolderContactList viewHolder = holder as RecycleViewHolderContactList;
            //viewHolder.LatLng.Text = lstData[position].Lat + lstData[position].Lng;

        }



        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemview = inflater.Inflate(Resource.Layout.itemContactList, parent, false);

            return new RecycleViewHolderContactList(itemview, OnClick);
        }
        private void OnClick(int position)
        {
            if (ItemClick != null)
            {
                ItemClick(this, position);
            }
        }
    }
}