using FPS.UI.ClickHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.FX.UI
{
    public class ButtonFXClickHandler : FXClickHandler
    {
        [SerializeField] private Button button;

        private void OnValidate()
        {
            button ??= GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }
    }
}