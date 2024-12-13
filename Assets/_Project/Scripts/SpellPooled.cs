using UnityEngine;


namespace ObjectPooling
{
    public class SpellPooled : PoolObject
    {
        new SpellPoolSettings Settings => (SpellPoolSettings) base.Settings;

        private Vector2 moveDirection;

        private bool isMoving = false;

        public void SetDirection(Vector2 direciton) => moveDirection = direciton;


        public void Cast()
        {
            gameObject.SetActive(true);

            isMoving = true;
        }

        private void Update()
        {
            if(isMoving) 
                transform.Translate(moveDirection * Time.deltaTime * Settings.Speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //its faster to search through the 5-10 components on the object,
            //then to create a centralized access point with the collection of all moving object (>10)
            //and change speed there
            if (collision.TryGetComponent<ISpellSusceptible>(out var spellSusceptible))
                spellSusceptible.React(this.Settings);

            ObjectPooler.ReturnToPool(this);
        }
    }
}
