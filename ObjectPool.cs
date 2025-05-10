using UnityEngine;

namespace ObjectPoolingSystem
{
    [System.Serializable]
    public class ObjectPool
    {
        public string key;
        public GameObject prefab;

        [Min(1)] public int size;
    }
}