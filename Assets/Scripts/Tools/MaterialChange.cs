using UnityEngine;

namespace Tools
{
    public class MaterialChange : MonoBehaviour
    {
        [SerializeField] private Material _targetMaterial;
        [SerializeField] private Texture2D _clearTexture;
        [SerializeField] private Material _targetMaterial1;


#if UNITY_EDITOR
        private void OnDestroy()
        {
            _targetMaterial.mainTexture = _clearTexture;
            _targetMaterial1.mainTexture = _clearTexture;
        }
#endif
    }
}
