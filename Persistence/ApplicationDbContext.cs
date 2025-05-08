namespace StrokePrediction.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<Medication> Medications { get; set; }
    }

}
