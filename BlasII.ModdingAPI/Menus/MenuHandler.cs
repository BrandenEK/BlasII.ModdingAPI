
namespace BlasII.ModdingAPI.Menus
{
    public class MenuHandler
    {
        private readonly BlasIIMod _mod;

        internal MenuHandler(BlasIIMod mod) => _mod = mod;

        public void RegisterNewGameMenu(BaseMenu menu)
        {
            menu.OwnerMod = _mod;
            MenuModder.AddNewGameMenu(menu);
        }

        public void RegisterLoadGameMenu(BaseMenu menu)
        {
            menu.OwnerMod = _mod;
            MenuModder.AddLoadGameMenu(menu);
        }
    }
}
