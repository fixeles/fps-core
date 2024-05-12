namespace FPS
{
    public class BaseCommandsQueue
    {
        public static void Insert(CommandQueue commandQueue)
        {
            commandQueue.Enqueue(new UIServiceInitCommand());
            commandQueue.Enqueue(new ShowLoaderCommand(commandQueue));
            commandQueue.Enqueue(new AudioInitCommand());
#if FPS_POOL
            commandQueue.Enqueue(new PoolInitCommand());
#endif
#if FPS_LOC
            commandQueue.Enqueue(new LocalizationInitCommand());
#endif
        }
    }
}