using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProgressMonitor.Models.JsonSerialization
{
	public class JiraProject
	{
		[JsonProperty("id")]
		public long Id  { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("avatarUrls")]
		public IReadOnlyDictionary<string, string> AvatarUrls { get; set; } 

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("lead")]
		public JiraUser Lead { get; set; }

		[JsonProperty("versions")]
		public IReadOnlyCollection<JiraVersion> Versions { get; set; }
	}
}