using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Auth;

namespace Dealanto.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static OAuth2Authenticator _oAuth2Authenticator;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();
            //UIKit.UIViewController ui_object = _oAuth2Authenticator.GetUI(); gives an error. not initialized

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

		public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
		{
            var uri = new Uri(url.AbsoluteString);
            AuthenticationState.Authenticator.OnPageLoading(uri);
            return true;
		}
	}
}
