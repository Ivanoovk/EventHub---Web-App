namespace EventHubApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class EventHubAppDbContext : IdentityDbContext
    {
        public EventHubAppDbContext(DbContextOptions<EventHubAppDbContext> options)
            : base(options)
        {
        }
    }
}
