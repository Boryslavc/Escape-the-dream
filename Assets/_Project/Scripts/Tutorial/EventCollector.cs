using UnityEngine;
using UnityEngine.Events;


public class EventCollector : MonoBehaviour
{
    public event UnityAction OnPromptTriggered;

    private CheckPointPrompt[] checkPoints;

    private void Awake()
    {
        checkPoints = FindObjectsByType<CheckPointPrompt>(FindObjectsSortMode.None);

        foreach (var checkPoint in checkPoints) 
            checkPoint.OnCheckPointReached += () => OnPromptTriggered?.Invoke();
    }

    private void OnDisable()
    {
        foreach (var checkPoint in checkPoints)
            checkPoint.OnCheckPointReached -= () => OnPromptTriggered?.Invoke();
    }
}
