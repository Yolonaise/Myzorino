using System;
using RestSharp;
using Tools;

namespace MovieApi
{
	public class MovieClient : RestClient
	{
		#region fields

		private static string apikey = "8a831a27";
		private static string apiurl = "http://www.omdbapi.com/";

		#endregion

		#region Constructors

		public MovieClient() : base(apiurl)
		{
		}

		#endregion

		public ResponseModel GetMovieInfo(string titre)
		{
			try
			{
				RestRequest request = new RestRequest(string.Empty, Method.GET)
				{
					RequestFormat = DataFormat.Json,
					JsonSerializer = NewtonsoftJsonSerializer.Default
				};
				request.AddParameter("apikey", apikey);
				request.AddParameter("t", titre);

				var response = Execute<ResponseModel>(request);
				return response.Data;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
