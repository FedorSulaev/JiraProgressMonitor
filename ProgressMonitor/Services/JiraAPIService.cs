using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

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

		private string EncodeCredentials(string username, string password)
		{
			string mergedCredentials = $"{username}:{password}";
			byte[] byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
			return Convert.ToBase64String(byteCredentials);
		}

		private string SendRequest(string category, string argument = null, string data = null, 
			string method = "GET")
		{
			string url = $"{_apiUrl}{category}/";
			if (argument != null)
			{
				url = $"{url}{argument}/";
			}
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
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
			HttpWebResponse response = request.GetResponse() as HttpWebResponse;
			string result = string.Empty;
			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				result = reader.ReadToEnd();
			}
			return result;
		}
	}
}