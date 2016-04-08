using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProgressMonitor.Models.JsonSerialization
{
	public class Project
	{
		[JsonProperty("self")]
		public string Self { get; set; }

		[JsonProperty("id")]
		public long Id  { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("avatarUrls")]
		public Dictionary<string, string> AvatarUrls { get; set; } 
	}
}