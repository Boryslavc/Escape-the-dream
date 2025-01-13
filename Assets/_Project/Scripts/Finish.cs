using Core.Characters;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Collider2D))]
public class Finish : MonoBehaviour
{
    [SerializeField] private SoundData winSound;
    [SerializeField] private Image congratulationImage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMediator player))
            OnPlayerWin();
    }

    private void OnPlayerWin()
    {
        AudioPlayer.Instance.Play(winSound);
        congratulationImage.gameObject.SetActive(true);
    }
}