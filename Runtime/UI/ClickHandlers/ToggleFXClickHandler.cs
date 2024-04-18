using FPS.UI.ClickHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.FX.UI
{
    public class ToggleFXClickHandler : FXClickHandler
    {
        [SerializeField] private Toggle toggle;

        private void OnValidate()
        {
            toggle ??= GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(OnToggleChange);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(OnToggleChange);
        }

        private void OnToggleChange(bool _) => OnClick();
    }
}