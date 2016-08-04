using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamChatCore.Model
{
    public class SteamUser
    {
        public SteamID SteamID { get; set; }

        public string PersonaName { get; set; }

        public string RealName { get; set; }

        public string AvatarURL { get; set; }

        public string AvatarMediumURL { get; set; }

        public string AvatarFullURL { get; set; }

        public string GameExtraInfo { get; set; }

        public DateTime LastLogOff { get; set; }

        public PersonaState PersonaState { get; set; }

        public SteamUser (SteamSharp.SteamUser user)
        {
            SteamID = new SteamID (user.SteamID);
            if (user.PlayerInfo != null) {
                PersonaName = user.PlayerInfo.PersonaName;
                RealName = user.PlayerInfo.RealName;
                AvatarURL = user.PlayerInfo.AvatarURL;
                AvatarMediumURL = user.PlayerInfo.AvatarMediumURL;
                AvatarFullURL = user.PlayerInfo.AvatarFullURL;
                GameExtraInfo = user.PlayerInfo.GameExtraInfo;
                LastLogOff = user.PlayerInfo.LastLogOff;
                PersonaState = (PersonaState)(int)user.PlayerInfo.PersonaState;
            }
        }
    }

    public static class SteamUserExtensions
    {
        public static List<SteamUser> FromSteamSharpList (this List<SteamSharp.SteamUser> users)
        {
            var newUsers = new List<SteamUser> ();
            foreach (var user in users) {
                newUsers.Add (new SteamUser (user));
            }
            return newUsers;
        }
    }
}
