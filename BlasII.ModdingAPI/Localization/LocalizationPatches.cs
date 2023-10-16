using BlasII.ModdingAPI.Storage;
using HarmonyLib;
using Il2CppI2.Loc;

namespace BlasII.ModdingAPI.Localization
{
    [HarmonyPatch(typeof(LocalizationManager), nameof(LocalizationManager.SetLanguageAndCode))]
    class Language_Change_Patch
    {
        public static void Postfix(string LanguageCode)
        {
            Main.ModdingAPI.Log($"Changing language to [{LanguageCode}]");
            ItemStorage.LocalizeAllItems();
        }
    }
}
