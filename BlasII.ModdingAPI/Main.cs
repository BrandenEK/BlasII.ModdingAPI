using Il2CppTGK.Game;
using Il2CppTGK.Game.Managers;
using HarmonyLib;
//using Il2CppSystem;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Il2CppTMPro;
using MelonLoader;

namespace BlasII.ModdingAPI
{
    public class Main : MelonLoader.MelonMod
    {
        private static Main instance;

        public static TestMod TestMod { get; private set; }

        public override void OnInitializeMelon()
        {
            MelonLoader.MelonLogger.Warning("Loading mod");
            instance ??= this;

            //RoomManager roomManager = CoreCache.Room;
            //if (roomManager != null)
            //{
            //    //RoomLoaded del = OnRoomLoaded;
            //    //System.Delegate.CreateDelegate(typeof(RoomManager.RoomDelegate2), OnRoomLoaded);
            //    //RoomManager.RoomDelegate2 del = new RoomManager.RoomDelegate2(OnRoomLoaded);

            //    MethodInfo method = AccessTools.DeclaredMethod(typeof(ModdingAPI), "OnRoomLoaded");
            //    RoomManager.RoomDelegate2 roomDelegate = Delegate.CreateDelegate(typeof(ModdingAPI), method);

            //    roomManager.OnGlobalRoomLoaded.Add(del);
            //}

            RoomManager room = CoreCache.Room;
            MelonLogger.Msg(room?.Name ?? "Null room");

            TestMod = new TestMod();
        }

        public static void Log(object message)
        {
            MelonLoader.MelonLogger.Msg(message);
        }

        public override void OnUpdate()
        {
            TestMod.OnUpdate();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MelonLoader.MelonLogger.Warning("Scene loaded: " + sceneName);

            if (sceneName != "MainMenu")
                return;

            foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                Log(obj.name);
                DisplayChildren(obj.transform, 1);
            }

            foreach (CanvasScaler canvas in Object.FindObjectsOfType<CanvasScaler>())
            {
                Log("Canvas: " + canvas.name);
                foreach (Component c in canvas.gameObject.GetComponents<Component>())
                {
                    Log(c.ToString());
                }

                Log("Children: " + canvas.transform.childCount);
                //TextMeshProUGUI text = canvas.gameObject.GetComponentInChildren<TextMeshProUGUI>();
                //Log(text?.name ?? "No tmpro");

                foreach (TextMeshProUGUI childText in canvas.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    Log(childText.name + ": " + childText.text);
                    if (childText.text.Contains("1.0.5"))
                    {
                        childText.text += "\n\nModding API v0.1.0";
                    }
                }
            }
        }

        private void DisplayChildren(Transform parent, int level)
        {
            for (int c = 0; c < parent.childCount; c++)
            {
                Transform child = parent.GetChild(c);
                string line = string.Empty;
                for (int i = 0; i < level; i++)
                    line += "\t";
                Log(line + child.name);
                DisplayChildren(child, level + 1);
            }
        }

        void OnRoomLoaded(Room oldRoom, Room newRoom)
        {

        }

        delegate void RoomLoaded (Room o, Room n);
    }

    [HarmonyPatch(typeof(RoomManager), nameof(RoomManager.SendActivationEvents))]
    class test
    {
        public static void Postfix()
        {
            Main.Log("Sending activation");
        }
    }

    [HarmonyPatch(typeof(AchievementsManager), nameof(AchievementsManager.OnInitialize))]
    class init
    {
        public static void Postfix()
        {
            MelonLogger.Error("Init");
            Main.TestMod.OnInitialize();

            RoomManager room = CoreCache.Room;
            MelonLogger.Msg(room?.Name ?? "Null room");
        }
    }

    [HarmonyPatch(typeof(AchievementsManager), nameof(AchievementsManager.OnAllInitialized))]
    class allinit
    {
        public static void Postfix()
        {
            MelonLogger.Error("All Init");
            Main.TestMod.OnAllInitialized();

            RoomManager room = CoreCache.Room;
            MelonLogger.Msg(room?.Name ?? "Null room");
        }
    }
}