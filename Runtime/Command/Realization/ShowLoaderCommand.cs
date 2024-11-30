using System.Threading;
using Cysharp.Threading.Tasks;
using FPS.UI;
using VContainer;

namespace FPS
{
	public class ShowLoaderCommand : AsyncCommand
	{
		private readonly IUIService _uiService;
		private CommandQueue _commandQueue;

		[Inject]
		public ShowLoaderCommand(IUIService uiService)
		{
			_uiService = uiService;
		}

		public Command WithParams(CommandQueue commandQueue)
		{
			_commandQueue = commandQueue;
			return this;
		}

		public override async UniTask Do(CancellationToken token)
		{
			await _uiService.Show<UILoaderWindow>();
			_commandQueue.ProgressUpdateEvent += UpdateProgressBar;
			Status = CommandStatus.Success;
			_commandQueue = null;
		}

		private void UpdateProgressBar(float progress)
		{
			if (!_uiService.TryGet(out UILoaderWindow window))
				return;

			window.UpdateProgress(progress, progress.ToString("P0"));
		}
	}
}