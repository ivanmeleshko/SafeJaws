
using System;
using System.Collections.Generic;
using Colors;
using Enum;
using UnityEngine;

namespace Model
{
    public class ModelObject : MonoBehaviour
    {

        [SerializeField] private ModelType _modelType;
        [SerializeField] private MaterialObject[] _materialObjects;

        private Dictionary<ModelType, MaterialObject> _materialObjectDictionary;

        public Dictionary<ModelType, MaterialObject> MaterialObjectDictionary
        {
            get
            {
                if (_materialObjectDictionary == null)
                {
                    _materialObjectDictionary = new Dictionary<ModelType, MaterialObject>();
                    for (int i = 0; i < _materialObjects.Length; i++)
                    {
                        if(_materialObjectDictionary.ContainsKey(_materialObjects[i].ColorType))
                            throw new Exception("Color material object already exist");
                        _materialObjectDictionary.Add(_materialObjects[i].ColorType, _materialObjects[i]);
                    }
                }

                return _materialObjectDictionary;
            }
        }

        public ModelType ModelType { get { return _modelType; } }

        public void Active()
        {
            gameObject.SetActive(true);
        }

        public void Deactive()
        {
            gameObject.SetActive(false);
        }

        public void UpdateColor(ModelType colorType, ColorObject colorObject)
        {
            if(MaterialObjectDictionary.ContainsKey(colorType))
                MaterialObjectDictionary[colorType].UpdateColor(colorObject);
            //else
            //    throw new Exception("Color material objects not found.");
        }

        public string GetColor(ModelType colorType)
        {
            if (MaterialObjectDictionary.ContainsKey(colorType))
               return MaterialObjectDictionary[colorType].GetColor();
            else
                throw new Exception("Color material objects not found.");
        }
    }
}
