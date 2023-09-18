
namespace BlasII.ModdingAPI.Persistence
{
    public interface IPersistentMod
    {
        public SaveData SaveGame();

        public void LoadGame(SaveData data);

        public void ResetGame();
    }
}
