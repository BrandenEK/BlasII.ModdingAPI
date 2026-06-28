using HarmonyLib;
using Il2CppTGK.Framework;
using Il2CppTGK.Game.Components.Attack;
using Il2CppTGK.Game.Components.Attack.Data;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.Game.Managers;
using UnityEngine;

namespace BlasII.ModdingAPI;

[HarmonyPatch(typeof(AttackReceiverComponent), nameof(AttackReceiverComponent.OnAttackReceive), typeof(AttackHit), typeof(Collision2D))]
class t
{
    public static void Postfix(AttackReceiverComponent __instance, AttackHit hit)
    {
        if (__instance.IsInvincible() || __instance.IsDodging)
            return;

        var data = hit.hitData.attackDamage;

        if (_lastReceiver == __instance && _lastAttack == data)
        {
            ModLog.Error($"x{++_count}");
            return;
        }

        ModLog.Error($"{__instance.name} received a hit: {hit?.attackID?.name} ({hit?.attackID?.GetType().Name})");

        ModLog.Warn("Physical sources");
        for (int i = 0; i < data.physicalAttackSources.Length; i++)
        {
            var attack = data.physicalAttackSources[i];
            ModLog.Info($"{attack?.attackType?.name}: {attack.baseDamage} damage");
        }

        ModLog.Warn("Elemental sources");
        for (int i = 0; i < data.elementalAttackSources.Length; i++)
        {
            var attack = data.elementalAttackSources[i];
            ModLog.Info($"{attack?.attackType?.name}: {attack.baseDamage} damage");
        }

        ModLog.Warn("Status effect sources");
        for (int i = 0; i < data.statusAttackSources.Length; i++)
        {
            var attack = data.statusAttackSources[i];
            ModLog.Info($"{attack?.attackType?.name}: {attack.baseDamage} damage");
        }

        _lastReceiver = __instance;
        _lastAttack = data;
        _count = 1;
    }

    private static AttackReceiverComponent _lastReceiver;
    private static AttackSourceData _lastAttack;
    private static int _count = 0;
}

[HarmonyPatch(typeof(Core), nameof(Core.Awake))]
class Core_Awake_Patch
{
    public static void Postfix() => Main.ModLoader.PreInitialize();
}

[HarmonyPatch(typeof(Core), nameof(Core.CreateManagers))]
class Core_CreateManagers_Patch
{
    public static void Postfix()
    {
        Main.ModLoader.Initialize();
        Main.ModLoader.PostInitialize();
    }
}

[HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.OnGlobalRoomLoaded))]
class Mod_SceneLoaded_Patch
{
    public static void Postfix(Room newRoom) => Main.ModLoader.SceneLoaded(newRoom?.Name ?? string.Empty);
}

[HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.OnGlobalBeforeChangeRoom))]
class Mod_SceneUnloaded_Patch
{
    public static void Postfix(Room oldRoom) => Main.ModLoader.SceneUnloaded(oldRoom?.Name ?? string.Empty);
}

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.OnShow))]
class Mod_LoadMenu_Patch
{
    public static void Postfix() => Main.ModLoader.SceneLoaded("MainMenu");
}

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.NewGame))]
class Mod_NewGame_Patch
{
    public static void Postfix(bool __runOriginal)
    {
        if (__runOriginal)
            Main.ModLoader.ProcessModFunction(mod => mod.OnNewGame());
    }
}

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.LoadGame))]
class Mod_LoadGame_Patch
{
    public static void Postfix(bool __runOriginal)
    {
        if (__runOriginal)
            Main.ModLoader.ProcessModFunction(mod => mod.OnLoadGame());
    }
}