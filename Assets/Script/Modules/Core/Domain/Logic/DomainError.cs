namespace Modules.Core.Domain.Logic
{
    public abstract class DomainError
    {
        public string Message { get; }
        public object Error { get; }
    }
}