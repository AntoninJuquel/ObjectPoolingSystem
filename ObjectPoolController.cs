using UnityEngine;

namespace ObjectPoolingSystem
{
    [DisallowMultipleComponent]
    public class ObjectPoolController : MonoBehaviour
    {
        private GameObject _prefab;

        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }

        private void OnDisable()
        {
            ObjectPoolManager.ReturnToPool(_prefab, gameObject);
        }
    }
}