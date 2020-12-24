using Enum;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Sound
{
    public class OnClickSoundTools : MonoBehaviour
    {

        [SerializeField] private SfxSounds _sfxSounds = SfxSounds.click_1;
        [SerializeField] private Button _button;

#if UNITY_EDITOR
        [ContextMenu("GetButtonOptions")]
        public void GetButton()
        {
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(OnClickSound);
        }
#endif

        public void OnClickSound()
        {
            SoundController.Instance.PlaySfxSound(_sfxSounds);
        }
    }
}
