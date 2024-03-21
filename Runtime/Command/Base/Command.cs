namespace FPS
{
    public abstract class Command
    {
        private CommandStatus _status;
        public CommandStatus Status
        {
            get => _status;
            protected set => _status = value;
        }
        public virtual string Name() => GetType().Name;

        public enum CommandStatus
        {
            Queued,
            Success,
            Error
        }
    }
}