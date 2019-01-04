using MukSoft.Core.Context;
using MukSoft.Core.Domain;
using MukSoft.Data.Interfaces;
using System.Threading.Tasks;

namespace MukSoft.Data.Services
{
    public class LogEntryServices : ILogEntryServices
    {
        private readonly ApplicationDbContext _context;
        public LogEntryServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(LogEntry model)
        {
            await _context.LogEntries.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
