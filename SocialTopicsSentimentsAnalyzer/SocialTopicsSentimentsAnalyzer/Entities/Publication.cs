namespace SocialTopicsSentimentsAnalyzer.Entities;

public enum SentimentType
{
	Negative,
	Neutral,
	Positive,
}

public class Publication
{
	private Publication()
	{
	}

	public Publication(Community community, string title, string permalink, int score, DateTime created,
		SentimentType sentiment)
	{
		Title = title;
		Permalink = permalink;
		Score = score;
		Created = created;
		Sentiment = sentiment;
		Community = community;
	}

	// ReSharper disable once UnusedAutoPropertyAccessor.Local
	public int Id { get; private set; }
	public Community Community { get; private set; }
	public string Title { get; private set; }
	public string Permalink { get; private set; }
	public int Score { get; private set; }
	public DateTime Created { get; private set; }

	public SentimentType Sentiment { get; private set; }
}