using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Persistence
{
    public class SaveLoadSystem : MonoBehaviour
    {
        public static SaveLoadSystem Instance;

        [Inject]
        private IDataService dataService;

        [SerializeField]
        private GameData gameData;
        private string defaultSaveName = "Save";


        public int GetLevelsCompleted()
        {
            return gameData.LevelsFinished;
        }

        private void Awake()
        {
            ResolveSingleton();
        }
        private void ResolveSingleton()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            if (dataService.SaveExists(defaultSaveName))
                LoadGame();
            else
                NewGame();
        }

        // Can externally create new save, but cannot externally modify it
        public void NewGame()
        {
            gameData = new GameData() { Name = defaultSaveName, LevelsFinished = 0};
        }

        private void SaveGame()
        {
            gameData.LevelsFinished = SceneManager.GetActiveScene().buildIndex;
            dataService.Save(gameData);
        }

        private void LoadGame()
        {
            gameData = dataService.Load(defaultSaveName);
        }

        private void OnDestroy()
        {
            SaveGame();
        }
    }
}