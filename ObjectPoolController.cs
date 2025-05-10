using UnityEngine;

namespace ObjectPoolingSystem
{
    public class ObjectPoolController : MonoBehaviour
    {
        private ObjectPoolManager _objectPoolManager;
        private string _key;

        private void OnDisable()
        {
            if (!_objectPoolManager)
            {
                Debug.LogWarning($"An ObjectPoolController {gameObject.name} has been disabled but has no ObjectPool set");
                return;
            }

            _objectPoolManager.ReturnToPool(gameObject, _key);
        }

        public void SetObjectPool(ObjectPoolManager objectPoolManager, string key, bool setActive = true)
        {
            _objectPoolManager = objectPoolManager;
            _key = key;
            gameObject.SetActive(setActive);
        }
    }
}