using Microsoft.AspNetCore.Mvc;
using SocialTopicsSentimentsAnalyzer.Communities.Data.Extraction;
using SocialTopicsSentimentsAnalyzer.Entities;
using SocialTopicsSentimentsAnalyzer.SentimentAnalysis;

namespace SocialTopicsSentimentsAnalyzer.Controllers;

[ApiController]
[Route("[controller]")]
public class DataServiceController: ControllerBase
{
	[HttpGet(Name = "GetDataService")]
	public void Get()
	{
		// RedditCommunityDataService.GetCommunityData("Eyebleach");
		// DataAnalyzer.TrainTheModel();
		DataAnalyzer.UseModelWithSingleItem();
	}
}