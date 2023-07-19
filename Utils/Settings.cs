using BlueprintCore.Utils;
using Kingmaker.Localization;
using ModMenu.Settings;
using Menu = ModMenu.ModMenu;

namespace IsekaidClass.Utils
{
    internal class Settings
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(Settings));

        private static readonly string RootKey = "isekaid-class.settings";
        private static readonly string SettingsTitle = "IsekaidClass.Settings.Title";
        private static readonly string SettingsEnableFeature = "IsekaidClass.Settings.EnableFeature";

        private static readonly string EnableMainClass = "IsekaidClass.Settings.MainClass";
        private static readonly string ClassFeaturesHeader = "IsekaidClass.Settings.ClassFeatures.Header";

        internal static bool IsEnabled(string key)
        {
            return Menu.GetSettingValue<bool>(GetKey(key));
        }

        internal static void Init()
        {
            logger.Info("Initializing settings");

            var settings = SettingsBuilder.New(RootKey, GetString(SettingsTitle))
                .AddDefaultButton(OnDefaultsApplied);

            // Main class
            settings.AddToggle(
                Toggle.New(
                    key: GetKey(Guids.IsekaidClass),
                    defaultValue: true,
                    description: GetString(EnableMainClass)
                )
                .WithLongDescription(GetString(SettingsEnableFeature))
            );

            // Class Features
            settings.AddSubHeader(
                title: GetString(ClassFeaturesHeader),
                startExpanded: false
            );

            foreach (var (guid, name) in Guids.ClassFeatures)
            {
                logger.Info($"  Creating setting for {name}");

                settings.AddToggle(
                    Toggle.New(
                        key: GetKey(guid),
                        defaultValue: true,
                        description: GetString(name)
                    )
                    .WithLongDescription(GetString(SettingsEnableFeature))
                );
            }

            Menu.AddSettings(settings);
        }

        private static void OnDefaultsApplied()
        {
            logger.Info("Default settings restored");
        }

        private static LocalizedString GetString(string key)
        {
            return LocalizationTool.GetString(key);
        }

        private static string GetKey(string partialKey)
        {
            return $"{RootKey}.{partialKey}";
        }
    }
}
