using System.Threading;
using Cysharp.Threading.Tasks;
using FPS.UI;

namespace FPS
{
    public class ShowLoaderCommand : AsyncCommand
    {
        private readonly CommandQueue _commandQueue;

        public ShowLoaderCommand(CommandQueue commandQueue)
        {
            _commandQueue = commandQueue;
        }

        public override async UniTask Do(CancellationToken token)
        {
            await UIService.Show<UILoaderWindow>();
            _commandQueue.ProgressUpdateEvent += UpdateProgressBar;
            Status = CommandStatus.Success;
        }

        private void UpdateProgressBar(float progress)
        {
            if (!UIService.TryGet(out UILoaderWindow window))
                return;

            window.UpdateProgress(progress, progress.ToString("P"));
        }
    }
}