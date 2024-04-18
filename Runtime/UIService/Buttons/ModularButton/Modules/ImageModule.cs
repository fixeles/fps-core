using UnityEngine;
using UnityEngine.UI;

namespace FPS.Buttons.ModularButton.Modules
{
    [DisallowMultipleComponent, RequireComponent(typeof(Game.UIService.Buttons.ModularButton))]
    public class ImageModule : ButtonModule
    {
        [SerializeField, Get(true)] private Image image;

        public void Fill(float amount)
        {
            image.fillAmount = amount;
        }
    }
}