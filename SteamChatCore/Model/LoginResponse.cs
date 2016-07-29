using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamChatCore.Model
{
    public enum LoginResponse
    {
        CaptchaThenSteamGuard,
        JustCaptcha,
        JustSteamGuard,
        IncorrectDetails,
        Success,
        Failed
    }

    public class CaptchaReturn
    {
        public string CaptchaGID { get; set; }

        public string CaptchaURL { get; set; }

        public CaptchaReturn (string gid, string url)
        {
            CaptchaGID = gid;
            CaptchaURL = url;
        }
    }

    public class SteamGuardReturn
    {
        public string SteamGuardID { get; set; }

        public string SteamGuardEmailDomain { get; set; }

        public SteamGuardReturn (string id, string email)
        {
            SteamGuardID = id;
            SteamGuardEmailDomain = email;
        }
    }

    public class CaptchaAndSteamGuardReturn
    {
        public CaptchaReturn CaptchaInfo { get; set; }

        public SteamGuardReturn SteamGuardInfo { get; set; }

        public CaptchaAndSteamGuardReturn (string gid, string url, string id, string email)
        {
            CaptchaInfo = new CaptchaReturn (gid, url);
            SteamGuardInfo = new SteamGuardReturn (id, email);
        }
    }
}


