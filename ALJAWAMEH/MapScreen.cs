using Android.App;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System;
using Android.Content;
using Android.Views;
using Android.Gms.Common.Apis;
using Android.Gms.Location.Places.UI;
using Android.Util;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Gms.Common;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using static Android.Gms.Maps.GoogleMap;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using System.Linq;
using Android.Content.PM;
using Android.Support.V7.Widget;
using ALJAWAMEH.Helper;

namespace ALJAWAMEH
{
    [Activity(Label = "App1",Theme = "@style/Theme.DesignDemo")]
    public class MapScreen : AppCompatActivity, IOnMapReadyCallback, IInfoWindowAdapter, ILocationListener
    {

        public string LAtLngData   = "[{\"ID\":984329,\"Lat\":\"31.434769\",\"Lng\":\"74.272854\"},{\"ID\":984330,\"Lat\":\"31.435758\",\"Lng\":\"74.267403\"},{\"ID\":984327,\"Lat\":\"31.434384\",\"Lng\":\"74.263605\"},{\"ID\":984316,\"Lat\":\"31.431766\",\"Lng\":\"74.264893\"},{\"ID\":984326,\"Lat\":\"31.432755\",\"Lng\":\"74.266760\"},{\"ID\":984322,\"Lat\":\"31.430045\",\"Lng\":\"74.261266\"}]";
        private EditText etCordinates;
        private GoogleMap GMap;
        string LongLat;
        private LatLng latLngSource;
        private LatLng LatLong2;
        private LatLng LatLong3;
        private LatLng latlngDestination;
        double earthRadius = 6371;
        TextView TextLocation;
        Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout SlidingUpLayout;
        ListView ListNearByLocation;
        RelativeLayout LayoutCheckRecord;
        PlaceAutocompleteFragment placeAutoComplete;
        Button BtnShowList;
        bool IsListFilled;
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        Location _currentLocation;
        LocationManager _locationManager;
        string _locationProvider;
        LocationManager locationManager;
        Location abc;
        RecyclerView recycler;
        RecycleViewAdapter _adapter;
        RecyclerView.LayoutManager layoutManager;
        List<Data> lstData = new List<Data>();
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.MapScreen);
            string sLocation = Intent.GetStringExtra("CameFromLocation");
            if(sLocation=="Yes")
            {
                Btn_Click(null, null);
            }
            latLngSource = new Android.Gms.Maps.Model.LatLng(31.446916, 74.267862);   // UCP
            latlngDestination = new Android.Gms.Maps.Model.LatLng(31.5204, 74.3587); // Lahore
           // etCordinates = FindViewById<EditText>(Resource.Id.etService);
           // Button btn = FindViewById<Button>(Resource.Id.place);
           // Button btnFind = FindViewById<Button>(Resource.Id.BtnFind);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            placeAutoComplete = FragmentManager.FindFragmentById<PlaceAutocompleteFragment>(Resource.Id.place_autocomplete);
            placeAutoComplete.PlaceSelected += PlaceAutoComplete_PlaceSelected;
            //drawerLayout.OpenDrawer(Gravity.START)
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            ImageView ImageV = FindViewById<ImageView>(Resource.Id.imgDrawer);
            ImageV.Click += ImageV_Click;
            navigationView.ItemIconTintList = null;
           
            navigationView.InflateMenu(Resource.Menu.menu);
            navigationView.Menu.Add("Add Mosque").SetIcon(Resource.Drawable.imgaddMosque);
            navigationView.Menu.Add("My Mosques").SetIcon(Resource.Drawable.imgMyMosques);
            //navigationView.Menu.Add("Notifications").SetIcon(Resource.Drawable.notifications);
            navigationView.Menu.Add("Settings").SetIcon(Resource.Drawable.imgSettings);
            navigationView.Menu.Add("Help").SetIcon(Resource.Drawable.imgHelp);
            navigationView.Menu.Add("Log Out").SetIcon(Resource.Drawable.imgLogout);
            navigationView.InflateHeaderView(Resource.Menu.menu_header);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            ListNearByLocation = FindViewById<ListView>(Resource.Id.lvNearByLocations);
            recycler = FindViewById<RecyclerView>(Resource.Id.recycler);
            recycler.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false); ;
            recycler.SetLayoutManager(layoutManager);
            recycler.NestedScrollingEnabled = true;
            _adapter = new RecycleViewAdapter(lstData);
            var GridList = JsonConvert.DeserializeObject<List<ListLocations>>(LAtLngData);
            for (int i = 0; i < GridList.Count; i++)
            {
                lstData.Add(new Data() { Lat = GridList[i].Lat, Lng = GridList[i].Lng });
            }

            recycler.SetAdapter(_adapter);
            //BtnShowList = FindViewById<Button>(Resource.Id.BtnShowList);
            //BtnShowList.Click += BtnShowList_Click;
            // btnFind.Click += BtnFind_Click;

            //SetUpGoogleMap();
            IsPlayServicesAvailable();
            // btn.Click += Btn_Click;
            if (CheckSelfPermission(Android.Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted)
            {

                // Should we show an explanation?
                if (ShouldShowRequestPermissionRationale(
                        Android.Manifest.Permission.AccessFineLocation));
                {
                    // Explain to the user why we need to read the contacts
                }

                RequestPermissions(new String[] { Android.Manifest.Permission.AccessFineLocation },
                        1);

                // MY_PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE is an
                // app-defined int constant that should be quite unique

            }
            if (CheckSelfPermission(Android.Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted)
            {

                // Should we show an explanation?
                if (ShouldShowRequestPermissionRationale(
                        Android.Manifest.Permission.AccessCoarseLocation)) ;
                {
                    // Explain to the user why we need to read the contacts
                }

                RequestPermissions(new String[] { Android.Manifest.Permission.AccessCoarseLocation },
                        2);

                // MY_PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE is an
                // app-defined int constant that should be quite unique

            }

            if(CheckSelfPermission(Android.Manifest.Permission.AccessCoarseLocation) == Android.Content.PM.Permission.Granted)
            {
                if (CheckSelfPermission(Android.Manifest.Permission.AccessFineLocation) == Android.Content.PM.Permission.Granted)
                {
                    locationManager = (LocationManager)GetSystemService(Context.LocationService);
                    locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 2000, 1, this);
                    if (locationManager.IsProviderEnabled(LocationManager.GpsProvider))
                    {
                        FetchLocation();
                        Toast.MakeText(this, "GPS is Enabled in your devide", ToastLength.Short).Show();
                    }
                    else
                    {
                        showGPSDisabledAlertToUser();
                    }
                }
            }
        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if(requestCode==1)
            {
                if(grantResults.Length > 0
                && grantResults[0] == Android.Content.PM.Permission.Granted)
                {
                    locationManager = (LocationManager)GetSystemService(Context.LocationService);
                    locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 2000, 1, this);
                    if (locationManager.IsProviderEnabled(LocationManager.GpsProvider))
                    {
                        //SetUpMap();
                        FetchLocation();
                        Toast.MakeText(this, "GPS is Enabled in your devide", ToastLength.Short).Show();
                    }
                    else
                    {
                        showGPSDisabledAlertToUser();
                    }
                }
            }
        }

        private void FetchLocation()
        {
            SetUpMap();
            Criteria locationCriteria = new Criteria();
            locationCriteria.Accuracy = Accuracy.Coarse;
            locationCriteria.PowerRequirement = Power.Medium;
            locationManager = (LocationManager)GetSystemService(Context.LocationService);
            var locationProvider = locationManager.GetBestProvider(locationCriteria, true);
            locationManager.RequestLocationUpdates(locationProvider, 2000, 1, this);
            abc = locationManager.GetLastKnownLocation(locationProvider);
            if (GMap!=null)
            {
                CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(new LatLng(abc.Latitude, abc.Longitude), 15);
                GMap.MoveCamera(camera);
                GMap.SetInfoWindowAdapter(this);
                MarkerOptions options = new MarkerOptions().SetPosition(new LatLng(abc.Latitude, abc.Longitude)).SetTitle("Your Location");
                GMap.AddMarker(options).ShowInfoWindow();
            }
        }

        private void showGPSDisabledAlertToUser()
        {
            Android.App.AlertDialog.Builder alertDialogBuilder = new Android.App.AlertDialog.Builder(this);
            alertDialogBuilder.SetMessage("GPS is disabled in your device. Would you like to enable it?")
            .SetCancelable(false)
            .SetPositiveButton("Enable Location", OpenSettings);
            Android.App.AlertDialog alert = alertDialogBuilder.Create();
            alert.Show();
        }

        private void cancel(object sender, DialogClickEventArgs e)
        {
            
        }

        private void OpenSettings(object sender, DialogClickEventArgs e)
        {
            Intent callGPSSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
            StartActivityForResult(callGPSSettingIntent, 2019);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == 2019)
            {
                locationManager = (LocationManager)GetSystemService(Context.LocationService);
                locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 2000, 1, this);
                if (locationManager.IsProviderEnabled(LocationManager.GpsProvider))
                {
                    FetchLocation();
                }
                else
                {
                    
                }
            }
        }

        private void SetUpMap()
        {
            if (GMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googlemap).GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.MapType = GoogleMap.MapTypeNormal;
            this.GMap = googleMap;
            this.GMap.UiSettings.IndoorLevelPickerEnabled = true;
            this.GMap.UiSettings.ScrollGesturesEnabled = true;
            this.GMap.UiSettings.MapToolbarEnabled = true;
            this.GMap.UiSettings.CompassEnabled = true;
            this.GMap.UiSettings.MyLocationButtonEnabled = true;
            this.GMap.UiSettings.ZoomControlsEnabled = true;
            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider))
            {
                Criteria locationCriteria = new Criteria();
                locationCriteria.Accuracy = Accuracy.Coarse;
                locationCriteria.PowerRequirement = Power.Medium;
                var locationProvider = locationManager.GetBestProvider(locationCriteria, true);
                locationManager.RequestLocationUpdates(locationProvider, 2000, 1, this);
                abc = locationManager.GetLastKnownLocation(locationProvider);
                
                CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(new LatLng(abc.Latitude, abc.Longitude), 15);
                GMap.MoveCamera(camera);
                GMap.SetInfoWindowAdapter(this);
                MarkerOptions options = new MarkerOptions().SetPosition(new LatLng(abc.Latitude, abc.Longitude)).SetTitle("Your Location");
                GMap.AddMarker(options).ShowInfoWindow();
            }
            else
            {
                Criteria locationCriteria = new Criteria();
                locationCriteria.Accuracy = Accuracy.Coarse;
                locationCriteria.PowerRequirement = Power.Medium;
                var locationProvider = locationManager.GetBestProvider(locationCriteria, true);
                locationManager.RequestLocationUpdates(locationProvider, 2000, 1, this);
                abc = locationManager.GetLastKnownLocation(locationProvider);

                CameraUpdate dcamera = CameraUpdateFactory.NewLatLngZoom(latlngDestination, 15);
                GMap.MoveCamera(dcamera);
                GMap.SetInfoWindowAdapter(this);
                MarkerOptions doptions = new MarkerOptions().SetPosition(latlngDestination).SetTitle("Location");
                GMap.AddMarker(doptions).ShowInfoWindow();
            }


        }

        protected override void OnPause()
        {
            base.OnPause();
        }
        protected override void OnResume()
        {
            base.OnResume();
        }
        public void OnLocationChanged(Location location)
        {
           
        }

        public void OnProviderDisabled(string provider)
        {
           // throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
          //  SetUpMap();
            Criteria locationCriteria = new Criteria();
            locationCriteria.Accuracy = Accuracy.Coarse;
            locationCriteria.PowerRequirement = Power.Medium;
            var locationProvider = locationManager.GetBestProvider(locationCriteria, true);
            locationManager.RequestLocationUpdates(locationProvider, 2000, 1, this);
            abc = locationManager.GetLastKnownLocation(locationProvider);
            //int count = 0;
            //if (GMap != null)
            //{
            //    new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            //    {
            //        while (count < 100)
            //        {
            //            count += 10;
            //            System.Threading.Thread.Sleep(10);
            //        }
            //        RunOnUiThread(() =>
            //        {
            //            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(new LatLng(abc.Latitude, abc.Longitude), 15);
            //            GMap.MoveCamera(camera);
            //            GMap.SetInfoWindowAdapter(this);
            //            MarkerOptions options = new MarkerOptions().SetPosition(new LatLng(abc.Latitude, abc.Longitude)).SetTitle("Your Location");
            //            GMap.AddMarker(options).ShowInfoWindow();
            //        });
            //    })).Start();
            //}
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
           // throw new NotImplementedException();
        }

        private void ImageV_Click(object sender, EventArgs e)
        {
            drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
            View headerView = navigationView.GetHeaderView(0);
            var spSharedPref = Application.Context.GetSharedPreferences("APP_INFO", FileCreationMode.Private);
            string sFirstName = spSharedPref.GetString("FirstName", null);
            string sSecondName = spSharedPref.GetString("LastName", null);
            string sEmail = spSharedPref.GetString("Email", null);
            var imageUri = Java.Net.URI.Create(spSharedPref.GetString("PhotoUri", ""));
            if (sFirstName != null)
            {
                TextView navUsername = (TextView)headerView.FindViewById(Resource.Id.name);
                navUsername.Text = sFirstName + " " + sSecondName;
                TextView navEmail = (TextView)headerView.FindViewById(Resource.Id.website);
                navEmail.Text = sEmail;
            }
        }


        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            if (e.MenuItem.TitleFormatted.ToString() == "Add Mosque")
            {
                drawerLayout.CloseDrawers();
                Intent i = new Intent(this, typeof(AddMosqueActivity));
                StartActivity(i);
            }
            else
            if (e.MenuItem.TitleFormatted.ToString() == "My Mosques")
            {
                Intent i = new Intent(this, typeof(MyMosques));
                StartActivity(i);
                drawerLayout.CloseDrawers();
            }
            else
            if (e.MenuItem.TitleFormatted.ToString() == "Settings")
            {
                Intent i = new Intent(this, typeof(Settings));
                StartActivity(i);
                drawerLayout.CloseDrawers();
            }
            else
            if (e.MenuItem.TitleFormatted.ToString() == "Help")
            {
                Intent i = new Intent(this, typeof(HelpActivity));
                StartActivity(i);
                drawerLayout.CloseDrawers();
            }
            else
            if (e.MenuItem.TitleFormatted.ToString() == "Log Out")
            {
                drawerLayout.CloseDrawers();
                this.Finish();
                var spDevice = Application.Context.GetSharedPreferences("APP_INFO", FileCreationMode.Private);
                var Loginedit = spDevice.Edit();
                Loginedit.PutString("Registered", "NO");
                Loginedit.Commit();
                Intent i = new Intent(this, typeof(Start));
                StartActivity(i);
            }
        }

        public override void OnBackPressed()
        {

        }

        private void BtnShowList_Click(object sender, EventArgs e)
        {
            if(IsListFilled)
            {
                Intent intent = new Intent(this, typeof(LocationListPopupAcivity));
                StartActivity(intent);
            }
            else
            {
                Android.App.AlertDialog.Builder objAlrt = new Android.App.AlertDialog.Builder(this);
                Utilities.ShowDialog("Can't show the list. Please select or add your location to proceed", "Alert!", objAlrt);
            }
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            //var builder = new PlacePicker.IntentBuilder();
            //StartActivityForResult(builder.Build(this), 1);
        }

        //protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        //{
        //    if (requestCode == 1 && resultCode == Result.Ok)
        //    {

        //        GetPlaceFromPicker(data);
        //    }
        //    base.OnActivityResult(requestCode, resultCode, data);
        //}

        private void GetPlaceFromPicker(Intent data)
        {
            IsListFilled = true;
            GMap.Clear();
            var placepicked = PlacePicker.GetPlace(this, data);
            var geo = new Geocoder(this);
            string name = placepicked.NameFormatted.ToString();
            // var addresses = geo.GetFromLocation(placepicked.LatLng.Latitude, placepicked.LatLng.Longitude, 1);
            etCordinates.Text = placepicked?.AddressFormatted.ToString();
            latLngSource = placepicked.LatLng;
            // Add the points of the polyline
            var GridList = JsonConvert.DeserializeObject<List<ListLocations>>(LAtLngData);
            if (GridList.Count > 0)
            {
                var abc = GridList;
                for (int i = 0; i < GridList.Count; i++)
                {
                    PolylineOptions line = new PolylineOptions();
                    line.InvokeColor(global::Android.Graphics.Color.Red);
                    line.InvokeWidth(3);
                    LatLng latLng = new LatLng(Convert.ToDouble(GridList[i].Lat),Convert.ToDouble(GridList[i].Lng));
                    MarkerOptions marker_options = new MarkerOptions().SetPosition(latLng).SetTitle(CalculateDistanceFromCurrentLocation(latLng) + " KM away.").SetSnippet(GetAdressFromGeoCoder(latLng));
                    GMap.AddMarker(marker_options).ShowInfoWindow();
                    line.Add(latLng);
                    latLng = latLngSource;
                    GMap.SetInfoWindowAdapter(this);
                    //line.Add(latLng);
                    //GMap.AddPolyline(line);
                }
            }
            else
            {
            }
            //latLng = new LatLng(31.423097, 74.259000);
            //line.Add(latLng);
            // Add the polyline to the map
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latLngSource, 15);
            GMap.MoveCamera(camera);
            Circle circle = GMap.AddCircle(new CircleOptions().InvokeCenter(latLngSource).InvokeRadius(2000).InvokeStrokeColor(Android.Graphics.Color.Red).InvokeFillColor(0x220000FF)); /// Here i am getting the exception
            //GroundOverlay overlay = GMap.AddGroundOverlay(new GroundOverlayOptions().InvokeBearing(10).InvokeTransparency(0).InvokeZIndex(5));
            MarkerOptions options = new MarkerOptions().SetPosition(latLngSource).SetTitle("Your Location").SetSnippet(GetAdressFromGeoCoder(latLngSource));
            GMap.AddMarker(options).ShowInfoWindow();
            // Popup layout to test
            Intent intent = new Intent(this, typeof(LocationListPopupAcivity));
            StartActivity(intent);
        }

        private string CalculateDistanceFromCurrentLocation(LatLng latlong)
        {
            double dLat = ToRadians(latlong.Latitude - latLngSource.Latitude);
            double dLng = ToRadians(latlong.Longitude - latLngSource.Longitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(ToRadians(latLngSource.Latitude)) * Math.Cos(ToRadians(latlong.Latitude)) *
            Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double dist = earthRadius * c;
            string sDistance = Math.Round(dist, 2).ToString();
            return sDistance;
        }

        public string GetAdressFromGeoCoder(LatLng latlong)
        {
            Geocoder geocoder;
           // var addresses;
            geocoder = new Geocoder(this);
            try
            {
                var addresses = geocoder.GetFromLocation(latlong.Latitude, latlong.Longitude, 1); // Here 1 represent max location result to returned, by documents it recommended 1 to 5
                String address = addresses[0].GetAddressLine(0); // If any additional address line present than only, check with max available address lines by getMaxAddressLineIndex()
                String city = addresses[0].Locality;
                String state = addresses[0].AdminArea;
                String country = addresses[0].CountryCode;
                String postalCode = addresses[0].PostalCode;
                String knownName = addresses[0].FeatureName; // Only if available else return NULL
                return " "+""+address + ", " + city + ", " + state + ", " + country + ", " + postalCode + ", " + knownName;
            }
            catch
            {
                Console.WriteLine("Not Found");
            }
            return "";
        }
        private async Task<string> GetAddressFromLatLngAsync(LatLng latlong)
        {
            string URL = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + latlong.Latitude + "," + latlong.Longitude + "&key=AIzaSyBjuOudEJf-wBlRrxgDVppRajP6wQ3B6Y8";
            string strJSONAddressResponse = await FnHttpAddressRequest(URL);
            if (strJSONAddressResponse != Model.Constants.strException)
            {
                return strJSONAddressResponse;
            }
            else
            {
                RunOnUiThread(() =>
                {
                    Toast.MakeText(this, Model.Constants.strUnableToConnect, ToastLength.Short).Show();
                });
            }
            return "";
        }

        async Task<string> FnHttpAddressRequest(string strUri)
        {
            webclient = new WebClient();
            string strResultData;
            try
            {
                strResultData = await webclient.DownloadStringTaskAsync(new Uri(strUri));
                Console.WriteLine(strResultData);
            }
            catch
            {
                strResultData = Model.Constants.strException;
            }
            finally
            {
                if (webclient != null)
                {
                    webclient.Dispose();
                    webclient = null;
                }
            }

            return strResultData;
        }

        WebClient webclient;
        async Task<string> FnHttpRequest(string strUri)
        {
            webclient = new WebClient();
            string strResultData;
            try
            {
                strResultData = await webclient.DownloadStringTaskAsync(new Uri(strUri));
                Console.WriteLine(strResultData);
            }
            catch
            {
                strResultData = Model.Constants.strException;
            }
            finally
            {
                if (webclient != null)
                {
                    webclient.Dispose();
                    webclient = null;
                }
            }

            return strResultData;
        }
                
        private void PlaceAutoComplete_PlaceSelected(object sender, PlaceSelectedEventArgs e)
        {
            
        }

       

        private void GMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            //string marker = GoogleMap.marker
            LongLat = e.Point.ToString();
            LatLng marker = e.Point;
            var geo = new Geocoder(this);
            // map.MyLocationEnabled = true
            //addresses[0].FeatureName + ", " + addresses[1].Thoroughfare + ", " + addresses[0].SubLocality
            //    + ", " + addresses[0].Locality + ", " + addresses[0].AdminArea + ", " + addresses[0].CountryName
            var addresses = geo.GetFromLocation(marker.Latitude, marker.Longitude, 1);
            MarkerOptions options = new MarkerOptions().SetPosition(marker).SetTitle(addresses[0].FeatureName + ", " + addresses[0].Thoroughfare + ", " + addresses[0].SubLocality
                + ", " + addresses[0].Locality + ", " + addresses[0].AdminArea + ", " + addresses[0].CountryName).SetSnippet(marker.ToString());
            GMap.AddMarker(options).ShowInfoWindow();
            double dLat = ToRadians(addresses[0].Latitude - latLngSource.Latitude);
            double dLng = ToRadians(addresses[0].Longitude - latLngSource.Longitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(ToRadians(latLngSource.Latitude)) * Math.Cos(ToRadians(addresses[0].Latitude)) *
            Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double dist = earthRadius * c;
            double distanceinmeter = dist * 1000;
            distanceinmeter = Convert.ToDouble(distanceinmeter.ToString().Split('.')[0]);
            Android.App.AlertDialog.Builder objAlrt = new Android.App.AlertDialog.Builder(this);
            objAlrt.SetTitle("Location Alert");
            objAlrt.SetMessage("Our vendor is " + dist.ToString() + " kilometer away from you. We will soon contact you on your provided number.");
            Dialog dialogue = objAlrt.Create();
            dialogue.Show();

        }

        double ToRadians(double degrees)
        {
            double LOCAL_PI = 3.1415926535897932385;
            double radians = degrees * LOCAL_PI / 180;
            return radians;
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {

                }
                    //msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    //msgText.Text = "Sorry, this device is not supported";
                }
                return false;
            }
            else
            {
                //msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        public View GetInfoContents(Marker marker)
        {
            return null;
        }

        public View GetInfoWindow(Marker marker)
        {
            View v = LayoutInflater.Inflate(Resource.Layout.info_window, null,false);
            v.FindViewById<TextView>(Resource.Id.TextLocationTitle).Text = marker.Title;
            v.FindViewById<TextView>(Resource.Id.TextLocation).Text = marker.Snippet;
            return v;
        }





        //public void OnLocationChanged(Location location)
        //{
        //    //throw new NotImplementedException();
        //    Double lat, lng;
        //    lat = location.Latitude;
        //    lng = location.Longitude;
        //    MarkerOptions options = new MarkerOptions().SetPosition(new LatLng(lat, lng)).SetTitle("My Location");
        //    GMap.AddMarker(options).ShowInfoWindow();
        //    CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
        //    builder.Target(new LatLng(lat, lng));
        //    CameraPosition cameraposition = builder.Build();
        //    CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraposition);
        //    GMap.MoveCamera(cameraUpdate);

        //}

    }
}

