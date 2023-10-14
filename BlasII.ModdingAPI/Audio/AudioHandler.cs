using Il2CppTGK.Game;

namespace BlasII.ModdingAPI.Audio
{
    public class AudioHandler
    {
        public void PlayUI(UISFX effect)
        {
            string id = "event:/SFX/UI/" + _uiEvents[(int)effect];
            CoreCache.AudioManager.InstantiateEvent(id);
            CoreCache.AudioManager.PlayOneShot(id);
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
