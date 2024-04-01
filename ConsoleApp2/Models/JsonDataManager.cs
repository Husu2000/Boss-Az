using Newtonsoft.Json;

namespace ConsoleApp2.Models;

public class JsonDataManager
{
	private static string workersFilePath = "..\\..\\..\\JsonFiles\\workers.json";
	private static string employersFilePath = "..\\..\\..\\JsonFiles\\employers.json";
	private static string LogFilePath = "..\\..\\..\\JsonFiles\\log.json";


	public static List<Worker>? LoadWorkerData()
	{
		try
		{
			if (File.Exists(workersFilePath))
			{
				string json2 = File.ReadAllText(workersFilePath);
				return JsonConvert.DeserializeObject<List<Worker>>(json2);
			}

		}
		catch (Exception e)
		{
			Console.WriteLine($"\n\n{e.Message}\n\n");
			return null;
		}
		return null;
	}
	public static List<Employer>? LoadEmployerData()
	{
		try
		{
			if (File.Exists(employersFilePath))
			{
				string json1 = File.ReadAllText(employersFilePath);
				return JsonConvert.DeserializeObject<List<Employer>>(json1);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"\n\n{e.Message}\n\n");
			return null;
		}
		return null;

	}
	public static void SaveUserData(UserData userData)
	{
		try
		{
			JsonSerializerSettings settings = new JsonSerializerSettings() { Formatting = Formatting.Indented };

			string json1 = JsonConvert.SerializeObject(userData.Workers, settings);
			File.WriteAllText(workersFilePath, json1);

			string json2 = JsonConvert.SerializeObject(userData.Employers, settings);
			File.WriteAllText(employersFilePath, json2);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in Save : {ex.Message}\n");
		}
	}


	public static void AddLogEntry(string message)
	{
		List<LogEntry> logEntries = new List<LogEntry>();
		JsonSerializerSettings settings = new JsonSerializerSettings() { Formatting = Formatting.Indented };

		if (File.Exists(LogFilePath))
		{
			string json = File.ReadAllText(LogFilePath);
			logEntries = JsonConvert.DeserializeObject<List<LogEntry>>(json, settings);
		}

		logEntries = logEntries ?? new List<LogEntry>();
		logEntries.Add(new LogEntry
		{
			Timestamp = DateTime.Now,
			Message = message
		});

		string updatedJson = JsonConvert.SerializeObject(logEntries, settings);
		File.WriteAllText(LogFilePath, updatedJson);
	}
}
