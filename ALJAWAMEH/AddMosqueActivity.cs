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
using Android.Provider;
using Android.Graphics;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Android.Media;
using Java.IO;
using System.IO;
using System.Collections.Specialized;
using Android.Graphics.Drawables;
using Android.Gms.Location.Places.UI;
using Android.Gms.Maps.Model;
using Android.Locations;

namespace ALJAWAMEH
{
    [Activity(Label = "AddMosqueActivity")]
    public class AddMosqueActivity : Activity
    {
        Button btnReset;
        Button btnSubmit;
        RelativeLayout LayoutChooseFromMap;
        RelativeLayout BtnChooseImage;
        ImageView ImgChooseImage1;
        ImageView ImgChooseImage2;
        ImageView ImgChooseImage3;
        ImageView ImgChooseImage4;
        TextView TextChooseImage1;
        TextView TextChooseImage2;
        TextView TextChooseImage3;
        TextView TextChooseImage4;
        string ImagePath1;
        string ImagePath2;
        string ImagePath3;
        string ImagePath4;
        string ImageName1;
        string ImageName2;
        string ImageName3;
        string ImageName4;
        byte[] ImageData1;
        byte[] ImageData2;
        byte[] ImageData3;
        byte[] ImageData4;
        EditText EtRemarks;
        Spinner SpnSect;
        ImageView ImgCloseFile1;
        ImageView ImgCloseFile2;
        ImageView ImgCloseFile3;
        ImageView ImgCloseFile4;
        ImageView ImgAddName;
        ImageView ImgAddNamaz;
        ImageView ImgRemoveNamaz;
        ArrayAdapter adapter;
        TableRow RowNamaz;
        Button BtnSaveContact;
        PopupWindow pw;
        EditText EtPopUpContactName;
        EditText EtPopUpContactNumber;
        EditText EtContact;
        ImageView ImgContactCancel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AddMosque);
            LayoutChooseFromMap = FindViewById<RelativeLayout>(Resource.Id.BtnFindFromMap);
            BtnChooseImage = FindViewById<RelativeLayout>(Resource.Id.btnChooseFile);
            SpnSect = FindViewById<Spinner>(Resource.Id.SpnSect);
            // Initializing a String Array
            adapter = new ArrayAdapter(this, Resource.Layout.CustomTextView);
            adapter.SetDropDownViewResource(Resource.Drawable.spinner_Item);
            adapter.Add("Sunni");
            adapter.Add("Ahl Hadith");
            adapter.Add("Bralvi");
            SpnSect.Adapter = adapter;
            //BtnChooseImage2 = FindViewById<Button>(Resource.Id.btnChooseFile2);
            //BtnChooseImage3 = FindViewById<Button>(Resource.Id.btnChooseFile3);
            //BtnChooseImage4 = FindViewById<Button>(Resource.Id.btnChooseFile4);
            ImgChooseImage1 = FindViewById<ImageView>(Resource.Id.imgChooseFile1);
            ImgChooseImage2 = FindViewById<ImageView>(Resource.Id.imgChooseFile2);
            ImgChooseImage3 = FindViewById<ImageView>(Resource.Id.imgChooseFile3);
            ImgChooseImage4 = FindViewById<ImageView>(Resource.Id.imgChooseFile4);
            RowNamaz = FindViewById<TableRow>(Resource.Id.RowNamaz);
            ImgCloseFile1 = FindViewById<ImageView>(Resource.Id.imgCloseFile1);
            ImgCloseFile2 = FindViewById<ImageView>(Resource.Id.imgCloseFile2);
            ImgCloseFile3 = FindViewById<ImageView>(Resource.Id.imgCloseFile3);
            ImgCloseFile4 = FindViewById<ImageView>(Resource.Id.imgCloseFile4);
            ImgAddName = FindViewById<ImageView>(Resource.Id.imgAddName);
            ImgAddNamaz = FindViewById<ImageView>(Resource.Id.ImgAddNamaz);
            ImgRemoveNamaz = FindViewById<ImageView>(Resource.Id.ImgRemoveNamaz);
            ImgContactCancel = FindViewById<ImageView>(Resource.Id.imgCancelContact);
            ImgContactCancel.Click += ImgContactCancel_Click;
            ImgRemoveNamaz.Click += ImgRemoveNamaz_Click;
            ImgAddNamaz.Click += ImgAddNamaz_Click;
            ImgAddName.Click += ImgAddName_Click;
            ImgRemoveNamaz.Visibility = ViewStates.Gone;
            RowNamaz.Visibility = ViewStates.Gone;
            ImgCloseFile1.Visibility = ViewStates.Gone;
            ImgCloseFile2.Visibility = ViewStates.Gone;
            ImgCloseFile3.Visibility = ViewStates.Gone;
            ImgCloseFile4.Visibility = ViewStates.Gone;
            TextChooseImage1 = FindViewById<TextView>(Resource.Id.etfilechoosen1);
            TextChooseImage2 = FindViewById<TextView>(Resource.Id.etfilechoosen2);
            TextChooseImage3 = FindViewById<TextView>(Resource.Id.etfilechoosen3);
            TextChooseImage4 = FindViewById<TextView>(Resource.Id.etfilechoosen4);
            EtRemarks = FindViewById<EditText>(Resource.Id.etRemarks);
            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            EtContact = FindViewById<EditText>(Resource.Id.EtContact);




            if (CheckSelfPermission(Android.Manifest.Permission.ReadExternalStorage) != Android.Content.PM.Permission.Granted)
            {

                // Should we show an explanation?
                if (ShouldShowRequestPermissionRationale(
                        Android.Manifest.Permission.ReadExternalStorage))
                {
                    // Explain to the user why we need to read the contacts
                }

                RequestPermissions(new String[] { Android.Manifest.Permission.ReadExternalStorage },
                        1);

                // MY_PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE is an
                // app-defined int constant that should be quite unique

            }
            //BtnChooseImage1.Click += BtnChooseImg_Click;
            //BtnChooseImage2.Click += BtnChooseImg_Click;
            //BtnChooseImage3.Click += BtnChooseImg_Click;
            //BtnChooseImage4.Click += BtnChooseImg_Click;
            BtnChooseImage.Click += BtnChooseImg_Click;
            LayoutChooseFromMap.Click += BtnChooseFromMap_Click;
            btnSubmit.Click += BtnSubmit_Click; 


        }

        private void ImgContactCancel_Click(object sender, EventArgs e)
        {
            EtContact.Text = "";
        }

        private void ImgRemoveNamaz_Click(object sender, EventArgs e)
        {
            ImgRemoveNamaz.Visibility = ViewStates.Gone;
            ImgAddNamaz.Visibility = ViewStates.Visible;
            RowNamaz.Visibility = ViewStates.Gone;
        }

        private void ImgAddNamaz_Click(object sender, EventArgs e)
        {
            ImgRemoveNamaz.Visibility = ViewStates.Visible;
            RowNamaz.Visibility = ViewStates.Visible;
        }

        private void ImgAddName_Click(object sender, EventArgs e)
        {
            LayoutInflater inflater = (LayoutInflater)this.GetSystemService(Context.LayoutInflaterService);
            pw = new PopupWindow(inflater.Inflate(Resource.Drawable.popup_Contact, null, false), 500, 450, true);
            pw.ShowAtLocation(this.FindViewById(Resource.Id.imgAddName), GravityFlags.Center, 0, 0);
            pw.SplitTouchEnabled = false;
            pw.OutsideTouchable = false;
            BtnSaveContact = pw.ContentView.FindViewById<Button>(Resource.Id.btnSaveContact);
            EtPopUpContactName = pw.ContentView.FindViewById<EditText>(Resource.Id.EtPopUpContactName);
            EtPopUpContactNumber = pw.ContentView.FindViewById<EditText>(Resource.Id.EtPopUpContactNumber);
            BtnSaveContact.Click += BtnSaveContact_Click;

        }

        private void BtnSaveContact_Click(object sender, EventArgs e)
        {
            EtContact.Text = EtPopUpContactName.Text + ", " + EtPopUpContactNumber.Text;
            pw.Dismiss();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (ImgChooseImage1.Background == null && ImgChooseImage1.Background == null &&
                    ImgChooseImage1.Background == null && ImgChooseImage1.Background == null )
            {
                    AlertDialog.Builder objAlrt = new AlertDialog.Builder(this);
                    Utilities.ShowDialog("Atleast one attachment is required!", "Warning", objAlrt);
            }
            else
            {
                //LayoutInflater inflater = (LayoutInflater)this.GetSystemService(Context.LayoutInflaterService);
                //PopupWindow pw = new PopupWindow(inflater.Inflate(Resource.Drawable.popup_DetailsAdded, null, false), 400, 400, true);
                //pw.ShowAtLocation(this.FindViewById(Resource.Id.imgAddName), GravityFlags.Center, 0, 0);
            }
        }

       

        private void BtnChooseFromMap_Click(object sender, EventArgs e)
        {
            var builder = new PlacePicker.IntentBuilder();
            StartActivityForResult(builder.Build(this), 2);
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
                return " " + "" + address + ", " + city + ", " + state + ", " + country + ", " + postalCode + ", " + knownName;
            }
            catch
            {
                System.Console.WriteLine("Not Found");
            }
            return "";
        }

    



        private void BtnChooseImg_Click(object sender, EventArgs e)
        {
            if (ImgChooseImage1.Background == null || ImgChooseImage2.Background == null ||
                  ImgChooseImage3.Background == null || ImgChooseImage4.Background == null)
            {
               // Button rb = (Button)sender;
               // string sButtonId = rb.Id.ToString();
                Intent i = new Intent(Intent.ActionPick, Android.Provider.MediaStore.Images.Media.ExternalContentUri);
                StartActivityForResult(i, 1);
            }
            else
            {
              
            }
        }


        public void button()
        {
            if (ImgChooseImage1.Background == null && ImgChooseImage2.Background == null &&
                   ImgChooseImage3.Background == null && ImgChooseImage4.Background == null )
            {
                    AlertDialog.Builder objAlrt = new AlertDialog.Builder(this);
                    Utilities.ShowDialog("Atleast one attachment is required!", "Warning", objAlrt);
            }
            else
            {
                //SubmitClaimApplication();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1 && resultCode == Result.Ok)
            {
               

                if (ImgChooseImage1.Background == null)
                {
                    Android.Net.Uri uri = data.Data;
                    string[] projection = { MediaStore.Images.Media.InterfaceConsts.Data };
                    Android.Database.ICursor cursor = ContentResolver.Query(uri, projection, null, null, null);
                    cursor.MoveToFirst();
                    int columnIndex = cursor.GetColumnIndex(projection[0]);
                    ImagePath1 = cursor.GetString(columnIndex);
                    ImageName1 = ImagePath1.Substring(ImagePath1.LastIndexOf("/") + 1);
                    TextChooseImage1.Text = ImageName1;
                    cursor.Close();
                    // String fname = new Java.IO.File(FilesDir, ImageName1).AbsolutePath;
                    // string path = GetRealPathFromURI(uri);
                    // var yourSelectedImageasdpath = BitmapFactory.DecodeFile(path);
                    // var yourSelectedImageasd = BitmapFactory.DecodeFile(fname);
                    var yourSelectedImage = BitmapFactory.DecodeFile(ImagePath1);
                    MemoryStream memStream = new MemoryStream();
                    string sFileExtension = ImageName1.Split('.')[1];
                    Bitmap.CompressFormat compressFormat = Bitmap.CompressFormat.Jpeg;
                    if (sFileExtension == "Png" || sFileExtension == "png" || sFileExtension == "PNG")
                    {
                        compressFormat = Bitmap.CompressFormat.Png;
                    }
                    else
                        if (sFileExtension == "Jpeg" || sFileExtension == "jpeg" || sFileExtension == "JPEG" || sFileExtension == "Jpg" || sFileExtension == "jpg" || sFileExtension == "JPG")
                    {
                        compressFormat = Bitmap.CompressFormat.Jpeg;
                    }
                    yourSelectedImage.Compress(compressFormat, 100, memStream);
                    ImageData1 = memStream.ToArray();
                    Android.Graphics.Drawables.Drawable d = new Android.Graphics.Drawables.BitmapDrawable(yourSelectedImage);
                    ImgChooseImage1.SetBackgroundDrawable(d);
                    ImgCloseFile1.Visibility = ViewStates.Visible;
                }
                else
                    if (ImgChooseImage2.Background == null)
                {
                    Android.Net.Uri uri = data.Data;
                    string[] projection = { MediaStore.Images.Media.InterfaceConsts.Data };
                    Android.Database.ICursor cursor = ContentResolver.Query(uri, projection, null, null, null);
                    cursor.MoveToFirst();
                    int columnIndex = cursor.GetColumnIndex(projection[0]);
                    ImagePath2 = cursor.GetString(columnIndex);
                    ImageName2 = ImagePath2.Substring(ImagePath2.LastIndexOf("/") + 1);
                    TextChooseImage2.Text = ImageName2;
                    cursor.Close();
                    Bitmap yourSelectedImage = BitmapFactory.DecodeFile(ImagePath2);
                    MemoryStream memStream = new MemoryStream();
                    string sFileExtension = ImageName2.Split('.')[1];
                    Bitmap.CompressFormat compressFormat = Bitmap.CompressFormat.Jpeg;
                    if (sFileExtension == "Png" || sFileExtension == "png" || sFileExtension == "PNG")
                    {
                        compressFormat = Bitmap.CompressFormat.Png;
                    }
                    else
                        if (sFileExtension == "Jpeg" || sFileExtension == "jpeg" || sFileExtension == "JPEG" || sFileExtension == "Jpg" || sFileExtension == "jpg" || sFileExtension == "JPG")
                    {
                        compressFormat = Bitmap.CompressFormat.Jpeg;
                    }
                    yourSelectedImage.Compress(compressFormat, 100, memStream);
                    ImageData2 = memStream.ToArray();
                    Android.Graphics.Drawables.Drawable d = new Android.Graphics.Drawables.BitmapDrawable(yourSelectedImage);
                    ImgChooseImage2.SetBackgroundDrawable(d);
                    ImgCloseFile1.Visibility = ViewStates.Visible;
                }
                else
                    if (ImgChooseImage3.Background == null)
                {
                    Android.Net.Uri uri = data.Data;
                    string[] projection = { MediaStore.Images.Media.InterfaceConsts.Data };
                    Android.Database.ICursor cursor = ContentResolver.Query(uri, projection, null, null, null);
                    cursor.MoveToFirst();
                    int columnIndex = cursor.GetColumnIndex(projection[0]);
                    ImagePath3 = cursor.GetString(columnIndex);
                    ImageName3 = ImagePath3.Substring(ImagePath3.LastIndexOf("/") + 1);
                    TextChooseImage3.Text = ImageName3;
                    cursor.Close();
                    Bitmap yourSelectedImage = BitmapFactory.DecodeFile(ImagePath3);
                    MemoryStream memStream = new MemoryStream();
                    string sFileExtension = ImageName3.Split('.')[1];
                    Bitmap.CompressFormat compressFormat = Bitmap.CompressFormat.Jpeg;
                    if (sFileExtension == "Png" || sFileExtension == "png" || sFileExtension == "PNG")
                    {
                        compressFormat = Bitmap.CompressFormat.Png;
                    }
                    else
                        if (sFileExtension == "Jpeg" || sFileExtension == "jpeg" || sFileExtension == "JPEG" || sFileExtension == "Jpg" || sFileExtension == "jpg" || sFileExtension == "JPG")
                    {
                        compressFormat = Bitmap.CompressFormat.Jpeg;
                    }
                    yourSelectedImage.Compress(compressFormat, 100, memStream);
                    ImageData3 = memStream.ToArray();
                    Android.Graphics.Drawables.Drawable d = new Android.Graphics.Drawables.BitmapDrawable(yourSelectedImage);
                    ImgChooseImage3.SetBackgroundDrawable(d);
                    ImgCloseFile1.Visibility = ViewStates.Visible;
                }
                else
                    if (ImgChooseImage4.Background == null)
                {
                    Android.Net.Uri uri = data.Data;
                    string[] projection = { MediaStore.Images.Media.InterfaceConsts.Data };
                    Android.Database.ICursor cursor = ContentResolver.Query(uri, projection, null, null, null);
                    cursor.MoveToFirst();
                    int columnIndex = cursor.GetColumnIndex(projection[0]);
                    ImagePath4 = cursor.GetString(columnIndex);
                    ImageName4 = ImagePath4.Substring(ImagePath4.LastIndexOf("/") + 1);
                    TextChooseImage4.Text = ImageName4;
                    cursor.Close();
                    Bitmap yourSelectedImage = BitmapFactory.DecodeFile(ImagePath4);
                    MemoryStream memStream = new MemoryStream();
                    string sFileExtension = ImageName4.Split('.')[1];
                    Bitmap.CompressFormat compressFormat = Bitmap.CompressFormat.Jpeg;
                    if (sFileExtension == "Png" || sFileExtension == "png" || sFileExtension == "PNG")
                    {
                        compressFormat = Bitmap.CompressFormat.Png;
                    }
                    else
                        if (sFileExtension == "Jpeg" || sFileExtension == "jpeg" || sFileExtension == "JPEG" || sFileExtension == "Jpg" || sFileExtension == "jpg" || sFileExtension == "JPG")
                    {
                        compressFormat = Bitmap.CompressFormat.Jpeg;
                    }
                    yourSelectedImage.Compress(compressFormat, 100, memStream);
                    ImageData4 = memStream.ToArray();
                    Android.Graphics.Drawables.Drawable d = new Android.Graphics.Drawables.BitmapDrawable(yourSelectedImage);
                    ImgChooseImage4.SetBackgroundDrawable(d);
                    ImgCloseFile1.Visibility = ViewStates.Visible;
                }
                
            }
            else
                if (requestCode == 2 && resultCode == Result.Ok)
            {
                var placepicked = PlacePicker.GetPlace(this, data);
                var geo = new Geocoder(this);
                string name = placepicked.NameFormatted.ToString();
                // var addresses = geo.GetFromLocation(placepicked.LatLng.Latitude, placepicked.LatLng.Longitude, 1);
                EtRemarks.Text = placepicked?.AddressFormatted.ToString();
                //GetAdressFromGeoCoder(data);
            }
        }


        private string GetRealPathFromURI(Android.Net.Uri uri)
        {
            string doc_id = "";
            using (var c1 = ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                string document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }
            string path = null;
            // The projection contains the columns we want to return in our query.
            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = ContentResolver.Query(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
        }




        // Loads the image from the phone storage to the screen
        private void LoadImage(Intent data, string filePath, string filename, TextView txtFileChoosen, byte[] picData, ImageView ImgChooseFile)
        {
            Android.Net.Uri uri = data.Data;
            string[] projection = { MediaStore.Images.Media.InterfaceConsts.Data };
            Android.Database.ICursor cursor = ContentResolver.Query(uri, projection, null, null, null);
            cursor.MoveToFirst();
            int columnIndex = cursor.GetColumnIndex(projection[0]);
            filePath = cursor.GetString(columnIndex);
            filename = filePath.Substring(filePath.LastIndexOf("/") + 1);
            txtFileChoosen.Text = filename;
            cursor.Close();
            Bitmap yourSelectedImage = BitmapFactory.DecodeFile(filePath);
            MemoryStream memStream = new MemoryStream();
            string sFileExtension = filename.Split('.')[1];
            Bitmap.CompressFormat compressFormat = Bitmap.CompressFormat.Jpeg;
            if (sFileExtension == "Png" || sFileExtension == "png" || sFileExtension == "PNG")
            {
                compressFormat = Bitmap.CompressFormat.Png;
            }
            else
                if (sFileExtension == "Jpeg" || sFileExtension == "jpeg" || sFileExtension == "JPEG" || sFileExtension == "Jpg" || sFileExtension == "jpg" || sFileExtension == "JPG")
            {
                compressFormat = Bitmap.CompressFormat.Jpeg;
            }
            yourSelectedImage.Compress(compressFormat, 100, memStream);
            picData = memStream.ToArray();
            Android.Graphics.Drawables.Drawable d = new Android.Graphics.Drawables.BitmapDrawable(yourSelectedImage);
            ImgChooseFile.SetBackgroundDrawable(d);
        }
    }



}