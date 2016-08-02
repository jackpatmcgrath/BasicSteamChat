using SteamChatCore.Model;
using SteamSharp;
using SteamSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SteamChatCore
{
    public sealed class LoginController
    {
        static readonly LoginController instance = new LoginController ();

        public static LoginController Instance {
            get {
                return instance;
            }
        }

        private LoginController () { }

        UserAuthenticator.SteamAccessRequestResult result;

        public async Task<Tuple<string, LoginResponse>> InitialLogin (string username, string password)
        {
            var client = new SteamClient ();

            try {
                result = await Task.Run (() => UserAuthenticator.GetAccessTokenForUser (username, password));
            } catch (SteamRequestException e) {
                Debug.WriteLine (e.Message);
                return new Tuple<string, LoginResponse> (null, LoginResponse.Failed);
            }

            Debug.WriteLineIf (!string.IsNullOrEmpty (result.SteamResponseMessage), result.SteamResponseMessage);
            return CheckResult ();
        }

        public async Task<Tuple<string, LoginResponse>> CaptchaLogin (string username, string password, string answer)
        {
            var captchaAnswer = new UserAuthenticator.CaptchaAnswer {
                GID = result.CaptchaGID,
                SolutionText = answer
            };

            try {
                result = await Task.Run (() => UserAuthenticator.GetAccessTokenForUser (username, password, null, captchaAnswer));
            } catch (SteamRequestException e) {
                Debug.WriteLine (e.Message);
                return new Tuple<string, LoginResponse> (null, LoginResponse.Failed);
            }

            Debug.WriteLineIf (!string.IsNullOrEmpty (result.SteamResponseMessage), result.SteamResponseMessage);
            return CheckResult ();
        }

        public async Task<Tuple<string, LoginResponse>> SteamGuardLogin (string username, string password, string answer)
        {
            var steamGuardAnswer = new UserAuthenticator.SteamGuardAnswer {
                ID = result.SteamGuardID,
                SolutionText = answer
            };

            try {
                result = await Task.Run (() => UserAuthenticator.GetAccessTokenForUser (username, password, steamGuardAnswer, null));
            } catch (SteamRequestException e) {
                Debug.WriteLine (e.Message);
                return new Tuple<string, LoginResponse> (null, LoginResponse.Failed);
            }

            Debug.WriteLineIf (!string.IsNullOrEmpty (result.SteamResponseMessage), result.SteamResponseMessage);
            return CheckResult ();
        }

        Tuple<string, LoginResponse> CheckResult ()
        {
            if (result.SteamResponseMessage == "Incorrect login.") {
                return new Tuple<string, LoginResponse> (null, LoginResponse.IncorrectDetails);
            }

            if (result.IsCaptchaNeeded) {
                return new Tuple<string, LoginResponse> (result.CaptchaURL, LoginResponse.JustCaptcha);
            }

            if (result.IsSteamGuardNeeded) {
                return new Tuple<string, LoginResponse> (result.SteamGuardEmailDomain, LoginResponse.JustSteamGuard);
            }

            if (result.IsSuccessful) {
                // cache auth token, set other details later
                Helpers.Settings.AuthToken = result.User.OAuthAccessToken;
                Helpers.Settings.SteamID = result.User.SteamID.ToString ();
                return new Tuple<string, LoginResponse> (null, LoginResponse.Success);
            }

            return new Tuple<string, LoginResponse> (null, LoginResponse.Failed);
        }

        public void Reset ()
        {
            result = null;
        }
    }
}
