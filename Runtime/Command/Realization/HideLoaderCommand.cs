using FPS.UI;
using VContainer;

namespace FPS
{
	public class HideLoaderCommand : SyncCommand
	{
		private IUIService _uiService;
		private CommandQueue _commandQueue;

		[Inject]
		public HideLoaderCommand(IUIService uiService)
		{
			_uiService = uiService;
		}

		public override void Do()
		{
			_uiService.Hide<UILoaderWindow>();
			_commandQueue.ClearSubscriptions();
			Status = CommandStatus.Success;
			_commandQueue = null;
			_uiService = null;
		}

		public Command WithParams(CommandQueue commandQueue)
		{
			_commandQueue = commandQueue;
			return this;
		}
	}
}