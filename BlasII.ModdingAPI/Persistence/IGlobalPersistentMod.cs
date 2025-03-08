
using System;

namespace BlasII.ModdingAPI.Persistence;

//public interface IGlobalPersistentMod<T> where T : GlobalSaveData
//{
//    public T SaveGlobal();

//    public void LoadGlobal(T data);
//}

//public interface IGlobalPersistentMod
//{
//    public GlobalSaveData Save();

//    public void Load(GlobalSaveData data);
//}


//public interface IGlobalPersistentMod<T> : IGlobalPersistentMod where T : GlobalSaveData
//{
//    public T Save();

//    public void Load(T data);
//}




//public interface IGlobalPersistentMod
//{
//    public GlobalSaveData Save();

//    public void Load(GlobalSaveData data);
//}


public interface IGlobalPersistentMod
{
    public Type GlobalDataType { get; }

    public GlobalSaveData Save();

    public void Load(GlobalSaveData data);
}

public class TestMod : IGlobalPersistentMod
{
    public Type GlobalDataType { get; } = typeof(TestGlobalSaveData);

    public void Load(GlobalSaveData data)
    {
        throw new NotImplementedException();
    }

    public GlobalSaveData Save()
    {
        throw new NotImplementedException();
    }
}

public class TestGlobalSaveData : GlobalSaveData
{
    public int Number { get; set; }
}
