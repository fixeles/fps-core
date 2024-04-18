using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FPS.UI.ClickHandlers
{
    public abstract class FXClickHandler : MonoBehaviour
    {
        // [SerializeField] private string sfxName = "event:/UI/Clicks/MenuClick";
        // [Inject] private SFXController _sfxController;

        private static bool _cooldown;

        protected void OnClick()
        {
            if (_cooldown)
                return;

            //   _sfxController.PlaySFX(sfxName, 0.1f);
            RefreshCooldown().Forget();
        }

        private async UniTaskVoid RefreshCooldown()
        {
            _cooldown = true;
            await UniTask.Delay(200);
            _cooldown = false;
        }
    }
}