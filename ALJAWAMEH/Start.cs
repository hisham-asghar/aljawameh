using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Support.V7.App;
using Android.Text;
using Xamarin.Auth;
using Newtonsoft.Json;
using Android.Graphics;
using System.Net;

namespace ALJAWAMEH
{
    [Activity(Label = "Login", Theme = "@style/Theme.DesignDemo")]
    public class Start : AppCompatActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        Button BtnRegister;
        Button BtnLogin;
        RelativeLayout LayoutSigninWithFacebook;
        RelativeLayout LayoutSigninWithGoogle;
        private GoogleApiClient mGoogleApiClient;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            var spRegister = Application.Context.GetSharedPreferences("APP_INFO", FileCreationMode.Private);
            string sKeyVal = spRegister.GetString("Registered", null);
            string sAccountCreated = spRegister.GetString("AccountCreated", null);

            if (sKeyVal == "YES")
            {
                Intent i = new Intent(this, typeof(MapScreen));
                StartActivity(i);
            }

            if (sAccountCreated == "YES")
            {
                Intent i = new Intent(this, typeof(Login));
                StartActivity(i);
            }
            // Create your application here
            SetContentView(Resource.Layout.Start);
            BtnRegister = FindViewById<Button>(Resource.Id.btnRegisterDummy);
            BtnLogin = FindViewById<Button>(Resource.Id.btnLoginDummy);
            LayoutSigninWithGoogle = FindViewById<RelativeLayout>(Resource.Id.LayoutSignWithGoogle);
            LayoutSigninWithFacebook = FindViewById<RelativeLayout>(Resource.Id.LayoutSigninWithFacebook);
            BtnRegister.Click += BtnRegister_Click;
            BtnLogin.Click += BtnLogin_Click;
            LayoutSigninWithGoogle.Click += LayoutSigninWithGoogle_Click;
            LayoutSigninWithFacebook.Click += LayoutSigninWithFacebook_Click;
            ConfigureGoogleSignin();

        }
        private void LayoutSigninWithFacebook_Click(object sender, EventArgs e)
        {
            var Auth = new OAuth2Authenticator(clientId: "2185563471761131",
               scope: "",
               authorizeUrl: new System.Uri("https://m.facebook.com/dialog/oauth"),
               redirectUrl: new System.Uri("https://www.facebook.com/connect/login_success.html"));
            Auth.Completed += Auth_Completed;
            var ui = Auth.GetUI(this);
            StartActivity(ui);
        }

        private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var request = new OAuth2Request(
                    "GET", new System.Uri("https://graph.facebook.com/me?fields=name,picture,cover,birthday,email,address"),
                    null, e.Account);
                var fbresponse = await request.GetResponseAsync();
                var json = fbresponse.GetResponseText();
                var fbuser = JsonConvert.DeserializeObject<FacebookUser>(json);
                var name = fbuser.Name;
                var email = fbuser.Email;
                var id = fbuser.Id;
                var picture = fbuser.Picture.Data.Url;
                var spDevice = Application.Context.GetSharedPreferences("APP_INFO", FileCreationMode.Private);
                var Loginedit = spDevice.Edit();
                Loginedit.PutString("Registered", "YES");
                Loginedit.PutString("FirstName", name.Split(' ')[0]);
                Loginedit.PutString("LastName",name.Split(' ')[1]);
                Loginedit.PutString("Email", email);
                Loginedit.PutString("PhotoUri", picture.ToString());
                Loginedit.PutString("UserId", id);
                Loginedit.Commit();
                Intent i = new Intent(this, typeof(MapScreen));
                StartActivity(i);


                // var cover = fbuser.Cover.Source;

                //  TvName.Text = name;
                //  TvID.Text = email;
                //  ImgPicture.SetImageBitmap(GetImageBitmapFromURL(picture));
                // ImgCover.SetImageBitmap(GetImageBitmapFromURL(cover));
            }
        }

        private Bitmap GetImageBitmapFromURL(string url)
        {
            Bitmap imagebitmap = null;
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imagebitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return imagebitmap;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 9001)
            {
                var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                HandleSignInResult(result);
            }
        }
        public override void OnBackPressed()
        {
            
        }

        private void HandleSignInResult(GoogleSignInResult result)
        {
            var ssr = result.Status;
            //throw new NotImplementedException();
            if (result.IsSuccess)
            {
                var accountDetails = result.SignInAccount;
                Toast.MakeText(this, accountDetails.DisplayName + accountDetails.Email + accountDetails.FamilyName, ToastLength.Long).Show();
                var spDevice = Application.Context.GetSharedPreferences("APP_INFO", FileCreationMode.Private);
                var Loginedit = spDevice.Edit();
                Loginedit.PutString("Registered", "YES");
                Loginedit.PutString("FirstName", accountDetails.GivenName);
                Loginedit.PutString("LastName", accountDetails.FamilyName);
                Loginedit.PutString("Email", accountDetails.Email);
                Loginedit.PutString("PhotoUri", (accountDetails.PhotoUrl).ToString());
                Loginedit.PutString("UserId", accountDetails.Id);
                Loginedit.Commit();
                Intent i = new Intent(this, typeof(MapScreen));
                StartActivity(i);
            }

        }

        private void LayoutSigninWithGoogle_Click(object sender, EventArgs e)
        {
            mGoogleApiClient.Connect();
            var signInIntent = Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
            StartActivityForResult(signInIntent, 9001);
        }

        private void ConfigureGoogleSignin()
        {
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn).RequestEmail().Build();
            mGoogleApiClient = new GoogleApiClient.Builder(this).EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso).AddConnectionCallbacks(this).Build();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(Login));
            StartActivity(i);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(Register));
            StartActivity(i);
        }

        public void OnConnected(Bundle connectionHint)
        {
            //throw new NotImplementedException();
        }

        public void OnConnectionSuspended(int cause)
        {
            // throw new NotImplementedException();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            // throw new NotImplementedException();
        }
    }
}