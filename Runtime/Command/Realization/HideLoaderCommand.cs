using FPS.UI;

namespace FPS
{
    public class HideLoaderCommand : SyncCommand
    {
       public override void Do()
        {
            UIService.Hide<UILoaderWindow>();
            Status = CommandStatus.Success;
        }
    }
}