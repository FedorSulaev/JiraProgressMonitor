using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using ProgressMonitor.Models.JsonSerialization;

namespace ProgressMonitor.Services
{
	public class JiraAPIService : IJiraAPIService
	{
		private readonly string _apiUrl;
		private readonly string _encodedCredentials;

		public JiraAPIService()
		{
			_apiUrl = ConfigurationManager.AppSettings["JiraURL"];
			_encodedCredentials = EncodeCredentials(
				ConfigurationManager.AppSettings["JiraUsername"],
				ConfigurationManager.AppSettings["JiraPassword"]);
		}

		public IReadOnlyCollection<Project> GetAllProjects()
		{
			string data = SendRequest("project");
			return DeserializeJsonString<IReadOnlyCollection<Project>>(data);
		}

		public Project GetProject(long id)
		{
			string data = SendRequest("project", id.ToString());
			return DeserializeJsonString<Project>(data);
		}

		private T DeserializeJsonString<T>(string data)
		{
			JsonSerializer serializer = new JsonSerializer();
			using (JsonReader reader = new JsonTextReader(new StringReader(data)))
			{
				return serializer.Deserialize<T>(reader);
			}
		}

		private string EncodeCredentials(string username, string password)
		{
			string mergedCredentials = $"{username}:{password}";
			byte[] byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
			return Convert.ToBase64String(byteCredentials);
		}

		private string SendRequest(string category, string argument = null, string data = null, 
			string method = "GET")
		{
			string url = CreateUrl(category, argument);
			HttpWebRequest request = CreateWebRequest(data, method, url);
			HttpWebResponse response = request.GetResponse() as HttpWebResponse;
			return ReadResponse(response);
		}

		private static string ReadResponse(HttpWebResponse response)
		{
			Stream responseStream = response.GetResponseStream();
			if (responseStream == null)
			{
				return string.Empty;
			}
			using (StreamReader reader = new StreamReader(responseStream))
			{
				return reader.ReadToEnd();
			}
		}

		private HttpWebRequest CreateWebRequest(string data, string method, string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.ContentType = "application/json";
			request.Method = method;
			if (data != null)
			{
				using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
				{
					writer.Write(data);
				}
			}
			request.Headers.Add("Authorization", "Basic " + _encodedCredentials);
			return request;
		}

		private string CreateUrl(string category, string argument)
		{
			string url = $"{_apiUrl}{category}/";
			if (argument != null)
			{
				url = $"{url}{argument}/";
			}
			return url;
		}
	}
}