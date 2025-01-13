using Core.Characters;
using UnityEngine;


namespace Core
{
    public class SpellPooled : PoolObject
    {
        new PooledSpellSettings Settings => (PooledSpellSettings)base.Settings;

        private Vector2 moveDirection;

        private bool isMoving = false;

        public void SetDirection(Vector2 direciton) => moveDirection = direciton;

        //manually enable and disable trails
        //to prevent trails from making noise traces while being pooled by object pooler
        private TrailRenderer[] trails;

        private void Awake()
        {
            trails = GetComponentsInChildren<TrailRenderer>();

            foreach (TrailRenderer trail in trails) 
                trail.enabled = false;
        }

        public void Cast()
        {
            gameObject.SetActive(true);
            
            foreach (TrailRenderer trail in trails)
                trail.enabled = true;

            isMoving = true;
        }

        private void Update()
        {
            if (isMoving)
                transform.Translate(moveDirection * Time.deltaTime * Settings.Speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerMediator>(out PlayerMediator playerController))
                return;
            
            //its faster to search through the 5-10 components on the object,
            //then to create a centralized access point with the collection of all moving object (>10)
            //and change speed there
            if (collision.TryGetComponent<ISpellSusceptible>(out var spellSusceptible))
                spellSusceptible.React(this.Settings);

            ObjectPooler.ReturnToPool(this);
        }
    }
}