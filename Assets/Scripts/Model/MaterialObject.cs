
using System;
using Colors;
using Enum;
using UITools;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class MaterialObject
    {
        [SerializeField] private ModelType _colorType;
        [SerializeField] private Material _material;
        [SerializeField] private Material _materialMetal;
        [SerializeField] private Material _materialTransperent;
        [SerializeField] private MeshRenderer _meshRenderer;

        public ModelType ColorType { get { return _colorType; } }

        public string GetColor()
        {
            return _material.color.GetColorHex();
        }

        public void UpdateColor(ColorObject colorObject)
        {
            _material.color = colorObject.Color;
            _materialMetal.color = colorObject.Color;
            if (colorObject.MetalicSmoothess > 0.8f)
            {
                _meshRenderer.material = _materialMetal;
            }
            else if (colorObject.MetalicSmoothess > 0.5f)
            {
                _meshRenderer.material = _materialTransperent;
            }
            else
            {
                _meshRenderer.material = _material;
            }
        }
    }
}