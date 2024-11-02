using FPS.UI;

namespace FPS
{
    public class HideLoaderCommand : SyncCommand
    {
        private readonly CommandQueue _queue;

        public HideLoaderCommand(CommandQueue queue)
        {
            _queue = queue;
        }

        public override void Do()
        {
            UIService.Hide<UILoaderWindow>();
            _queue.ClearSubscriptions();
            Status = CommandStatus.Success;
        }
    }
}