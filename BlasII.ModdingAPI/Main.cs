using MelonLoader;
using System;
using System.IO;
using System.Reflection;

namespace BlasII.ModdingAPI;

internal class Main : MelonMod
{
    public static ModLoader ModLoader { get; private set; }
    public static ModdingAPI ModdingAPI { get; private set; }

    public override void OnInitializeMelon()
    {
        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LoadMissingAssemblies);

        ModLoader = new ModLoader();
        ModdingAPI = new ModdingAPI();
    }

    public override void OnUpdate() => ModLoader.Update();

    public override void OnLateUpdate() => ModLoader.LateUpdate();

    public override void OnSceneWasLoaded(int _, string sceneName) => ModLoader.UnitySceneLoaded(sceneName);

    private Assembly LoadMissingAssemblies(object send, ResolveEventArgs args)
    {
        string assemblyPath = Path.GetFullPath($"Modding/data/{args.Name[..args.Name.IndexOf(",")]}.dll");

        if (File.Exists(assemblyPath))
        {
            ModLog.Log("Successfully loaded missing assembly: " + args.Name, ModInfo.MOD_NAME, ModLog.LogLevel.Warning);
            return Assembly.LoadFrom(assemblyPath);
        }
        else
        {
            ModLog.Log("Failed to load missing assembly: " + args.Name, ModInfo.MOD_NAME, ModLog.LogLevel.Warning);
            return null;
        }
    }
}