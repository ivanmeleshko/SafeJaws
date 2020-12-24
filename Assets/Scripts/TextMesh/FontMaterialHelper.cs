
using UnityEngine;

namespace TextMesh
{
    public class FontMaterialHelper : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _targetMaterialMeshRenderer;
        [SerializeField] private MeshRenderer _newMaterialMeshRenderer;
        [SerializeField] private MeshFilter _targetMeshFilter;
        [SerializeField] private UnityEngine.TextMesh _textMesh;


        private void Update()
        {
            _newMaterialMeshRenderer.materials = _targetMaterialMeshRenderer.materials;
        }
    }
}
