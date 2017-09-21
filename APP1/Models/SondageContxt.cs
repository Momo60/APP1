using System;
using Microsoft.EntityFrameworkCore;
using USherbrooke.ServiceModel.Sondage;

namespace APP1.Models
{
    public class SondageContext : DbContext
	{
		public SondageContext(DbContextOptions<SondageContext> options)
			: base(options)
		{
		}

		public DbSet<SimpleSondageDAO> SimpleSondageDAO { get; set; }

	}
}
