using UnityEngine;
using UnityEngine.UI;

namespace FPS.Buttons.ModularButton.Modules
{
    [DisallowMultipleComponent, RequireComponent(typeof(Game.UIService.Buttons.ModularButton))]
    public class ColorModule : ButtonModule
    {
        [SerializeField, Get(true)] private Graphic target;

        public void SetColor(Color color)
        {
            target.color = color;
        }
    }
}