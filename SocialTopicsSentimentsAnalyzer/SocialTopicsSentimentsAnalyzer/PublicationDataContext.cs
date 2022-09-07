using Microsoft.EntityFrameworkCore;
using SocialTopicsSentimentsAnalyzer.Entities;

namespace SocialTopicsSentimentsAnalyzer;

public class PublicationDataContext : DbContext
{
	public DbSet<Publication> Publications { get; set; }
	public DbSet<Community> Communities { get; set; }
	
	public PublicationDataContext()
	{
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(@"DataSource=PublicationsData.db;Cache=Shared");
	}
}