using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ProgressMonitor.Models.JsonSerialization
{
	public class Project
	{
		[JsonProperty("id")]
		public long Id  { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("avatarUrls")]
		public ReadOnlyDictionary<string, string> AvatarUrls { get; set; } 

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("lead")]
		public JiraUser Lead { get; set; }

		[JsonProperty("versions")]
		public ReadOnlyCollection<Version> Versions { get; set; }
	}
}