using UnityEngine;


namespace ObjectPooling
{
    public abstract class PooledObjectSettings : ScriptableObject
    {
        public GameObject Prefab;
        public string Identifier;

        public abstract PoolObject Create();

        public virtual void OnGet(PoolObject p) => p.gameObject.SetActive(true);
        public virtual void OnRelease(PoolObject p) => p.gameObject.SetActive(false);
        public virtual void OnDestroyPoolObject(PoolObject p) => Destroy(p.gameObject);
    }
}
