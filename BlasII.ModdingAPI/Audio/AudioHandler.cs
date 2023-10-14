using Il2CppTGK.Game;

namespace BlasII.ModdingAPI.Audio
{
    public class AudioHandler
    {
        public void PlayEffectUI(UISFX effect)
        {
            PlayEffect("event:/SFX/UI/" + _uiEvents[(int)effect]);
        }

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
