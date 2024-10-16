using UnityEngine;
using UnityEngine.UI;

namespace FPS.UI.Buttons
{
    [DisallowMultipleComponent, RequireComponent(typeof(ModularButton))]
    public class ImageModule : ButtonModule
    {
        [SerializeField, Get(true)] private Image image;

        public void Fill(float amount)
        {
            image.fillAmount = amount;
        }
    }
}