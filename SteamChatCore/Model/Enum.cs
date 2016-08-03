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

    public enum PersonaState
    {
        Offline,
        Online,
        Busy,
        Away,
        Snooze,
        LookingToTrade,
        LookingToPlay
    }
}


