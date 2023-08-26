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
            ModLoader.RegisterMod(new Randomizer());
        }

        public static void Log(object message)
        {
            MelonLogger.Msg(message);
        }

        public override void OnUpdate() => ModLoader.Update();

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MelonLogger.Warning("Scene loaded: " + sceneName);

            if (sceneName == "LandingScene" || sceneName == "MainMenu")
            {
                foreach (CanvasScaler scaler in Object.FindObjectsOfType<CanvasScaler>())
                {
                    MelonLogger.Error(scaler.name);
                }

                AddModListToTitleScreen();
                ModLoader.SceneLoaded("MainMenu");
            }
        }

        private void AddModListToTitleScreen()
        {
            // Do this better
            CanvasScaler canvas = Object.FindObjectOfType<CanvasScaler>();
            if (canvas == null)
            {
                MelonLogger.Msg("Canvas was null");
                return;
            }

            foreach (TextMeshProUGUI childText in canvas.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (childText.text.Contains("1.0.5"))
                {
                    childText.text += ModLoader.CalculateModListText();
                    childText.alignment = TextAlignmentOptions.TopRight;
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
}