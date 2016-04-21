using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProgressMonitor.Models.JsonSerialization
{
	public class IssuesSearchResult
	{
		[JsonProperty("issues")]
		public IReadOnlyCollection<Issue> Issues { get; set; }
	}
}