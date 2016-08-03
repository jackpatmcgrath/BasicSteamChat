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
            chatClient.SteamChatConnectionChanged += SteamChatConnectionChangedHandler;
            chatClient.SteamChatMessagesReceived += SteamChatMessagesReceivedHandler;
            chatClient.SteamChatUserStateChange += SteamChatUserStateChangeHandler;

            try {
                await chatClient.LogOn (AuthenticatedClient);
            } catch (SteamRequestException e) {
                Debug.WriteLine (e.Message);
                return false;
            }

            return true;
        }

        public async Task UpdateFriends ()
        {
            await SteamCommunity.GetBulkProfileDataAsync (AuthenticatedClient, chatClient.FriendsList.Friends);
        }

        public SteamClient AuthenticatedClient {
            get {
                var client = new SteamClient ();
                client.Authenticator = UserAuthenticator.ForProtectedResource (Settings.AuthToken);
                return client;
            }
        }

        public List<Model.SteamUser> Friends {
            get {
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
