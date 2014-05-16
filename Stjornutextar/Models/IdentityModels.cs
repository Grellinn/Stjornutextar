using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Stjornutextar.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		// Látum default constructorinn vera tengingu við AppContext reference-ið í web.config
		public ApplicationDbContext()
			: base("RuDatabase")
		{
		}

		// Búum töflur fyrir gagnagrunninn sem á að mappa við klasana.
		public DbSet<Category> Categories { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<Subtitle> Subtitles { get; set; }
		public DbSet<SubtitlePart> SubtitleParts { get; set; }

		// Kemur í veg fyrir að EntityFramework-ið breyti nafni töflunnar í fleirtölu þegar hún býr hana til.
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
			modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
			modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
		}
	}
}