namespace SocialTopicsSentimentsAnalyzer.Communities.Data;

using System.Text.Json.Serialization;

public class Child
{
	public Data data { get; set; }
}

public class Data
{
	public List<Child> children { get; set; }
	public string subreddit { get; set; }
	public string selftext { get; set; }
	public string title { get; set; }
	public int score { get; set; }
	public double created { get; set; }
	public List<object> treatment_tags { get; set; }
	public string id { get; set; }
	public string author { get; set; }
	public int num_comments { get; set; }
	public string permalink { get; set; }
	public int subreddit_subscribers { get; set; }
	public double created_utc { get; set; }
}

public class Root
{
	public string kind { get; set; }
	public Data data { get; set; }
}