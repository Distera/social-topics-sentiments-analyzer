using System.Globalization;
using Microsoft.ML;
using CsvHelper;
using CsvHelper.Configuration;

namespace SocialTopicsSentimentsAnalyzer.SentimentAnalysis;

public class DataAnalyzer
{
	public static void TrainTheModel()
	{
		var dataFile = "training.1600000.processed.noemoticon.csv";
		var mlContext = new MLContext();

		var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false };
		using var reader = new StreamReader(dataFile);
		using var csv = new CsvReader(reader, configuration);
		var records = csv.GetRecords<SentimentDataTemp>().ToList();

		var dataView = mlContext.Data.LoadFromEnumerable(records.Select(record => new SentimentData
			{ Sentiment = record.Sentiment > 0, SentimentText = record.Text }));

		var splitDataView = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

		// Data process configuration with pipeline data transformations
		var estimator  = mlContext.Transforms.Text.FeaturizeText(
			outputColumnName: "Features",
			inputColumnName: nameof(SentimentData.SentimentText)
		).Append(
			mlContext.BinaryClassification.Trainers
				.LbfgsLogisticRegression(labelColumnName: "Label", featureColumnName: "Features")
		);

		// Train the model fitting to the DataSet
		Console.WriteLine("Train the Model ...");
		ITransformer model = estimator.Fit(splitDataView.TrainSet);
		Console.WriteLine("End of training.");

		// Evaluate the model and show accuracy stats
		var predictions = model.Transform(splitDataView.TestSet);
		var metrics = mlContext.BinaryClassification.Evaluate(predictions);

		Console.WriteLine();
		Console.WriteLine("--------------------------------");
		Console.WriteLine("Model quality metrics evaluation:");
		Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
		Console.WriteLine($"Auc: {metrics.AreaUnderRocCurve:P2}");
		Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
		Console.WriteLine("--------------------------------");

		// Save Trained Model
		mlContext.Model.Save(model, splitDataView.TrainSet.Schema, "model.zip");
	}

	public static void UseModelWithSingleItem()
	{
		var mlContext = new MLContext();

		// Load trained model
		var model = mlContext.Model.Load("model.zip", out _);

		var predictionFunction =
			mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);

		var sampleStatement = new SentimentData { SentimentText = "This was a very bad steak" };

		var resultPrediction = predictionFunction.Predict(sampleStatement);

		Console.WriteLine();
		Console.WriteLine("Prediction Test of model with a single sample and test dataset:");
		Console.WriteLine($"Sentiment: {resultPrediction.SentimentText}");
		Console.WriteLine($"Prediction: {(Convert.ToBoolean(resultPrediction.Prediction) ? "Positive" : "Negative")}");
		Console.WriteLine($"Probability: {resultPrediction.Probability}");
		Console.WriteLine("End of Predictions ...");
		Console.WriteLine();
	}
}