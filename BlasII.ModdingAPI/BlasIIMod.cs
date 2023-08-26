using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;

namespace BlasII.ModdingAPI
{
    //internal class BlasIIMod : MelonMod
    //{
    //    public override sealed void OnSceneWasLoaded(int buildIndex, string sceneName)
    //    {
    //        base.OnSceneWasLoaded(buildIndex, sceneName);
    //    }
    //}

    //internal class TestMod : BlasIIMod
    //{
    //    public override void OnUpdate()
    //    {
    //        base.OnUpdate();
    //        MelonLogger.Msg("Version: " + this.Info.Version);
    //    }
    //}

    public class BlasIIMod
    {
        internal string Id { get; private set; }

        internal string Name { get; private set; }

        internal string Version { get; private set; }

        public BlasIIMod(string id, string name, string version)
        {
            Id = id;
            Name = name;
            Version = version;
        }

        protected internal virtual void OnInitialize() { }

        protected internal virtual void OnAllInitialized() { }

        protected internal virtual void OnDispose() { }

        protected internal virtual void OnUpdate() { }

        protected internal virtual void OnSceneLoaded(string sceneName) { }

        protected internal virtual void OnSceneUnloaded(string sceneName) { }
    }

    public class TestMod : BlasIIMod
    {
        public TestMod() : base("BlasII.ModdingAPI", "Modding API", "0.1.0") { }

        protected internal override void OnInitialize()
        {
            MelonLogger.Warning("Test mod initialize");
        }

        protected internal override void OnAllInitialized()
        {
            MelonLogger.Warning("Test mod all initialized");
        }

        protected internal override void OnDispose()
        {
            MelonLogger.Warning("Test mod dispose");
        }

        protected internal override void OnUpdate()
        {
            if (Time.frameCount % 120 == 0)
            {
                MelonLogger.Error("Test mod update");
            }
        }

        protected internal override void OnSceneLoaded(string sceneName)
        {
            MelonLogger.Warning("Test mod scene loaded: " + sceneName);
        }

        protected internal override void OnSceneUnloaded(string sceneName)
        {
            MelonLogger.Warning("Test mod scene unloaded: " + sceneName);
        }
    }
}
