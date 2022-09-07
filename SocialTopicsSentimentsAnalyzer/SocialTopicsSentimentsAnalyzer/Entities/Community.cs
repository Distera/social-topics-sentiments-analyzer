namespace SocialTopicsSentimentsAnalyzer.Entities;

public class Community
{
	private Community()
	{
	}

	public Community(int name)
	{
		Name = name;
	}

	public int Id { get; private set; }
	public int Name { get; private set; }

	public ICollection<Publication> Publications { get; private set; }
}