using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SteamChatCore.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings {
            get {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        const string SettingsKey = "settings_key";
        const string TokenKey = "token_key";
        const string IDKey = "id_key";

        static readonly string SettingsDefault = string.Empty;

        #endregion

        public static string GeneralSettings {
            get {
                return AppSettings.GetValueOrDefault (SettingsKey, SettingsDefault);
            }
            set {
                AppSettings.AddOrUpdateValue (SettingsKey, value);
            }
        }

        public static string AuthToken {
            get {
                return AppSettings.GetValueOrDefault (TokenKey, SettingsDefault);
            }
            set {
                AppSettings.AddOrUpdateValue (TokenKey, value);
            }
        }

        public static string SteamID {
            get {
                return AppSettings.GetValueOrDefault (IDKey, SettingsDefault);
            }
            set {
                AppSettings.AddOrUpdateValue (IDKey, value);
            }
        }
    }
}