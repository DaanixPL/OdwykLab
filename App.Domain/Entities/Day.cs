namespace OdwykLab.Domain.Entities
{
    public class Day
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
