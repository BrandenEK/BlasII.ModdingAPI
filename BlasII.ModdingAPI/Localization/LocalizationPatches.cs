using BlasII.ModdingAPI.Assets;
using HarmonyLib;
using Il2CppI2.Loc;

namespace BlasII.ModdingAPI.Localization
{
    /// <summary>
    /// Whenever the selected language is changed, update all items and handlers
    /// </summary>
    [HarmonyPatch(typeof(LocalizationManager), nameof(LocalizationManager.SetLanguageAndCode))]
    class Language_Change_Patch
    {
        public static void Postfix(string LanguageCode)
        {
            ModLog.Info($"Changing language to [{LanguageCode}]");
            AssetStorage.LocalizeAllItems();

            Main.ModLoader.ProcessModFunction(mod => mod.LocalizationHandler.OnLangaugeChanged());
        }
    }
}
