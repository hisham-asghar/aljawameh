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
    [Activity(Label = "Settings")]
    public class Settings : Activity
    {
        Spinner SpnLanguage;
        ArrayAdapter AdapterLanguage;
        ImageButton ImgBtnClose;
        Button BtnSave;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Settings);
           // SpnLanguage = FindViewById<Spinner>(Resource.Id.spinnerLanguage);
           // ImgBtnClose = FindViewById<ImageButton>(Resource.Id.imgBtnClose);
           //BtnSave = FindViewById<Button>(Resource.Id.btnSave);
            AdapterLanguage = new ArrayAdapter(this, Resource.Layout.CustomTextView);
            AdapterLanguage.SetDropDownViewResource(Resource.Layout.CustomTextView);
            AdapterLanguage.Add("English");
            AdapterLanguage.Add("Urdu");
            AdapterLanguage.Add("Arabic");
          //  SpnLanguage.Adapter = AdapterLanguage;
           // SpnLanguage.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_Branch_ItemSelected);
          //  BtnSave.Click += BtnSave_Click;
          //  ImgBtnClose.Click += ImgBtnClose_Click;
        }
    }
}