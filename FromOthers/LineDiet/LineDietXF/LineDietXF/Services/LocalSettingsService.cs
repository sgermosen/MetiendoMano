using System;
using LineDietXF.Enumerations;
using LineDietXF.Interfaces;
using Plugin.Settings;

// NOTE:: Uses Xam.Plugins.Settings (ref: https://github.com/jamesmontemagno/SettingsPlugin)
namespace LineDietXF.Services
{
    /// <summary>
    /// Used for storing app settings locally
    /// </summary>
    public class LocalSettingsService : ISettingsService
    {
        public bool HasDismissedStartupView
        {
            get
            {
                return CrossSettings.Current.GetValueOrDefault<bool>(Constants.App.Setting_DismissedStartupView_Key,
                                                                     Constants.App.Setting_DismissedStartupView_Default);
            }
            set
            {
                CrossSettings.Current.AddOrUpdateValue<bool>(Constants.App.Setting_DismissedStartupView_Key, value);
            }
        }

        public WeightUnitEnum WeightUnit
        { 
            get
            {
                return CrossSettings.Current.GetValueOrDefault<WeightUnitEnum>(Constants.App.Setting_WeightUnits_Key,
                    Constants.App.Setting_WeightUnits_Default);
            }
            set
            {
                CrossSettings.Current.AddOrUpdateValue<WeightUnitEnum>(Constants.App.Setting_WeightUnits_Key, value);
            }
        }

        public void Initialize()
        {
        }
    }
}