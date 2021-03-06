﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using ProgressMonitor.Models.DbModels;
using ProgressMonitor.Models.JsonSerialization;

namespace ProgressMonitor.Services
{
	public class JiraAPIService : IJiraAPIService
	{
		private readonly string _apiUrl;
		private readonly string _encodedCredentials;
		private readonly ProgressMonitorDbContext _context;

		public JiraAPIService()
		{
			_context = new ProgressMonitorDbContext();
			_apiUrl = ConfigurationManager.AppSettings["JiraURL"];
			_encodedCredentials = EncodeCredentials(
				ConfigurationManager.AppSettings["JiraUsername"],
				ConfigurationManager.AppSettings["JiraPassword"]);
		}

		public IReadOnlyCollection<JiraProject> GetAllProjects()
		{
			string data = SendRequest("project");
			return DeserializeJsonString<IReadOnlyCollection<JiraProject>>(data);
		}

		public IReadOnlyDictionary<JiraProject, bool> GetProjectAccessForUser(string userId)
		{
			IReadOnlyCollection<JiraProject> projects = GetAllProjects();
			Dictionary<JiraProject, bool> projectAccess = new Dictionary<JiraProject, bool>();
			foreach (JiraProject jiraProject in projects)
			{
				projectAccess[jiraProject] = _context.ProjectSet.Any(ps => ps.JiraId == jiraProject.Id)
					&& _context.ProjectSet.First(ps => ps.JiraId == jiraProject.Id).UsersWithAccess
						.Any(u => u.Id == userId);
			}
			return projectAccess;
		}

		public JiraProject GetProject(long id)
		{
			string data = SendRequest("project", id.ToString());
			return DeserializeJsonString<JiraProject>(data);
		}

		public IReadOnlyCollection<JiraIssue> GetIssuesByVersion(long versionId)
		{
			string data = SendSearchRequest("search?jql=FixVersion=" + versionId);
			return DeserializeJsonString<JiraIssuesSearchResult>(data).Issues;
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
			return GetResponse(data, method, url);
		}

		private string SendSearchRequest(string jql, string data = null, string method = "GET")
		{
			string url = CreateSearchUrl(jql);
			return GetResponse(data, method, url);
		}

		private string GetResponse(string data, string method, string url)
		{
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

		private string CreateSearchUrl(string jql)
		{
			return $"{_apiUrl}{jql}";
		}
	}
}