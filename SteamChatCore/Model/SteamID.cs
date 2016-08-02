using System.Collections.Generic;

namespace SteamChatCore.Model
{
    public class SteamID
    {
        string _steamID;

        public long LongSteamID {
            get {
                return long.Parse (_steamID);
            }
        }

        public SteamID (SteamSharp.SteamID steamID)
        {
            _steamID = steamID.ToString ();
        }

        public override string ToString ()
        {
            return _steamID;
        }
    }

    public static class SteamIDExtensions
    {
        public static List<SteamID> FromSteamSharpList (this List<SteamSharp.SteamID> ids)
        {
            var newIDs = new List<SteamID> ();
            foreach (var id in ids) {
                newIDs.Add (new SteamID (id));
            }
            return newIDs;
        }
    }
}
