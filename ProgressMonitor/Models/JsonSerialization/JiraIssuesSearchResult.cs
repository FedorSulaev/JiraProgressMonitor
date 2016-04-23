using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProgressMonitor.Models.JsonSerialization
{
	public class JiraIssuesSearchResult
	{
		[JsonProperty("issues")]
		public IReadOnlyCollection<JiraIssue> Issues { get; set; }
	}
}