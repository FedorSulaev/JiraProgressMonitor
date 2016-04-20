using Newtonsoft.Json;

namespace ProgressMonitor.Models.JsonSerialization
{
	public class Issue
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }
	}
}