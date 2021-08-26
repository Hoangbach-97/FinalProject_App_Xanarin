using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace FinalProject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView textViewSignup;
        TextView textViewForgotPass;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
           // SetContentView(Resource.Layout.forggot_pass);
            SetContentView(Resource.Layout.activity_main);

            //direct to Sign up
            textViewSignup = FindViewById<TextView>(Resource.Id.textViewSignup);
            textViewSignup.Click += delegate { StartActivity(typeof(ActivitySignup)); };

            // Direct to Forgotpass
            textViewForgotPass = FindViewById<TextView>(Resource.Id.textViewForgotPass);
            textViewForgotPass.Click += delegate { StartActivity(typeof(ActivityForgotPass)); };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}