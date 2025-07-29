using System.Collections.Generic;
using UnityEngine;

namespace Utility.Unity.Common.ObjectPool
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance { get; private set; }

        private readonly Dictionary<string, ObjectPool> pools = new();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void CreatePool(string key, GameObject prefab, int initialSize = 10, Transform parent = null)
        {
            if (!pools.ContainsKey(key))
                pools[key] = new ObjectPool(prefab, initialSize, parent);
        }

        public GameObject Spawn(string key)
        {
            if (pools.TryGetValue(key, out var pool))
                return pool.Get();

            Debug.LogWarning($"PoolManager : No pool found for key : {key}");
            return null;
        }

        public void Release(string key, GameObject obj)
        {
            if (pools.TryGetValue(key, out var pool))
                pool.Release(obj);
            else
                Debug.LogWarning($"PoolManager : No pool found for key : {key}");
        }
    }
}
