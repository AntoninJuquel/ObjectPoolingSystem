using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolingSystem
{
    public static class ObjectPoolManager
    {
        private static readonly Dictionary<GameObject, Queue<GameObject>> _poolDictionary = new();

        public static GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(prefab))
            {
                _poolDictionary[prefab] = new Queue<GameObject>();
            }

            GameObject obj;

            if (_poolDictionary[prefab].Count > 0)
            {
                obj = _poolDictionary[prefab].Dequeue();
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
            }
            else
            {
                obj = GameObject.Instantiate(prefab, position, rotation);
                obj.TryGetComponent<ObjectPoolController>(out var poolController);
                if (poolController == null)
                {
                    poolController = obj.AddComponent<ObjectPoolController>();
                }
                poolController.SetPrefab(prefab);
            }

            return obj;
        }

        public static T SpawnFromPool<T>(GameObject prefab, Vector3 position, Quaternion rotation) where T : Component
        {
            return SpawnFromPool(prefab, position, rotation).GetComponent<T>();
        }

        public static void ReturnToPool(GameObject prefab, GameObject obj)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(false);
            }

            if (!_poolDictionary.ContainsKey(prefab))
            {
                _poolDictionary[prefab] = new Queue<GameObject>();
            }

            _poolDictionary[prefab].Enqueue(obj);
        }
    }
}
