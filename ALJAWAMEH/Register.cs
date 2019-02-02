using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace ALJAWAMEH
{
    [Activity(Label = "Start", Theme = "@style/Theme.DesignDemo")]
    public class Register : AppCompatActivity, GoogleApiClient.IConnectionCallbacks,GoogleApiClient.IOnConnectionFailedListener
    {
        EditText EtEmail;
        EditText EtPassword;
        EditText EtConfirmPassword;
        CheckBox CheckAgreeTerms;
        Button BtnRegister;
        private GoogleApiClient mGoogleApiClient;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register);
            EtEmail = FindViewById<EditText>(Resource.Id.editEmail);
            EtPassword = FindViewById<EditText>(Resource.Id.editPassword);
            EtConfirmPassword = FindViewById<EditText>(Resource.Id.editConfirmPassowrd);
            BtnRegister = FindViewById<Button>(Resource.Id.btnRegister);
           // LayoutSigninWithGoogle = FindViewById<RelativeLayout>(Resource.Id.LayoutSignWithGoogle);
          //  LayoutSigninWithFacebook = FindViewById<RelativeLayout>(Resource.Id.LayoutSigninWithFacebook);
            BtnRegister.Click += BtnRegister_Click;
           // LayoutSigninWithGoogle.Click += LayoutSigninWithGoogle_Click;
          //  LayoutSigninWithFacebook.Click += LayoutSigninWithFacebook_Click;
            ConfigureGoogleSignin();
        }

        private void LayoutSigninWithFacebook_Click(object sender, EventArgs e)
        {
            
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(requestCode==9001)
            {
                var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                HandleSignInResult(result);
            }
        }

        private void HandleSignInResult(GoogleSignInResult result)
        {
            var ssr = result.Status;
            //throw new NotImplementedException();
            if(result.IsSuccess)
            {
                var accountDetails = result.SignInAccount;
                Toast.MakeText(this, accountDetails.DisplayName + accountDetails.Email+ accountDetails.FamilyName, ToastLength.Long).Show();
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
            StartActivityForResult(signInIntent,9001);
        }

        private void ConfigureGoogleSignin()
        {
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn).RequestEmail().Build();
            mGoogleApiClient = new GoogleApiClient.Builder(this).EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso).AddConnectionCallbacks(this).Build();
        }



        

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (EtPassword.Text.Trim() != "" && EtConfirmPassword.Text.Trim() != "" && EtEmail.Text.Trim() != "")
            {
                string email = EtEmail.Text.ToString();
                if (Android.Util.Patterns.EmailAddress.Matcher(email).Matches())
                {
                    if (EtPassword.Text == EtConfirmPassword.Text)
                    {
                        Intent i = new Intent(this, typeof(Login));
                        var spDevice = Application.Context.GetSharedPreferences("APP_INFO", FileCreationMode.Private);
                        var Loginedit = spDevice.Edit();
                        Loginedit.PutString("AccountCreated", "YES");
                        Loginedit.PutString("EmailRegistered", EtEmail.Text);
                        Loginedit.PutString("PasswordRegistered", EtPassword.Text);
                        Loginedit.Commit();
                        StartActivity(i);
                    }
                    else
                    {
                        Android.App.AlertDialog.Builder alertDialogBuilder = new Android.App.AlertDialog.Builder(this);
                        alertDialogBuilder.SetMessage("Passowrd and Confirm Password does not match")
                        .SetCancelable(false)
                        .SetPositiveButton("OK", EventOk);
                        Android.App.AlertDialog alert = alertDialogBuilder.Create();
                        alert.Show();
                    }
                }
                else
                {
                    EtEmail.Error = (Html.FromHtml("Invalid Email Adress")).ToString();
                    EtEmail.RequestFocus();
                }
            }
            else
            {
                if (String.IsNullOrEmpty(EtPassword.Text.Trim()))
                {
                    EtPassword.Error = (Html.FromHtml("Required")).ToString();
                    EtPassword.RequestFocus();
                }

                if (String.IsNullOrEmpty(EtConfirmPassword.Text.Trim()))
                {
                    EtConfirmPassword.Error = (Html.FromHtml("Required")).ToString();
                    EtConfirmPassword.RequestFocus();
                }

                if (String.IsNullOrEmpty(EtEmail.Text.Trim()))
                {
                    EtEmail.Error = (Html.FromHtml("Required")).ToString();
                    EtEmail.RequestFocus();
                }
            }
        }

        private void EventOk(object sender, DialogClickEventArgs e)
        {
            
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
