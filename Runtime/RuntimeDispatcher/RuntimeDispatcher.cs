using System.Threading;
using UnityEngine;

namespace FPS
{
    public class RuntimeDispatcher : MonoBehaviour
    {
        public CancellationToken CancellationToken => destroyCancellationToken;
    }
}