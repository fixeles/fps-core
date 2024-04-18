using FPS.UI.ClickHandlers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.FX.UI
{
    public class DropdownFXClickHandler : FXClickHandler, IPointerClickHandler
    {
        [SerializeField] private TMP_Dropdown dropdown;
        private Camera _camera;

        private void OnValidate()
        {
            dropdown ??= GetComponent<TMP_Dropdown>();
        }

        private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(OnToggleChange);
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(OnToggleChange);
        }

        private void OnToggleChange(int index) => OnClick();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (dropdown.interactable)
                OnClick();
        }
    }
}