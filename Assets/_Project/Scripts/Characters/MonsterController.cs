using System.Collections;
using Core;
using Core.Characters;
using UnityEngine;
using Zenject;

[RequireComponent (typeof(Collider2D))]
public class MonsterController : MonoBehaviour, IStoppable
{
    [SerializeField] private PlayerMediator player;

    [Header("Animation Settings")]
    [SerializeField] private string attackTriggerName = "AttackTrig";

    [Header("Sounds")]
    [SerializeField] private SoundData attackSound;
    [SerializeField] private SoundData chasingSound;

    private bool isMoving = true;

    private Animator animator;
    [Inject] private ICharacterMover characterMover;
    private float initSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterMover.GetTransform(this.transform);
        initSpeed = characterMover.GetSpeed();

        player.OnPlayerVanish += Stop;
    }

    private void OnDestroy()
    {
        player.OnPlayerVanish -= Stop;
    }

    private void Start()
    {
        AudioPlayer.Instance.Play(chasingSound);
    }

    private void Update()
    {
        if(isMoving) 
            characterMover.Move();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ReferenceEquals(collision.gameObject, player))
            Attack();
    }
    private void Attack()
    {
        animator.SetTrigger(attackTriggerName);
        AudioPlayer.Instance.Play(attackSound);

        Stop();
    }

    public void Stop()
    {
        StartCoroutine(ChangeSpeed(0, 0.7f));
    }

    public void Resume()
    {
        StartCoroutine(ChangeSpeed(1,0.7f));
    }
    private IEnumerator ChangeSpeed(float targetCharacterMoverSpeed, float duration)
    {
        float initialCharacterMoverSpeed = characterMover.GetSpeed();

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            characterMover.SetSpeed(Mathf.Lerp(initialCharacterMoverSpeed, targetCharacterMoverSpeed, normalizedTime));
            yield return null;
        }
        characterMover.SetSpeed(targetCharacterMoverSpeed);
    }
}
