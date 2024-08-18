using Microsoft.EntityFrameworkCore;
using Payment.Adapter.Out.Configurations;

namespace Payment.Adapter.Out;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// OnModelCreating
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.ApplyConfiguration(new PaymentConfigurations());
    }


    /// <summary>
    /// Gets or sets the MessageChannels.
    /// </summary>
    /// <value>
    /// The MessageChannels.
    /// </value>
    public virtual DbSet<Entities.Payment> Payments { get; set; }
}