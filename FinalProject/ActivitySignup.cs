using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    [Activity(Label = "Signup")]
    public class ActivitySignup : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.sign_up);
            //ActionBar.SetDisplayShowHomeEnabled(true);
            //ActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        /*  public override bool OnOptionsItemSelected(IMenuItem item)
        {
             if(item.ItemId == Android.Resource.Id.Home)
             {
                 Finish();
                 return true;
             }
             return base.OnOptionsItemSelected(item);
         }*/
    }
}