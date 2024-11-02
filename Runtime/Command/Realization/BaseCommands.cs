using FPS.UI;

namespace FPS
{
	public static class BaseCommands
	{
		public static void Insert(CommandQueue commandQueue)
		{
			commandQueue.Enqueue(new UIServiceInitCommand());
			commandQueue.Enqueue(new ShowLoaderCommand(commandQueue));
#if FPS_POOL
			commandQueue.Enqueue(new PoolInitCommand());
#endif
#if FPS_LOC
            commandQueue.Enqueue(new LocalizationInitCommand());
#endif
		}
	}
}