namespace Stjornutextar.Migrations
{
	using Stjornutextar.Models;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<Stjornutextar.Models.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			ContextKey = "Stjornutextar.Models.ApplicationDbContext";
		}

		protected override void Seed(Stjornutextar.Models.ApplicationDbContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
			context.Languages.AddOrUpdate(
				new Language { LanguageName = "Íslenska" },
				new Language { LanguageName = "Enska" },
				new Language { LanguageName = "Danska" },
				new Language { LanguageName = "Sænska" }
				);

			//context.Titles.AddOrUpdate(
			//	new Title { TitleName = "Dark Knight" },
			//	new Title { TitleName = "Superbad" },
			//	new Title { TitleName = "Breaking Bad" },
			//	new Title { TitleName = "World War II - Documentary" },
			//	new Title { TitleName = "Smurf" },
			//	new Title { TitleName = "Seinfeld" }
			//	);

			context.Categories.AddOrUpdate(
				new Category { CategoryName = "Kvikmyndir" },
				new Category { CategoryName = "Þættir" },
				new Category { CategoryName = "Heimildarmyndir" },
				new Category { CategoryName = "Barnaefni" }
				);
		}
	}
}
