using Core.Characters;
using UnityEngine;
using UnityEngine.UI;


public class SceneFlowController : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;

    private PlayerMediator playerMediator;
    private void Awake()
    {
        playerMediator = FindAnyObjectByType<PlayerMediator>();
        playerMediator.OnPlayerVanish += GameEnd;
    }
    private void OnDestroy()
    {
        playerMediator.OnPlayerVanish -= GameEnd;
    }

    private void GameEnd()
    {
        retryButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
}
