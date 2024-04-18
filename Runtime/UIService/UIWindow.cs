using UnityEngine;

namespace FPS
{
    [RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
    public abstract class UIWindow : MonoBehaviour, IWindow
    {
        [SerializeField, Get] private RectTransform cachedTransform;
        [SerializeField, Get] private CanvasGroup canvasGroup;

        public RectTransform Transform => cachedTransform;

        public void Show(WindowSwitchType switchType)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            BeforeShow();
            gameObject.SetActive(true);
        }

        public void Hide(WindowSwitchType switchType)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
            AfterHide();
        }

        protected void OnValidate()
        {
            var windowTypeName = GetType().Name;
            if (gameObject.name != windowTypeName)
                gameObject.name = windowTypeName;
        }

        protected virtual void BeforeShow()
        {
        }

        protected virtual void AfterHide()
        {
        }
    }
}