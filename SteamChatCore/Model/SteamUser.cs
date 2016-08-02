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

        public SteamUser (SteamSharp.SteamUser user)
        {
            SteamID = new SteamID (user.SteamID);
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
