using DAP.Runtime.Core;
using DAP.Runtime.Data;
using UnityEngine;
using UnityEngine.UI;

namespace DAP.Runtime.UI
{
    public class ButtonSFX : MonoBehaviour
    {
        [SerializeField] private SFXType _sfxType = SFXType.ButtonClick;

        private Button _button;

        private void Awake() => _button = GetComponent<Button>();
        private void OnEnable() => _button?.onClick.AddListener(PlaySound);
        private void OnDisable() => _button?.onClick.RemoveListener(PlaySound);
        private void PlaySound() => AudioDispatcher.PlaySFX(_sfxType);

    }
}