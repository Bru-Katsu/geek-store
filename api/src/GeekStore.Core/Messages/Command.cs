using FluentValidation.Results;
using GeekStore.Core.Messages;
using MediatR;

namespace GeekStore.Core.Messages
{
    public abstract class Command : Message, IRequest
    {
        public Command()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid() => throw new NotImplementedException("Recurso de validação não está implementado!");
    }
}
