using UnityEngine;

namespace Core.Characters
{
    public class SpellCaster 
    {
        private PooledSpellSettings[] spells;

        private Vector3 spellOffset = new Vector2(0.5f, 0);
        private int currentSpell;

        private Camera camera;

        public SpellCaster(Camera camera, params PooledSpellSettings[] slowingSpellSettings)
        {
            spells = slowingSpellSettings;
            this.camera = camera;

            currentSpell = 0;
        }

        public void Cast(Vector3 casterPos, Vector2 inputPoint)
        {
            var spell = ObjectPooler.Spawn(spells[currentSpell]) as SpellPooled;

            if (spell != null)
            {
                spell.transform.position = casterPos + spellOffset;
                var direction = GetDirection(casterPos, inputPoint);
                spell.SetDirection(direction);
                spell.Cast();
            }
        }

        private Vector3 GetDirection(Vector3 casterPos, Vector2 inputPoint)
        {
            Vector3 worldPosition = camera.ScreenToWorldPoint(inputPoint);
            Vector3 direction = worldPosition - casterPos;
            direction.Normalize();

            return direction;
        }

        public void ChangeSpell()
        {
            currentSpell = (currentSpell + 1) % spells.Length;
        }
    }

}