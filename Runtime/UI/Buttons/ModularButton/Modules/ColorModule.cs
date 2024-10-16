using UnityEngine;
using UnityEngine.UI;

namespace FPS.UI.Buttons
{
    [DisallowMultipleComponent, RequireComponent(typeof(ModularButton))]
    public class ColorModule : ButtonModule
    {
        [SerializeField, Get(true)] private Graphic target;

        public void SetColor(Color color)
        {
            target.color = color;
        }
    }
}