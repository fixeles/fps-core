using System.Threading;
using Cysharp.Threading.Tasks;

namespace FPS
{
    public abstract class AsyncCommand : Command
    {
        public abstract UniTask<CommandStatus> Do(CancellationToken token);
    }
}