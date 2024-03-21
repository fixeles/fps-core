using UnityEngine;

namespace FPS
{
    public class AppLauncher : MonoBehaviour
    {
        private void Launch()
        {
            using var queue = new CommandQueue();
            //queue.AddCommand(new LocalizationInitCommand());
            //queue.AddCommand(new PoolInitCommand());
            queue.Execute().Forget();
        }
    }
}