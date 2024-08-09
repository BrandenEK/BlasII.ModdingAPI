using Il2CppTGK.Game;

namespace BlasII.ModdingAPI.Helpers;

/// <summary>
/// Helpful methods for playing audio effects
/// </summary>
public static class AudioHelper
{
    /// <summary>
    /// Plays a sound effect from the given ID
    /// </summary>
    public static void PlayEffect(string effect)
    {
        CoreCache.AudioManager.InstantiateEvent(effect);
        CoreCache.AudioManager.PlayOneShot(effect);
    }

    /// <summary>
    /// Plays a sound effect from the UI module
    /// </summary>
    public static void PlayEffectUI(SfxUI effect)
    {
        PlayEffect("event:/SFX/UI/" + effect switch
        {
            SfxUI.EquipItem => "Equip Item",
            SfxUI.ChangeTab => "Change Tab",
            SfxUI.ChangeSelection => "Change Selection",
            SfxUI.OpenMenu => "OpenMenu",
            SfxUI.CloseMenu => "CloseMenu",
            SfxUI.RelicCollected => "Relic Collected",
            _ => throw new System.ArgumentException("Invalid sfx", nameof(effect))
        });
    }

    /// <summary>
    /// A sound effect from the UI module
    /// </summary>
    public enum SfxUI
    {
        /// <summary> event:/SFX/UI/Equip Item </summary>
        EquipItem = 0,
        /// <summary> event:/SFX/UI/Change Tab </summary>
        ChangeTab = 1,
        /// <summary> event:/SFX/UI/Change Selection </summary>
        ChangeSelection = 2,
        /// <summary> event:/SFX/UI/OpenMenu </summary>
        OpenMenu = 3,
        /// <summary> event:/SFX/UI/CloseMenu </summary>
        CloseMenu = 4,
        /// <summary> event:/SFX/UI/Relic Collected </summary>
        RelicCollected = 5,
    }
}
