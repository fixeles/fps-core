using EasyButtons;
using UnityEngine;

namespace UIService.Mono
{
    public class RectScaleFitter : MonoBehaviour
    {
        [SerializeField] private Vector2 referenceSize;
        [SerializeField] private FitMode fitMode;

        private void Start()
        {
            ApplyScale();
        }

#if UNITY_EDITOR
        [Button]
        private void SerializeSize()
        {
            referenceSize = ScreenSize;
        }
#endif

        [Button]
        private void ApplyScale()
        {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(transform, "resize");
#endif
            var screenSize = ScreenSize;
            float scaleMultiplier = fitMode switch
            {
                FitMode.Auto => Mathf.Min(screenSize.x / referenceSize.x, screenSize.y / referenceSize.y),
                FitMode.Width => screenSize.x / referenceSize.x,
                FitMode.Height => screenSize.y / referenceSize.y,
                _ => 1
            };
            transform.localScale = Vector3.one * scaleMultiplier;
        }

        private static Vector2 ScreenSize
        {
            get
            {
#if UNITY_EDITOR
                string[] res = UnityEditor.UnityStats.screenRes.Split('x');
                return new Vector2(int.Parse(res[0]), int.Parse(res[1]));
#endif
#pragma warning disable CS0162
                return new Vector2(Screen.width, Screen.height);
#pragma warning restore CS0162
            }
        }

        private enum FitMode
        {
            Auto,
            Width,
            Height
        }
    }
}