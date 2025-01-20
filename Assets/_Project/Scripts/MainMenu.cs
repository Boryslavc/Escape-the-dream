using Core.Persistence;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private SaveLoadSystem saveLoadSystem;
    [SerializeField] private SceneLoader sceneLoader;


    private void Awake()
    {
        if (saveLoadSystem == null)
            Debug.LogError($"Missing save load system reference on object {gameObject.name}");
        
        if(sceneLoader == null)
            Debug.LogError($"Missing scene loader reference on object {gameObject.name}");


        newGameButton.onClick.AddListener(StartNewGame);
        continueButton.onClick.AddListener(Continue);
        quitButton.onClick.AddListener(Quit);
    }

    private void StartNewGame()
    {
        saveLoadSystem.NewGame();
        sceneLoader.LoadScene(1);
    }

    private void Continue()
    {
        sceneLoader.LoadScene(saveLoadSystem.GetLevelsCompleted());
    }

    private void Quit()
    {
        Application.Quit();
    }
}
