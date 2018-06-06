using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Auth;
using Dealanto.Utils;
using System.Diagnostics;
using Dealanto.Models;
using System.Linq;
using Newtonsoft.Json;

namespace Dealanto.Views.Pages
{
    public partial class LoginPage : ContentPage
    {
        OAuth2Authenticator _oAuth2Authenticator;
        OAuth2Authenticator _fbOAuth2Authenticator;
        Account _account;
        readonly AccountStore _store;

        public LoginPage()
        {
            InitializeComponent();
            _store = AccountStore.Create();
            _account = _store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            btnLogin.Clicked += OnAuth;
            btnFacebookLogin.Clicked += OnFacebookLogin;
        }

        private void OnFacebookLogin(object sender, EventArgs e)
        {
           var authenticator = new OAuth2Authenticator(Constants.FacebookAppId, 
                                                            "email",
                                                            new Uri(Constants.FacebookAuthorizeUrl), 
                                                            new Uri(Constants.FacebookRedirectUrl), 
                                                            null,
                                                       false);
            authenticator.AllowCancel = true;
            authenticator.Completed += OnFacebookAuthCompleted;
            authenticator.Error += OnAuthError;
            AuthenticationState.Authenticator = authenticator;

            new Xamarin.Auth.Presenters.OAuthLoginPresenter().Login(authenticator);
        }

        async void OnFacebookAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            if (e.IsAuthenticated)
            {
                var tokeType = e.Account.Properties.ContainsKey("token_type") ? e.Account.Properties["token_type"] : "";
                var accessToken = e.Account.Properties.ContainsKey("access_token") ? e.Account.Properties["access_token"] : "";
                var expires = e.Account.Properties.ContainsKey("expires_in") ? e.Account.Properties["expires_in"] : "";

                Debug.WriteLine($"{tokeType} Access Token : {accessToken}");
                var request = new OAuth2Request("GET", new Uri(Constants.FacebookUserProfileUrl),null, e.Account);
                await request.GetResponseAsync().ContinueWith(t => {
                    if (t.IsFaulted)
                        Console.WriteLine("Error: " + t.Exception.InnerException.Message);
                    else
                    {
                        string json = t.Result.GetResponseText();
                        Console.WriteLine(json);
                    }
                });

                if (_account != null)
                {
                    await _store.DeleteAsync(_account, Constants.AppName);
                }
                //Can only save on the physical phone to the keychain, otherwise an exception is known, bug on iOS simulator
                //await _store.SaveAsync(_account = e.Account, Constants.AppName);
            }            
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            Debug.WriteLine(e.Message);
        }

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            UserProfile userProfile = null;

            if (e.IsAuthenticated)
            {
                var tokeType = e.Account.Properties.ContainsKey("token_type") ? e.Account.Properties["token_type"] : "";
                var accessToken = e.Account.Properties.ContainsKey("access_token") ?  e.Account.Properties["access_token"] : "";
                var expires = e.Account.Properties.ContainsKey("expires_in") ? e.Account.Properties["expires_in"] : "";

                Debug.WriteLine($"{tokeType} Access Token : {accessToken}");
                var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    var result = await response.GetResponseTextAsync();

                    userProfile = JsonConvert.DeserializeObject<UserProfile>(result, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    Debug.WriteLine(result);
                }

                if (_account != null)
                {
                    await _store.DeleteAsync(_account, Constants.AppName);
                    await _store.SaveAsync(_account = e.Account, Constants.AppName);
                }
            }
        }

        void OnAuth(object sender, EventArgs e)
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.ClientIdiOS;
                    redirectUri = Constants.RedirectUrliOS;
                    break;

                case Device.Android:
                    clientId = Constants.ClientIdAndroid;
                    redirectUri = Constants.RedirectUrlAndroid;
                    break;
            }

            _oAuth2Authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.Scope,
                new Uri(Constants.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.AccessTokeUrl),
                null,
                true);
            
            _oAuth2Authenticator.Completed += OnAuthCompleted;
            _oAuth2Authenticator.Error += OnAuthError;
            AuthenticationState.Authenticator = _oAuth2Authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(_oAuth2Authenticator);
        }
    }
}
