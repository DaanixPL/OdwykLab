using App.Domain.Entities;

namespace App.Domain.Abstractions
{
    public interface IDayRepository
    {
        Task<List<Day>> GetDaysByUserIdAsync(int userId, CancellationToken cancellationToken = default);

        Task AddDayAsync(Day day, CancellationToken cancellationToken = default);
        Task DeleteDayAsync(Day day, CancellationToken cancellationToken = default);
    }
}
