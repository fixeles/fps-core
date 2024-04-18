using TMPro;
using UnityEngine;

namespace FPS.Buttons.ModularButton.Modules
{
    [DisallowMultipleComponent, RequireComponent(typeof(Game.UIService.Buttons.ModularButton))]
    public class TextModule : ButtonModule
    {
        [SerializeField] private TMP_Text label;

        public void SetText(string content)
        {
            label.text = content;
        }

        public void SetColor(Color color)
        {
            label.color = color;
        }

        private void OnValidate()
        {
            if (label != null)
                return;

            var components = GetComponentsInChildren<TMP_Text>();
            if (components.Length > 0)
                label = components[0];
        }
    }
}