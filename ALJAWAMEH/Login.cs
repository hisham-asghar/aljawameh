using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace ALJAWAMEH
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        Button BtnLogin;
        EditText EtEmail;
        EditText EtPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);
            BtnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            EtEmail = FindViewById<EditText>(Resource.Id.editEmail);
            EtPassword = FindViewById<EditText>(Resource.Id.editPassword);
            BtnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {

            if (EtPassword.Text.Trim() != "" && EtEmail.Text.Trim() != "")
            {
                string email = EtEmail.Text.ToString();
                if (Android.Util.Patterns.EmailAddress.Matcher(email).Matches())
                {
                    var spDevice = Application.Context.GetSharedPreferences("APP_INFO", FileCreationMode.Private);
                    string sKeyEmail = spDevice.GetString("EmailRegistered", null);
                    string sKeyPassword = spDevice.GetString("PasswordRegistered", null);
                    //if (EtPassword.Text == sKeyPassword)
                    //{

                        //if (EtEmail.Text == sKeyEmail)
                        //{
                            var spDevice1 = Application.Context.GetSharedPreferences("APP_INFO", FileCreationMode.Private);

                            var Loginedit = spDevice1.Edit();
                            Loginedit.PutString("Registered", "YES");
                            Loginedit.Commit();
                            Intent i = new Intent(this, typeof(MapScreen));
                            StartActivity(i);
                        //}
                        //else
                        //{
                        //    Android.App.AlertDialog.Builder alertDialogBuilder = new Android.App.AlertDialog.Builder(this);
                        //    alertDialogBuilder.SetMessage("Incorrect Email")
                        //    .SetCancelable(false)
                        //    .SetPositiveButton("OK", EventOk);
                        //    Android.App.AlertDialog alert = alertDialogBuilder.Create();
                        //    alert.Show();
                        //}
                    //}
                    //else
                    //{
                    //    Android.App.AlertDialog.Builder alertDialogBuilder = new Android.App.AlertDialog.Builder(this);
                    //    alertDialogBuilder.SetMessage("Incorrect Password")
                    //    .SetCancelable(false)
                    //    .SetPositiveButton("OK", EventOk);
                    //    Android.App.AlertDialog alert = alertDialogBuilder.Create();
                    //    alert.Show();
                    //}
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

        public override void OnBackPressed()
        {
            
        }
    }
}