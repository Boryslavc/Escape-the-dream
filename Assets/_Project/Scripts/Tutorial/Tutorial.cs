using System.Collections.Generic;
using Core;
using Core.Characters;
using UnityEngine;


public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<Prompt> promts;

    private List<IStoppable> stoppables = new List<IStoppable>(2);
    private EventCollector collector;


    private int index = 0;

    private void Awake()
    {
        collector = gameObject.AddComponent<EventCollector>();
        collector.OnPromptTriggered += ShowPromt;

        var movingObjects = FindObjectsByType<MovingObject>(FindObjectsSortMode.None);

        foreach (var movingObject in movingObjects)
            stoppables.Add(movingObject);

        var player = FindFirstObjectByType<PlayerMediator>();
        stoppables.Add(player);

        var monsterController = FindFirstObjectByType<MonsterController>();
        stoppables.Add(monsterController);
    }

    private void ShowPromt()
    {
        foreach(var stoppable in stoppables)
            stoppable.Stop();

        var current = promts[index];
        current.ActivateSelf();

        var duration = current.GetLifeTime();

        Invoke(nameof(HidePromt), duration);
    }

    private void HidePromt()
    {
        promts[index].DeactivateSelf();

        foreach(var stoppable in stoppables)
            stoppable.Resume();

        index++;
    }
}
