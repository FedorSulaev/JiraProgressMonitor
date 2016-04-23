using Newtonsoft.Json;

namespace ProgressMonitor.Models.JsonSerialization
{
	public class JiraIssue
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }
	}
}