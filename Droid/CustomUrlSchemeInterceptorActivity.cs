
using System;

using Android.App;
using Android.Content;
using Android.OS;

namespace Dealanto.Droid
{
    [Activity(Label = "AuthHandler", NoHistory = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    [
        IntentFilter
        (
            new[] { Intent.ActionView },
            Categories = new[] {
                Intent.CategoryDefault, Intent.CategoryBrowsable
            },
            DataSchemes = new[] {
            "com.googleusercontent.apps.app-id"
            },
            DataHosts = new[] {
                "authorize"
            },
            DataPaths = new[] { 
                "/oauth2redirect"
            },
            AutoVerify = true
        )
    ]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Convert Android.Net.Url to Uri
            var uri = new Uri(Intent.Data.ToString());

            // Load redirectUrl page
            AuthenticationState.Authenticator.OnPageLoading(uri);

            Finish();
        }
    }
}
