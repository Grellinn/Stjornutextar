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
			: base("DefaultConnection")
		{
		}

		// Búum töflur fyrir gagnagrunninn sem á að mappa við klasana.
		public DbSet<Subtitle> Subtitles { get; set; }
		public DbSet<Title> Titles { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Language> Languages { get; set; }

		// Kemur í veg fyrir að EntityFramework-ið breyti nafni töflunnar í fleirtölu þegar hún býr hana til.
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}