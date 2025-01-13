using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


namespace Core
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] private int maxPoolSize = 100;
        [SerializeField] private int defaultPoolSize = 20;

        public static ObjectPooler Instance;
        private static Dictionary<string, IObjectPool<PoolObject>> pools
            = new Dictionary<string, IObjectPool<PoolObject>>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public static PoolObject Spawn(PooledObjectSettings settings)
                => Instance.GetPoolOf(settings)?.Get();

        public static void ReturnToPool(PoolObject pooled) 
                => Instance.GetPoolOf(pooled.Settings)?.Release(pooled);


        IObjectPool<PoolObject> GetPoolOf(PooledObjectSettings poolSettings)
        {
            IObjectPool<PoolObject> res;

            if(pools.TryGetValue(poolSettings.Identifier, out res)) return res;

            res = new ObjectPool<PoolObject>(
                poolSettings.Create,
                poolSettings.OnGet,
                poolSettings.OnRelease,
                poolSettings.OnDestroyPoolObject,
                true,
                defaultPoolSize,
                maxPoolSize);

            pools.Add(poolSettings.Identifier, res);
            return res;
        }
    }
}
