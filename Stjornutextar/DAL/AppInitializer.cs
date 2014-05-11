using Stjornutextar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stjornutextar.DAL
{
	public class AppInitializer : System.Data.Entity.DropCreateDatabaseAlways<AppContext>
	{
		protected override void Seed(AppContext context)
		{
			// Búum til lista af NewsItem klösum og frumstillum þá með titli, texta, flokk og dagsetningu fréttar, til þess að hafa fylla inn í gagnagrunnstöfluna í upphafi.
			var titles = new List<Title>
			{
				new Title{
					TitleName="Iron Man"
				},
				new Title{
					TitleName="Iron Man2"
				},
				new Title{
					TitleName="Iron Man3"
				},
				new Title{
					TitleName="Iron Man4"
				},
				new Title{
					TitleName="Iron Man5"
				},
				new Title{
					TitleName="Iron Man6"
				}
			};

			titles.ForEach(t => context.Titles.Add(t));
			context.SaveChanges();

			var categories = new List<Category>
			{
				new Category{
					CategoryName="Kvikmyndir"
				},
				new Category{
					CategoryName="Þættir"
				},
				new Category{
					CategoryName="Fræðsluefni"
				},
				new Category{
					CategoryName="Barnaefni"
				}
			};

			categories.ForEach(c => context.Categories.Add(c));
			context.SaveChanges();
		}
	}
}