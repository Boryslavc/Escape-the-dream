namespace Core.Persistence
{
    public interface IDataService
    {
        bool SaveExists(string name);
        void Save(GameData data);
        GameData Load(string json);
        void Delete(string name);
    }
}