using Backend_dotnet8.Core.DbContext;
using Backend_dotnet8.Core.Dtos.Log;
using Backend_dotnet8.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend_dotnet8.Core.Services.Implements
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _context;

        public LogService(AppDbContext context)
        {
            _context = context;
        }


        public async Task SaveNewLog(string userName, string description)
        {
            var newLog = new Log()
            {
                UserName = userName,
                Description = description
            };
            await _context.Logs.AddAsync(newLog);
            await _context.SaveChangesAsync();

        }
        public async Task<IEnumerable<GetLogDto>> GetLogAsync()
        {
            var logs = await _context.Logs.Select(log => new GetLogDto
            {
                CreatedAt = log.CreatedAt,
                Description = log.Description,
                UserName = log.UserName,
            }).OrderByDescending(log => log.CreatedAt)
            .ToListAsync();

            return logs;
        }

        public async Task<IEnumerable<GetLogDto>> GetMyLogsAsync(ClaimsPrincipal user)
        {
            var logs = await _context.Logs
                .Where(log => log.UserName == user.Identity.Name)
                .Select(log => new GetLogDto
                {
                    CreatedAt = log.CreatedAt,
                    Description = log.Description,
                    UserName = log.UserName,
                }).OrderByDescending(log => log.CreatedAt)
            .ToListAsync();

            return logs;
        }


    }
}
