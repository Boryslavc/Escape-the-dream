using System.IO;
using UnityEngine;

namespace Core.Persistence
{
    public class FileService : IDataService
    {
        private ISerializer serializer;
        private readonly string path = Application.persistentDataPath;
        private readonly string fileExtension = ".json";
        
        public FileService(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        private string GetPathToFile(string fileName)
        {
            return Path.Combine(path, string.Concat(fileName, fileExtension));
        }

        public void Save(GameData data)
        {
            string path = GetPathToFile(data.Name);

            string format = serializer.Serialize(data);
            File.WriteAllText(path, format);
        }

        public GameData Load(string fileName)
        {
            string path = GetPathToFile(fileName);

            if (File.Exists(path))
            {
                string format = File.ReadAllText(path);
                return serializer.Deserialize<GameData>(format);
            }
            else 
                throw new IOException($"No file found with name {fileName}");
        }

        public void Delete(string name)
        {
            string path = GetPathToFile(name);

            if (File.Exists(path))
                File.Delete(path);
        }

        public bool SaveExists(string name)
        {
            string path = GetPathToFile(name);

            if (File.Exists(path))
                return true;

            return false;
        }
    }
}