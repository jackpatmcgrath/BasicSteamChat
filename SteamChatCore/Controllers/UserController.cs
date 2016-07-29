using SteamChatCore.Model;
using SteamSharp;
using SteamSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamChatCore
{
    public sealed class UserController
    {
        static readonly UserController instance = new UserController ();

        public static UserController Instance {
            get {
                return instance;
            }
        }

        private UserController () { }

        // Returns a data object and an enum representing the login response
        public async Task<Tuple<object, LoginResponse>> Login (string username, string password)
        {
            var client = new SteamClient ();
            var result = await Task.Run (() => UserAuthenticator.GetAccessTokenForUser (username, password));
            System.Diagnostics.Debug.WriteLine (result.SteamResponseMessage);

            if (result.SteamResponseMessage == "Incorrect login.") {
                System.Diagnostics.Debug.WriteLine ("Incorrect login, returning nothing.");
                return new Tuple<object, LoginResponse> (null, LoginResponse.IncorrectDetails);
            }

            if (result.IsCaptchaNeeded && result.IsSteamGuardNeeded) {
                System.Diagnostics.Debug.WriteLine ("Captcha and authentication needed, returning data.");
                var data = new CaptchaAndSteamGuardReturn (result.CaptchaGID, result.CaptchaGID, result.SteamGuardID, result.SteamGuardEmailDomain);
                return new Tuple<object, LoginResponse> (data, LoginResponse.CaptchaThenSteamGuard);
            }

            if (result.IsCaptchaNeeded) {
                System.Diagnostics.Debug.WriteLine ("Captcha needed, returning data.");
                var data = new CaptchaReturn (result.CaptchaGID, result.CaptchaURL);
                return new Tuple<object, LoginResponse> (data, LoginResponse.JustCaptcha);
            }

            if (result.IsSteamGuardNeeded) {
                System.Diagnostics.Debug.WriteLine ("Steam guard needed, returning data.");
                var data = new SteamGuardReturn (result.SteamGuardID, result.SteamGuardEmailDomain);
                return new Tuple<object, LoginResponse> (data, LoginResponse.JustSteamGuard);
            }

            result = UserAuthenticator.GetAccessTokenForUser (username, password, null, null);

            if (result.IsSuccessful) {
                // cache auth token, set other details later
                System.Diagnostics.Debug.WriteLine (string.Format ("Authentication token: {0}", result.User.OAuthAccessToken));
                Helpers.Settings.AuthToken = result.User.OAuthAccessToken;
                return new Tuple<object, LoginResponse> (null, LoginResponse.Success);
            }

            return new Tuple<object, LoginResponse> (null, LoginResponse.Failed);
        }
    }
}
