using System.Net.Http.Headers;
using System.Text.Json;

namespace SocialTopicsSentimentsAnalyzer.Communities.Data.Extraction;

public class RedditCommunityDataService
{
	private static readonly HttpClient client = new HttpClient();
	private static string Link;

	public static async Task<Root> GetCommunityData(string nameCommunity)
	{
		Link = $"https://www.reddit.com/r/{nameCommunity}/top.json";

		return await DeserializeResultJson(await MakeRequests());
	}

	private static async Task<Stream> MakeRequests()
	{
		client.DefaultRequestHeaders.Accept.Clear();
		client.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/json"));

		var receivedStream = await client.GetStreamAsync(Link);

		return receivedStream;
	}

	private static async Task<Root> DeserializeResultJson(Stream streamDataJson)
	{
		var communityData = await JsonSerializer.DeserializeAsync<Root>(streamDataJson,
			new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		foreach (var comm in communityData!.data.children)
		{
			Console.WriteLine(comm.data.subreddit);
			Console.WriteLine(comm.data.title);
			Console.WriteLine(comm.data.selftext);
			Console.WriteLine(comm.data.permalink);
			Console.WriteLine(comm.data.score);
			Console.WriteLine(comm.data.created_utc);
			Console.WriteLine("----------------------------------------");
		}
		return communityData;
	}
}