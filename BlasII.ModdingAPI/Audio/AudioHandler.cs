using Il2CppTGK.Game;

namespace BlasII.ModdingAPI.Audio
{
    /// <summary>
    /// Provides access to playing different sound effects
    /// </summary>
    public class AudioHandler
    {
        /// <summary>
        /// Plays a sound effect from the UI module
        /// </summary>
        public void PlayEffectUI(UISFX effect)
        {
            PlayEffect("event:/SFX/UI/" + _uiEvents[(int)effect]);
        }

        /// <summary>
        /// Plays a sound effect from the given ID
        /// </summary>
        public void PlayEffect(string effect)
        {
            CoreCache.AudioManager.InstantiateEvent(effect);
            CoreCache.AudioManager.PlayOneShot(effect);
        }

        private static readonly string[] _uiEvents = new string[]
        {
            "Equip Item",
            "Change Tab",
            "Change Selection",
            "OpenMenu",
            "CloseMenu",
            "Relic Collected",
        };
    }
}
