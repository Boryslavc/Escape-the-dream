using UnityEngine;


namespace Core
{
    [CreateAssetMenu(fileName = "Trail Settings", menuName = "Object Pooling/Settings/Trail")]
    public class PooledSpellSettings : PooledObjectSettings
    {
        public float SpeedAffectMultiplicator;
        public float Speed;

        public override PoolObject Create()
        {
            var go = Instantiate(Prefab);
            go.name = Identifier;

            var pooled = go.AddComponent<SpellPooled>();
            pooled.Settings = this;

            pooled.gameObject.SetActive(false);

            return pooled;
        }

        public override void OnGet(PoolObject p)
        {
            //do nothing, set active is called inside the spell Pooled class
        }
    }
}
