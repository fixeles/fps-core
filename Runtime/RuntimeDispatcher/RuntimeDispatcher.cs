using System.Threading;
using UnityEngine;

namespace FPS
{
    public class RuntimeDispatcher : MonoBehaviour
    {
        private static RuntimeDispatcher _instance;

        public static CancellationToken CancellationToken => _instance.destroyCancellationToken;

        public void Init()
        {
            _instance ??= this;
        }
    }
}