
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


public interface IGlobalPersistentMod<T> where T : GlobalSaveData
{
    public T Save();

    public void Load(T data);
}

public class TestMod : IGlobalPersistentMod<TestGlobalSaveData>
{
    public void Load(TestGlobalSaveData data)
    {
        throw new System.NotImplementedException();
    }

    public TestGlobalSaveData Save()
    {
        throw new System.NotImplementedException();
    }
}

public class TestGlobalSaveData : GlobalSaveData
{
    public int Number { get; set; }
}
