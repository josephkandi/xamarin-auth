# Setup

For this to work you will need to change the Google client ID for Android and iOS and also the Facebook App ID.

In the `Constants.cs` file change this :

```csharp
// Change to the iOS client id from Google console    
public static string ClientIdiOS = "ios-client-id-gc6ih5922efrnpvlgh7fh25n40m3it7d.apps.googleusercontent.com";
// Change to the Android client id from Google console
public static string ClientIdAndroid = "android-client-id-rfabh2o24oa2p9991pjeq2m0h7cil36o.apps.googleusercontent.com";

// Change the Android client id redirect url
public static string RedirectUrlAndroid = "com.googleusercontent.apps.app-id-rfabh2o24oa2p9991pjeq2m0h7cil36o:/oauth2redirect";

//  Change the ios client id redirect url
public static string RedirectUrliOS = "com.googleusercontent.apps.app-id-gc6ih5922efrnpvlgh7fh25n40m3it7d:/oauth2redirect";
```

## Android Project Facebook Login

In the `FacebookUrlInterceptorActivity.cs` change the intent filter `DataScheme` to the Facebook app id. The id should start with `fb`-your-facebook-app-id.

```csharp
[IntentFilter
    (
        actions: new []{ Intent.ActionView}, 
        AutoVerify = true, 
        Categories = new []{ Intent.CategoryDefault,Intent.CategoryBrowsable}, 
        DataScheme = "fb-app-id"
    )
]
```

## Android Project Google Login

In the `CustomUrlSchemeInterceptorActivity.cs` intent filter registration change the `DataScheme` to the Google app id. It should start with `com.googleusercontent.apps` and then your app id.

```csharp
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
```

## iOS URL Scheme Registration for Google and Facebook

In the `Info.plist` file, add the following keys :

```xml
<key>CFBundleURLTypes</key>
<array>
    <dict>
        <key>CFBundleURLName</key>
        <string>Xamarin.Auth Google OAuth</string>
        <key>CFBundleURLSchemes</key>
        <array>
            <string>google-app-id-here</string>
            <string>fb-id-here</string>
        </array>
        <key>CFBundleURLTypes</key>
        <string>Viewer</string>
    </dict>
</array>
```

The Facebook app id should start with `fb` and then your app id, e.g `fb0000000000000` and the Google client id should start with `com.googleusercontent.apps-something-here`.

That's all the changes you need to make for the app to run.