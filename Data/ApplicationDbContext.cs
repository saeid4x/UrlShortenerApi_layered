using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApi01.Models;

namespace UrlShortenerApi01.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        public DbSet<Membership> Memeberships { get; set; }
        public DbSet<ClickStat> ClickStat { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<QrCode> QrCode { get; set; }
        public DbSet<ShortLinks> ShortLinks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // seed membership data 
            builder.Entity<Membership>().HasData(
                new Membership
                {
                    Id = 1,
                    AllowAnalytics = false,
                    AllowCustomShortLinks = false,
                    AllowExpiration = false,
                    MaxQrCodes = 2,
                    MaxShortLinks = 5,
                    MontlyPrice = 0,
                    Name = "Basic",
                },
                new Membership
                {
                    Id = 2,
                    Name = "Pro",
                    MontlyPrice = 20,
                    AllowExpiration = true,
                    AllowCustomShortLinks = true,
                    AllowAnalytics = true,
                    MaxQrCodes = 6,
                    MaxShortLinks = 10,
                },
                new Membership
                {
                    Id = 3,
                    Name = "Plus",
                    MontlyPrice = 200,
                    AllowExpiration = true,
                    AllowCustomShortLinks = true,
                    MaxShortLinks = 1000,
                    MaxQrCodes = 100,
                    AllowAnalytics = true,

                });   
            

            // one-to-one: ApplicationUser <-> Profile (explicit foreign key)
            builder.Entity<ApplicationUser>()
                .HasOne(a=>a.Profile)
                .WithOne(p=>p.User)
                .HasForeignKey<Profile>(p=>p.UserId) // Explicitly setting foreign key
                 .OnDelete(DeleteBehavior.Cascade);  // Adjust as needed


        }



    }
}
