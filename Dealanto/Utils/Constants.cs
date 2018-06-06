using System;
namespace Dealanto.Utils
{
    public class Constants
    {
        public static string ClientIdiOS = "ios-client-id-gc6ih5922efrnpvlgh7fh25n40m3it7d.apps.googleusercontent.com";
        public static string ClientIdAndroid = "android-client-id-rfabh2o24oa2p9991pjeq2m0h7cil36o.apps.googleusercontent.com";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/v2/auth";
        public static string RedirectUrlAndroid = "com.googleusercontent.apps.app-id-rfabh2o24oa2p9991pjeq2m0h7cil36o:/oauth2redirect";
        public static string RedirectUrliOS = "com.googleusercontent.apps.app-id-gc6ih5922efrnpvlgh7fh25n40m3it7d:/oauth2redirect";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
        public static string AccessTokeUrl = "https://www.googleapis.com/oauth2/v4/token";
        public const string Scope = "email";
        public const string AppName = "Dealanto";

        // Facebook Constants
        public const string FacebookAppId = "facebook-app-id";
        public const string FacebookAuthorizeUrl = "https://www.facebook.com/v2.9/dialog/oauth";
        public static readonly string FacebookRedirectUrl = $"fb{FacebookAppId}://authorize";
        public const string FacebookUserProfileUrl = "https://graph.facebook.com/me?fields=id,name,email";

    }
}
