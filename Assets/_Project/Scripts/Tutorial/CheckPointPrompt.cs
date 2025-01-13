using Core.Characters;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider2D))]
public class CheckPointPrompt : MonoBehaviour
{
    public event UnityAction OnCheckPointReached;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMediator>(out PlayerMediator mediator))
            OnCheckPointReached?.Invoke();    
    }
}
