using MukSoft.Core.Domain;
using System.Threading.Tasks;

namespace MukSoft.Data.Interfaces
{
    public interface ILogEntryServices
    {
        Task Add(LogEntry model);
    }
}
