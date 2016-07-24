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

        public async Task<Tuple<List<string>, LoginResponse>> Login (string username, string password)
        {
            var client = new SteamClient ();
            var result = await Task.Run (() => UserAuthenticator.GetAccessTokenForUser (username, password));

            if (result.SteamResponseMessage == "Incorrect login") {
                return new Tuple<List<string>, LoginResponse> (null, LoginResponse.IncorrectDetails);
            }

            if (result.IsCaptchaNeeded && result.IsSteamGuardNeeded) {
                var data = new List<string> { result.CaptchaURL, result.CaptchaGID, result.SteamGuardID, result.SteamGuardEmailDomain };
                return new Tuple<List<string>, LoginResponse> (data, LoginResponse.CaptchaThenSteamGuard);
            }

            if (result.IsCaptchaNeeded) {
                var data = new List<string> { result.CaptchaURL, result.CaptchaGID };
                return new Tuple<List<string>, LoginResponse> (data, LoginResponse.JustCaptcha);
            }

            if (result.IsSteamGuardNeeded) {
                var data = new List<string> { result.SteamGuardID, result.SteamGuardEmailDomain };
                return new Tuple<List<string>, LoginResponse> (data, LoginResponse.JustSteamGuard);
            }

            // cache auth token, set other details later
            Helpers.Settings.AuthToken = result.User.OAuthAccessToken;

            return new Tuple<List<string>, LoginResponse> (null, LoginResponse.Success);
        }
    }
}
