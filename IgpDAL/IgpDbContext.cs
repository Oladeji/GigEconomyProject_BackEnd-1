
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

namespace IgpDAL
{
//public class IgpDbContext :DbContext
 public class IgpDbContext : IdentityDbContext<IdentityUser>
{
    public IgpDbContext(DbContextOptions<IgpDbContext>options ):base (options)
    {
         Database.EnsureCreated();  
    }
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
          base.OnModelCreating(modelBuilder);
          modelBuilder.Entity<IgpUser>();
          modelBuilder.Seed();
          
//                 // Configure Student & StudentAddress entity
//                 modelBuilder.Entity<JobStatus>()
//                 .HasOptional(s => s.Invoice) // Mark Address property optional in Student entity
//                 .WithRequired(ad => ad.JobStatus); // mark Student property as required in StudentAddress entity. Cannot save StudentAddress without Student


// // Configure the primary key for the OfficeAssignment
//              modelBuilder.Entity<JobStatus>()
//              .HasKey(t => t.JobStatusId);
           
// // Configure the primary key for the OfficeAssignment
// modelBuilder.Entity<Invoice>()
//     .HasKey(t => t.InvoiceId);

// // Map one-to-zero or one relationship
// modelBuilder.Entity<Invoice>()
//     .HasRequired(t => t.)
//     .WithOptional(t => t.OfficeAssignment);


        }
    public DbSet<Client> Clients { get; set; }
    
    public DbSet<FinancedProject> FinancedProject { get; set; }
    public DbSet<IntentionBoard> IntentionBoards { get; set; }
    public DbSet<JobStatus> JobStatuss { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<JobBoard> JobBoards { get; set; }  
    public DbSet<ProviderSkillSet> ProviderSkillSets { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ServiceProvider> ServiceProviders { get; set; }  
    public DbSet<ServiceProviderDetail> ServiceProviderDetails { get; set; }
    public DbSet<SkillType> SkillTypes { get; set; }
    }
}