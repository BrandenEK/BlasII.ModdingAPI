using HarmonyLib;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Managers;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BlasII.ModdingAPI
{
    internal class Main : MelonMod
    {
        private static Main instance;

        public static ModLoader ModLoader { get; private set; }

        public override void OnInitializeMelon()
        {
            ModLoader = new ModLoader();
            instance ??= this;

            ModLoader.RegisterMod(new TestMod());
        }

        public static void Log(object message)
        {
            MelonLogger.Msg(message);
        }

        public override void OnUpdate() => ModLoader.Update();

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MelonLogger.Warning("Scene loaded: " + sceneName);

            if (sceneName != "MainMenu")
                return;

            // Do this better
            CanvasScaler canvas = Object.FindObjectOfType<CanvasScaler>();
            foreach (TextMeshProUGUI childText in canvas.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (childText.text.Contains("1.0.5"))
                {
                    childText.text += ModLoader.CalculateModListText();
                    foreach (Component c in childText.gameObject.GetComponents<Component>())
                    {
                        MelonLogger.Warning("Version compoentn: " + c.ToString());
                    }
                }
            }
        }


        //private void DisplayChildren(Transform parent, int level)
        //{
        //    for (int c = 0; c < parent.childCount; c++)
        //    {
        //        Transform child = parent.GetChild(c);
        //        string line = string.Empty;
        //        for (int i = 0; i < level; i++)
        //            line += "\t";
        //        Log(line + child.name);
        //        DisplayChildren(child, level + 1);
        //    }
        //}
    }

    [HarmonyPatch(typeof(RoomManager), nameof(RoomManager.SendActivationEvents))]
    class test
    {
        public static void Postfix(RoomManager __instance)
        {
            Main.Log("Sending activation");
            Room room = __instance.CurrentRoom;
            Main.ModLoader.SceneLoaded(room?.Name ?? "Empty room");
        }
    }
}