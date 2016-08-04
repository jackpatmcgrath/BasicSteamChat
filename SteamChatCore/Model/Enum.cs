namespace SteamChatCore.Model
{
    public enum LoginResponse
    {
        Captcha,
        SteamGuard,
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

    public static class EnumExtensions
    {
        public static string PersonaStateToString (this PersonaState personaState)
        {
            switch (personaState) {
                case PersonaState.Offline:
                    return "Offline";
                case PersonaState.Online:
                    return "Online";
                case PersonaState.Busy:
                    return "Busy";
                case PersonaState.Away:
                    return "Away";
                case PersonaState.Snooze:
                    return "Snooze";
                case PersonaState.LookingToTrade:
                    return "Looking to Trade";
                case PersonaState.LookingToPlay:
                    return "Looking to Play";
                default:
                    return string.Empty;
            }
        }
    }
}


