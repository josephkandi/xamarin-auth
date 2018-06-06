
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

namespace Dealanto.Droid
{
    [Activity(Label = "FacebookUrlInterceptorActivity")]
    [IntentFilter
     (
         actions: new []{ Intent.ActionView}, 
         AutoVerify = true, 
         Categories = new []{ Intent.CategoryDefault,Intent.CategoryBrowsable}, 
         DataScheme = "fb-app-id"
     )
    ]
    public class FacebookUrlInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var uri = new Uri(Intent.Data.ToString());
            AuthenticationState.Authenticator.OnPageLoading(uri);
            Finish();
        }
    }
}
