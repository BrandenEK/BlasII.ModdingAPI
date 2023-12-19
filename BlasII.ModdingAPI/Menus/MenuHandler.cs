
namespace BlasII.ModdingAPI.Menus
{
    public class MenuHandler
    {
        private readonly BlasIIMod _mod;

        internal MenuHandler(BlasIIMod mod) => _mod = mod;

        public void RegisterNewGameMenu(BaseMenu menu)
        {
            Main.ModdingAPI.Log($"Registering NewGame menu: " + menu.GetType().Name);
            menu.OwnerMod = _mod;
            MenuModder.AddNewGameMenu(menu);
        }

        public void RegisterLoadGameMenu(BaseMenu menu)
        {
            Main.ModdingAPI.Log($"Registering LoadGame menu: " + menu.GetType().Name);
            menu.OwnerMod = _mod;
            MenuModder.AddLoadGameMenu(menu);
        }
    }
}
