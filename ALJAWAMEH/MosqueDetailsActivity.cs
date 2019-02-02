using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace ALJAWAMEH
{
    [Activity(Label = "MosqueDetailsActivity", Theme = "@style/Theme.DesignDemo")]
    public class MosqueDetailsActivity : AppCompatActivity
    {
        Button BtnMoveToList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MosqueDetails);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //SupportActionBar.SetIcon(Resource.Drawable.);
            Android.Support.V4.View.ViewPager
             viewpager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewpager);

            setupViewPager(viewpager); //Calling SetupViewPager Method  
                                       //TabLayout  
            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewpager);
            //FloatingActionButton  
            //var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += (sender, e) => {
            //    Snackbar.Make(fab, "Here's a snackbar!", Snackbar.LengthLong).SetAction("Action", v => Console WriteLine("Action handler")).Show();
            //};
        }

        void setupViewPager(Android.Support.V4.View.ViewPager viewPager)
        {
            var adapter = new AdapterMosqueDetail(SupportFragmentManager);
            adapter.AddFragment(new FragmentMosqueDetailsOverview(), "Overview");
            adapter.AddFragment(new FragmentMosqueDetailsContactList(), "Contact List");
            adapter.AddFragment(new FragmentMosqueDetailsNamazTimings(), "Namaz Timings");
            viewPager.Adapter = adapter;
            viewPager.Adapter.NotifyDataSetChanged();
        }

        private void BtnMoveToList_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
    }

    class AdapterMosqueDetail : Android.Support.V4.App.FragmentPagerAdapter
    {
        List<V4Fragment> fragments = new List<V4Fragment>();
        List<string> fragmentTitles = new List<string>();
        public AdapterMosqueDetail(V4FragmentManager fm) : base(fm) { }
        public void AddFragment(V4Fragment fragment, string title)
        {
            fragments.Add(fragment);
            fragmentTitles.Add(title);
        }
        public override V4Fragment GetItem(int position)
        {
            return fragments[position];
        }
        public override int Count
        {
            get
            {
                return fragments.Count;
            }
        }
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(fragmentTitles[position]);
        }
    }

}