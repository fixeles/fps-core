namespace FPS
{
    public class BaseCommandsQueue
    {
        public static void Insert(CommandQueue commandQueue)
        {
            commandQueue.Enqueue(new UIServiceInitCommand());
            commandQueue.Enqueue(new ShowLoaderCommand(commandQueue));
#if FPS_POOL
            commandQueue.Enqueue(new PoolInitCommand());
#endif
            commandQueue.Enqueue(new AudioInitCommand());
#if FPS_LOC
            commandQueue.Enqueue(new LocalizationInitCommand());
#endif
        }
    }
}