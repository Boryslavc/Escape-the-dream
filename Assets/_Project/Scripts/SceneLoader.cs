using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scene Loader", menuName = "Systems/Core/Scene Loading")]
public class SceneLoader : ScriptableObject
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadCurrent()
    {
        var sceneInd = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneInd);
    }

    public void LoadNext()
    {
        var currSceneInd = SceneManager.GetActiveScene().buildIndex;

        if(currSceneInd != SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currSceneInd + 1);
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
