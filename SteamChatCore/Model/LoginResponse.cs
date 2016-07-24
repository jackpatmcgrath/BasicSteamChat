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
        Success
    }
}
