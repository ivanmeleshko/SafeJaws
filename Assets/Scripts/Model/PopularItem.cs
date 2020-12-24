using UnityEngine;

namespace Model
{
    public class PopularItem : MonoBehaviour
    {

        [SerializeField] private int _popularIndex;

        public void ChoosePopular()
        {
            ModelController.Instance.ChoosePopular(_popularIndex);
        }

        [ContextMenu("Update name")]
        private void UpdateName()
        {
            gameObject.name = string.Format("menuitem - {0}", _popularIndex);
        }
    }
}
