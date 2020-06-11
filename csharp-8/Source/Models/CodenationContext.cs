using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Codenation.Challenge.Models
{
    public class CodenationContext : DbContext
    {
        public DbSet<Acceleration> Accelerations { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<User> Users { get; set; }
                
        // this constructor is for enable testing with in-memory data
        public CodenationContext(DbContextOptions<CodenationContext> options)
            : base(options)
        {             
        }
        //criei essa linha
        public CodenationContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
          // optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Codenation;Trusted_Connection=True");        
                optionsBuilder.UseSqlServer(@"Data Source=PROGRAMADOR;" +
                    "Initial Catalog=Codenation;Persist Security Info = False;Integrated Security=SSPI");
                // configurar um arquivo jsom 
         /*
            var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appserrings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaulConnection"));
         */
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new CandidateConfiguration());
            modelBuilder.ApplyConfiguration(new SubmissionConfiguration());
        }
    }
}