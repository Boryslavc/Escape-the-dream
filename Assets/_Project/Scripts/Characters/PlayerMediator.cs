using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Core.Characters
{
    public class PlayerMediator : MonoBehaviour, IStoppable
    {
        [Header("References")]
        [SerializeField] private InputReader inputReader;
        [SerializeField] private PooledSpellSettings slowingSpell;
        [SerializeField] private PooledSpellSettings acceleratingSpell;

        [Header("Sounds")]
        [SerializeField] private SoundData castSound;
        [SerializeField] private SoundData vanishSound;

        [Header("Animation Settings")]
        [SerializeField] private string SpellCastTriggerName = "AttackTrig";
        [SerializeField] private ParticleSystem vanishFX;

        [Header("UI References")]
        [SerializeField] private Image acceleratingSpellIcon;
        [SerializeField] private Image deceleratingSpellIcon;

        private SpellCaster spellCaster;
        private Animator animator;
        private PlayerUI playerUI;

        [Inject]
        private ICharacterMover characterMover;

        public event UnityAction OnPlayerStartedMoving;
        public event UnityAction OnPlayerVanish;

        private bool isActive = true; //for spell casting
        private bool isMoving = true; //for movement

        private void Awake()
        {
            spellCaster = new SpellCaster(Camera.main, slowingSpell, acceleratingSpell);
            playerUI = new PlayerUI(deceleratingSpellIcon, acceleratingSpellIcon);

            inputReader.SpellCasted += OnCastSpell;
            inputReader.SpellChanged += OnSpellChange;

            animator = GetComponent<Animator>();
            characterMover.GetTransform(this.transform);
        }


        private void OnDestroy()
        {
            inputReader.SpellCasted -= OnCastSpell;
            inputReader.SpellChanged -= OnSpellChange;
        }
        

        private void OnCastSpell(Vector2 inputPosition)
        {
            if (isActive)
            {
                spellCaster.Cast(transform.position, inputPosition);
                animator.SetTrigger(SpellCastTriggerName);
                AudioPlayer.Instance.Play(castSound);
            }
        }

        private void OnSpellChange()
        {
            if (isActive)
            {
                spellCaster.ChangeSpell();
                playerUI.EmphasizeNextIcon();
            }
        }

        private void Update()
        {
            if(isMoving) 
                characterMover.Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out MovingObject movingObject) && isActive)
                isMoving = false;

            if (collision.gameObject.TryGetComponent(out Finish finish))
                Vanish();
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<MonsterController>(out MonsterController monsterController))
                Vanish();
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out MovingObject movingObject) && isActive)
                isMoving = true;
        }

        private void Vanish()
        {
            if (isActive)
            {
                isActive = false;
                isMoving = false;
                GetComponent<SpriteRenderer>().enabled = false;
                vanishFX.Play();
                AudioPlayer.Instance.Play(vanishSound);
                OnPlayerVanish?.Invoke();
            }
        }

        public void Stop()
        {
            StartCoroutine(ChangeSpeed(0, 0, 0.7f));
        }

        public void Resume()
        {
            StartCoroutine(ChangeSpeed(1, 1, 0.7f));
        }
        private IEnumerator ChangeSpeed(float targetAnimatorSpeed, float targetCharacterMoverSpeed, float duration)
        {
            float initialAnimatorSpeed = animator.speed;
            float initialCharacterMoverSpeed = characterMover.GetSpeed();

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                animator.speed = Mathf.Lerp(initialAnimatorSpeed, targetAnimatorSpeed, normalizedTime);
                characterMover.SetSpeed(Mathf.Lerp(initialCharacterMoverSpeed, targetCharacterMoverSpeed, normalizedTime));
                yield return null;
            }
            animator.speed = targetAnimatorSpeed;
            characterMover.SetSpeed(targetCharacterMoverSpeed);
        }
    }
}