using System;
using System.Text;

namespace ProgressMonitor.Services
{
	public class JiraAPIService : IJiraAPIService
	{
		private string EncodeCredentials(string username, string password)
		{
			string mergedCredentials = $"{username}:{password}";
			byte[] byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
			return Convert.ToBase64String(byteCredentials);
		}
	}
}