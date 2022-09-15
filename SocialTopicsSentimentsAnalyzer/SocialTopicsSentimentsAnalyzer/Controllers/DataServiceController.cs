using Microsoft.AspNetCore.Mvc;
using SocialTopicsSentimentsAnalyzer.Communities.Data.Extraction;
using SocialTopicsSentimentsAnalyzer.Entities;

namespace SocialTopicsSentimentsAnalyzer.Controllers;

[ApiController]
[Route("[controller]")]
public class DataServiceController: ControllerBase
{
	[HttpGet(Name = "GetDataService")]
	public void Get()
	{
		RedditCommunityDataService.GetCommunityData("Eyebleach");
	}
}