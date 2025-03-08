using System;

namespace BlasII.ModdingAPI.Persistence;

public interface IGlobalPersistentMod
{
    public Type GlobalDataType { get; }

    public GlobalSaveData Save();

    public void Load(GlobalSaveData data);
}

public class TestGlobalSaveData : GlobalSaveData
{
    public int Number { get; set; }
}
