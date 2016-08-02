using SteamChatCore.Helpers;
using SteamChatCore.Model;
using SteamSharp;
using SteamSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SteamChatCore.Controllers
{
    public sealed class ChatController
    {
        static readonly ChatController instance = new ChatController ();

        public static ChatController Instance {
            get {
                return instance;
            }
        }

        private ChatController () { }

        SteamChatClient chatClient;

        public async Task<bool> LogOntoChat ()
        {
            chatClient = new SteamChatClient ();
            var client = new SteamClient ();
            client.Authenticator = UserAuthenticator.ForProtectedResource (Settings.AuthToken);

            chatClient.SteamChatConnectionChanged += SteamChatConnectionChangedHandler;
            chatClient.SteamChatMessagesReceived += SteamChatMessagesReceivedHandler;
            chatClient.SteamChatUserStateChange += SteamChatUserStateChangeHandler;

            try {
                await chatClient.LogOn (client);
            } catch (SteamRequestException e) {
                Debug.WriteLine (e.Message);
                return false;
            }

            return true;
        }

        public /*Dictionary<Model.SteamID, Model.SteamUser>*/List<Model.SteamUser> Friends {
            get {
                //var keys = chatClient.FriendsList.Friends.Keys.ToList ().FromSteamSharpList ();
                //var values = chatClient.FriendsList.Friends.Values.ToList ().FromSteamSharpList ();
                //return keys.Zip (values, (k, v) => new { Key = k, Value = v } ).ToDictionary (x => x.Key, x => x.Value);
                var friends = chatClient.FriendsList.Friends.Values.ToList ().FromSteamSharpList ();
                foreach (var friend in friends) {
                    friend.SteamID.ToString ();
                }
                return chatClient.FriendsList.Friends.Values.ToList ().FromSteamSharpList ();
            }
        }

        void SteamChatUserStateChangeHandler (object sender, SteamChatClient.SteamChatUserStateChangeEventArgs e)
        {
            //throw new NotImplementedException ();
        }

        void SteamChatMessagesReceivedHandler (object sender, SteamChatClient.SteamChatMessagesReceivedEventArgs e)
        {
            //throw new NotImplementedException ();
        }

        void SteamChatConnectionChangedHandler (object sender, SteamChatClient.SteamChatConnectionChangeEventArgs e)
        {
            //throw new NotImplementedException ();
        }
    }
}
