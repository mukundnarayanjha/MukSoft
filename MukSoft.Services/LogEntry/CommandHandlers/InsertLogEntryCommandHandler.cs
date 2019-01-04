using MediatR;
using MukSoft.Services.LogEntry.Command;
using System.Threading;
using System.Threading.Tasks;

namespace MukSoft.Services.LogEntry.CommandHandlers
{
    public class InsertLogEntryCommandHandler : IRequestHandler<InsertLogEntryCommand, bool>
    {
        public InsertLogEntryCommandHandler()
        {

        }
        public Task<bool> Handle(InsertLogEntryCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
