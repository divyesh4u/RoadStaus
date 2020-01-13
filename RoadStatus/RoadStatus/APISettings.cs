using System.Configuration;

namespace RoadStatus
{
    public sealed class APISettings
    {
        private static APISettings apiSettingInstance;
        private static readonly object readOnlyObject = new object();

        private APISettings()
        {

        }

        public static APISettings APISettingInstance
        {
            get
            {
                lock (readOnlyObject)
                {
                    if (apiSettingInstance == null)
                    {
                        apiSettingInstance = new APISettings();
                    }
                    return apiSettingInstance;
                }
            }
        }

        private KeyValueConfigurationCollection KeyValueConfigurationCollection;
        public KeyValueConfigurationCollection Settings
        {
            get
            {
                if (KeyValueConfigurationCollection == null)
                {
                    KeyValueConfigurationCollection = new KeyValueConfigurationCollection();
                    KeyValueConfigurationCollection = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;

                }
                return KeyValueConfigurationCollection;
            }
        }
    }
}
