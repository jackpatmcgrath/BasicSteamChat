using SteamChatCore.Helpers;
using SteamSharp;
using SteamSharp.Authenticators;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SteamChatCore.Controllers
{
    public sealed class FriendsController
    {
        static readonly FriendsController instance = new FriendsController ();

        public static FriendsController Instance {
            get {
                return instance;
            }
        }

        private FriendsController () { }

        public async Task<Dictionary<SteamID, SteamUser>> FetchFriends ()
        {
            var client = new SteamClient ();
            client.Authenticator = UserAuthenticator.ForProtectedResource (Settings.AuthToken);

            SteamFriendsList result = null;
            try {
                result = await Task.Run (() => SteamCommunity.GetFriendsList (client, new SteamID (Settings.SteamID)));
            } catch (SteamRequestException e) {
                Debug.WriteLine (e.Message);
            }

            return result != null ? result.Friends : null;
        }
    }
}
