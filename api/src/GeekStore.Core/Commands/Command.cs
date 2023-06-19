using GeekStore.Core.Models;
using MediatR;

namespace GeekStore.Core.Commands
{
    public abstract class Command : Validable, IRequest
    {
        public Command()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; private set; }
    }
}
