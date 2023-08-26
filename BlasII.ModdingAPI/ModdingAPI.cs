using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlasII.ModdingAPI
{
    internal class ModdingAPI : BlasIIMod
    {
        public ModdingAPI() : base("BlasII.ModdingAPI", "Modding API", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()) { }

        protected internal override void OnInitialize()
        {
            LogWarning("Initialize");
        }

        protected internal override void OnAllInitialized()
        {
            LogWarning("All initialized");
        }

        protected internal override void OnDispose()
        {
            LogWarning("Dispose");
        }

        protected internal override void OnUpdate()
        {
            //if (Time.frameCount % 120 == 0)
            //{
            //    MelonLogger.Error("Test mod update");
            //}

            //if (Input.GetKeyDown(KeyCode.Backslash))
            //{
            //    if (Main.cm == null)
            //    {
            //        MelonLogger.Error("No cm");
            //    }
            //    else
            //    {
            //        Main.cm.Toggle();
            //    }
            //    //for (int i = 0; i < 100; i++)
            //    //{
            //    //    GameManager gm = Singleton<Core>.Instance.GetManager(i);
            //    //    MelonLogger.Warning(i + ": " + gm?.Name ?? "none");
            //    //}
            //}

        }

        protected internal override void OnSceneLoaded(string sceneName)
        {
            LogError("Scene loaded: " + sceneName);
        }

        protected internal override void OnSceneUnloaded(string sceneName)
        {
            LogError("Scene unloaded: " + sceneName);
        }
    }
}
