using FPS.UI.ClickHandlers;
using UnityEngine;

namespace Game.FX.UI
{
    public class ScrollViewFXClickHandler : FXClickHandler
    {
        [SerializeField] private RectTransform scrollRectTransform;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnValidate()
        {
            scrollRectTransform ??= GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && IsMouseWithinScrollRect())
                OnClick();
        }

        private bool IsMouseWithinScrollRect()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(scrollRectTransform, Input.mousePosition, _camera, out var localPoint);
            return scrollRectTransform.rect.Contains(localPoint);
        }
    }
}