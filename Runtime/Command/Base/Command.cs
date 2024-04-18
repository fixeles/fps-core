namespace FPS
{
    public abstract class Command
    {
        public CommandStatus Status { get; protected set; } = CommandStatus.Queued;
        public virtual string Name => GetType().Name;

        public enum CommandStatus
        {
            Queued,
            Success,
            Error
        }
    }
}