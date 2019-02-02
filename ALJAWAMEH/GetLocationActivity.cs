using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace ALJAWAMEH
{
    [Activity(Label = "GetLocationActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class GetLocationActivity : AppCompatActivity
    {
       // DrawerLayout drawerLayout;
        NavigationView navigationView;
        View header;
        ActionBarDrawerToggle mDrawerToogle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GetLocation);
            Button btn = FindViewById<Button>(Resource.Id.btnRegister);
            TextView EditEnterLocation = FindViewById<TextView>(Resource.Id.editEnterLocation);
           // drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.ItemIconTintList = null;
            navigationView.InflateMenu(Resource.Menu.menu);
            navigationView.Menu.Add("Add Mosque Data").SetIcon(Resource.Drawable.mosque_menu);
            navigationView.Menu.Add("Notifications").SetIcon(Resource.Drawable.notifications);
            navigationView.Menu.Add("Settings").SetIcon(Resource.Drawable.settings);
            navigationView.Menu.Add("Help").SetIcon(Resource.Drawable.help);
            navigationView.Menu.Add("Log Out").SetIcon(Resource.Drawable.help);
            navigationView.InflateHeaderView(Resource.Menu.menu_header);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            // Create your application here
            btn.Click += Btn_Click;
            EditEnterLocation.Click += EditEnterLocation_Click;
        }

        private void EditEnterLocation_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(MapScreen));
            StartActivity(i);
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            if (e.MenuItem.TitleFormatted.ToString() == "Add Mosque Data")
            {
                //drawerLayout.CloseDrawers();
                Intent i = new Intent(this, typeof(AddMosqueActivity));
                StartActivity(i);
            }
            else
            if (e.MenuItem.TitleFormatted.ToString() == "Notifications")
            {
                //drawerLayout.CloseDrawers();
            }
            else
            if (e.MenuItem.TitleFormatted.ToString() == "Settings")
            {
                //drawerLayout.CloseDrawers();
            }
            else
            if (e.MenuItem.TitleFormatted.ToString() == "Help")
            {
                //drawerLayout.CloseDrawers();
            }
            else
            if (e.MenuItem.TitleFormatted.ToString() == "Log Out")
            {
               // drawerLayout.CloseDrawers();
                this.Finish();
                Intent i = new Intent(this, typeof(Login));
                StartActivity(i);
            }


        }

        public override void OnBackPressed()
        {
            
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(MapScreen));
            i.PutExtra("CameFromLocation", "Yes");
            StartActivity(i);
        }
    }
}