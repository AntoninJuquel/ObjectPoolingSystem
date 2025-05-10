using UnityEngine;
using System.Collections.Generic;

namespace ObjectPoolingSystem
{
    public class ObjectPoolManager : MonoBehaviour
    {
        [SerializeField] private List<ObjectPool> objectPools = new();
        private readonly Dictionary<string, Queue<GameObject>> _poolDictionary = new();
        private readonly Dictionary<string, GameObject> _prefabLookup = new();

#if UNITY_EDITOR
        [SerializeField] private bool debug = false;
#endif

        protected virtual void Awake()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            foreach (var pool in objectPools)
            {
                if (string.IsNullOrEmpty(pool.key))
                {
                    Debug.LogWarning("ObjectPool key is missing.");
                    continue;
                }

                if (pool.prefab == null)
                {
                    Debug.LogWarning($"ObjectPool '{pool.key}' is missing a prefab.");
                    continue;
                }

                if (_poolDictionary.ContainsKey(pool.key))
                {
                    Debug.LogWarning($"Duplicate key detected: {pool.key}. Skipping.");
                    continue;
                }

                _poolDictionary[pool.key] = new Queue<GameObject>();
                _prefabLookup[pool.key] = pool.prefab;

                for (int i = 0; i < pool.size; i++)
                {
                    Instantiate(pool.prefab).AddComponent<ObjectPoolController>().SetObjectPool(this, pool.key, false);
                }

#if UNITY_EDITOR
                if (debug)
                    Debug.Log($"[ObjectPooling] Initialized pool '{pool.key}' with {pool.size} objects.");
#endif
            }
        }

        public GameObject SpawnFromPool(string key, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(key))
            {
                Debug.LogWarning($"Object pool with key '{key}' not found.");
                return null;
            }

            GameObject obj;

            if (_poolDictionary[key].Count > 0)
            {
                obj = _poolDictionary[key].Dequeue();
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
            }
            else
            {
                obj = Instantiate(_prefabLookup[key], position, rotation);
                obj.AddComponent<ObjectPoolController>().SetObjectPool(this, key);

#if UNITY_EDITOR
                if (debug)
                    Debug.Log($"[ObjectPooling] Instantiated new '{key}' as pool was empty.");
#endif
            }

            return obj;
        }

        public T SpawnFromPool<T>(string key, Vector3 position, Quaternion rotation) where T : Component
        {
            return SpawnFromPool(key, position, rotation).GetComponent<T>();
        }

        public void ReturnToPool(GameObject obj, string key)
        {
            if (!_poolDictionary.ContainsKey(key))
            {
                Debug.LogWarning($"Trying to return object to non-existent pool: '{key}'");
                Destroy(obj);
                return;
            }

            obj.SetActive(false);
            _poolDictionary[key].Enqueue(obj);

#if UNITY_EDITOR
            if (debug)
                Debug.Log($"[ObjectPooling] Returned '{obj.name}' to pool '{key}'.");
#endif
        }
    }
}
