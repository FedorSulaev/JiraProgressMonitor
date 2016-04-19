using System;
using Newtonsoft.Json;

namespace ProgressMonitor.Models.JsonSerialization
{
	public class Version
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("archived")]
		public bool Archived { get; set; }

		[JsonProperty("released")]
		public bool Released { get; set; }

		[JsonProperty("startDate")]
		public DateTime StartDate { get; set; }

		[JsonProperty("releaseDate")]
		public DateTime ReleaseDate { get; set; }
	}
}