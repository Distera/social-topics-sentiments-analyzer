using CsvHelper.Configuration.Attributes;
using Microsoft.ML.Data;

namespace SocialTopicsSentimentsAnalyzer.SentimentAnalysis;

public class SentimentDataTemp
{
	[Index(0)] [ColumnName("Sentiment")] public int Sentiment { get; set; }
	[Index(5)] [ColumnName("Text")] public string Text { get; set; }
}

public class SentimentData
{
	[LoadColumn(0)]
	public string SentimentText;

	[LoadColumn(1), ColumnName("Label")]
	public bool Sentiment;
}

public class SentimentPrediction : SentimentData
{

	[ColumnName("PredictedLabel")]
	public bool Prediction { get; set; }

	public float Probability { get; set; }

	public float Score { get; set; }
}