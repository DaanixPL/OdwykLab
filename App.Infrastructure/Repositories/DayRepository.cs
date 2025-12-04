using App.Domain.Abstractions;
using App.Domain.Entities;
using App.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories
{
    public class DayRepository : IDayRepository
    {
        private readonly AppDbContext _dbContext;
        public DayRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Day>> GetDaysByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Days
                .Where(day => day.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddDayAsync(Day day, CancellationToken cancellationToken = default)
        {
            await _dbContext.Days.AddAsync(day, cancellationToken);
            return;
        }
        public async Task DeleteDayAsync(Day day, CancellationToken cancellationToken = default)
        {
            _dbContext.Days.Remove(day);
            return;
        }
    }
}
