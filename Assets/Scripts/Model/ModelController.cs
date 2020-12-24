
using Colors;
using Enum;
using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using Managers;

namespace Model
{
    public class ModelController : Singelton<ModelController>
    {
        [SerializeField] private GameObject _customGroup;
        [SerializeField] private GameObject _popularGroup;
        [SerializeField] private ModelObject[] _modelObjects;
        [SerializeField] private List<PopularObject> popularItems = new List<PopularObject>();

        private Dictionary<int, PopularObject> _popularItems = null;
        private int _currentIndex = 18;
        public bool selectedPopular = false;
        public int SelectedPopularDesign { get; set; }
        public bool IsPopular { get; private set; } = false;
        private Dictionary<ModelType, ModelObject> _modelObjectDictionary;

        public Dictionary<ModelType, ModelObject> ModelObjectDictionary
        {
            get
            {
                if (_modelObjectDictionary == null)
                {
                    _modelObjectDictionary = new Dictionary<ModelType, ModelObject>();
                    for (int i = 0; i < _modelObjects.Length; i++)
                    {
                        if (_modelObjectDictionary.ContainsKey(_modelObjects[i].ModelType))
                        {
                            throw new Exception("Dictionary already contains this key");
                        }
                        _modelObjectDictionary.Add(_modelObjects[i].ModelType, _modelObjects[i]);
                    }
                }

                return _modelObjectDictionary;
            }
        }
        public Dictionary<int, PopularObject> PopularItems
        {
            get
            {
                if (_popularItems == null)
                {
                    _popularItems = new Dictionary<int, PopularObject>();
                    for (int i = 0; i < popularItems.Count; i++)
                        _popularItems.Add(popularItems[i].Index, popularItems[i]);
                }
                return _popularItems;
            }
        }

        public int CurrentIndex { get => _currentIndex;}

        public ModelType SelectModel(ModelType modelTypeSelected)
        {
            ModelObjectDictionary.ForEach((type, p) => p.Deactive());
            if (ModelObjectDictionary.ContainsKey(modelTypeSelected))
            {
                ModelObjectDictionary[modelTypeSelected].Active();
                return modelTypeSelected;
            }
            
            throw new Exception("Model not find.");
        }

        public void UpdateColor(ModelType currentModel, ModelType colorType, ColorObject colorObject)
        {
            if (ModelObjectDictionary.ContainsKey(currentModel))
            {
                ModelObjectDictionary[currentModel].UpdateColor(colorType, colorObject);
                UpdateAllModel(colorType, colorObject);
            }
            //else
            //    throw new Exception("Model not find.");
        }

        private void UpdateAllModel(ModelType colorType, ColorObject colorObject)
        {
            if (ModelObjectDictionary.ContainsKey(ModelType.OneColor)) ModelObjectDictionary[ModelType.OneColor].UpdateColor(colorType, colorObject);
            if (ModelObjectDictionary.ContainsKey(ModelType.TwoColor)) ModelObjectDictionary[ModelType.TwoColor].UpdateColor(colorType, colorObject);
            if (ModelObjectDictionary.ContainsKey(ModelType.ThirdColor)) ModelObjectDictionary[ModelType.ThirdColor].UpdateColor(colorType, colorObject);
        }

        public string GetColor(ModelType currentModel, ModelType colorType)
        {
            return ModelObjectDictionary[currentModel].GetColor(colorType);
        }

        public void SelectCustom()
        {
            selectedPopular = false;
            _customGroup.SetActive(true);
            _popularGroup.SetActive(false);
            IsPopular = false;
            Managers.GameManager.Instance.ChangePrice();
        }

        public void SelectPopular()
        {
            selectedPopular = true;
            _customGroup.SetActive(false);
            _popularGroup.SetActive(true);
            IsPopular = true;
            Managers.GameManager.Instance.ChangePrice();
            ChoosePopular(_currentIndex);
        }
        
        public void ChoosePopular(int index)
        {
            _currentIndex = index;
            popularItems.ForEach(p => p.Hide());
            if (PopularItems.ContainsKey(index))
            {
                PopularItems[index].Show();
                if (GameManager.Instance.StartPrice == 0)
                    GameManager.Instance.ChangePrice(0);
                else
                    GameManager.Instance.ChangePrice(PopularItems[index].Price);
            }
            else
            {
                throw new Exception("Key not found in dictionary.");
            }
        }

        public string GetCurrentProductId()
        {
            if (PopularItems.ContainsKey(_currentIndex))
                return PopularItems[_currentIndex].ProductId;
            return "6823015943";
        }

        [ContextMenu("Update name")]
        private void UpdateName()
        {
            popularItems.ForEach(p => p.UpdateName());
        }
    }

    [Serializable]
    public class PopularObject
    {
        public string name;
        [SerializeField] private int _index;
        [SerializeField] private GameObject _object;
        [SerializeField] private float _price;
        [SerializeField] private string _productId;

        public int Index { get => _index; }
        public float Price { get => _price; }
        public string ProductId { get => _productId; }

        public void Show()
        {
            _object.SetActive(true);
        }

        public void Hide()
        {
            _object.SetActive(false);
        }

        public void UpdateName()
        {
            name = string.Format("{0} : {1}", _index, _object.name);
        }

    }
}