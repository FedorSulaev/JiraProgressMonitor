using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProgressMonitor.Models.JsonSerialization
{
	public class JiraUser
	{
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("avatarUrls")]
		public IReadOnlyDictionary<string, string> AvatarUrls { get; set; }

		[JsonProperty("displayName")]
		public string DisplayName { get; set; }
	}
}