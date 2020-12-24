
using UnityEngine;

namespace UITools
{
    public class MarkerFollow : MonoBehaviour
    {
        private Transform _targetTransform;

        public void SetFolowObject(Transform targetTransform)
        {
            gameObject.SetActive(true);
            _targetTransform = targetTransform;
        }

        private void LateUpdate()
        {
            if (_targetTransform)
                transform.position = _targetTransform.position;
        }
    }
}
