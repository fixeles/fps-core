using UnityEngine;

namespace FPS
{
    public interface IWindow
    {
        void Show(WindowSwitchType switchType);
        void Hide(WindowSwitchType switchType);
        RectTransform Transform { get; }
    }
}