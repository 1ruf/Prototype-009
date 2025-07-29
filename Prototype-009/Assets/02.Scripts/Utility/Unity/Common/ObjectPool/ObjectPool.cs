using System.Collections.Generic;
using UnityEngine;

namespace Utility.Unity.Common.ObjectPool
{
    public class ObjectPool
    {
        private readonly GameObject prefab;
        private readonly Transform parent;
        private readonly Queue<GameObject> pool = new();

        public ObjectPool(GameObject prefab, int initialSize, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                var obj = GameObject.Instantiate(prefab, parent);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public GameObject Get()
        {
            GameObject obj = pool.Count > 0 ? pool.Dequeue() : GameObject.Instantiate(prefab, parent); //메인
            obj.SetActive(true);

            if (obj.TryGetComponent<IPoolable>(out var poolable))
                poolable.Create();

            return obj;
        }

        public void Release(GameObject obj)
        {
            if (obj.TryGetComponent<IPoolable>(out var poolable))
                poolable.Destroy();

            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

}
