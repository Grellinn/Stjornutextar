﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Stjornutextar.DAL
{
	public class AppContext : DbContext
	{
		// Látum default constructorinn vera tengingu við AppContext reference-ið í web.config
		public AppContext()
			: base("DefaultConnection")
		{

		}

		// Búum töflur fyrir gagnagrunninn sem á að mappa við klasana.
		//public DbSet<NewsItem> News { get; set; }

		// Kemur í veg fyrir að EntityFramework-ið breyti nafni töflunnar í fleirtölu þegar hún býr hana til.
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}