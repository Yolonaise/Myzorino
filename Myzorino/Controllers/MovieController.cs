using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web.Http;
using MovieApi;
using Tools;

namespace Myzorino.Controllers
{
	[RoutePrefix("api/movie")]
	public class MovieController : ApiController
	{
		[HttpGet]
		[Route("")]
		public HttpResponseMessage Get()
		{
			var Movies = new List<ResponseModel>();

			try
			{
				WebClient wc = new WebClient();
				wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
				var v = wc.OpenRead(new Uri("http://www.voirfilms.ws/film-en-streaming"));
				List<ResponseModel> responses = new List<ResponseModel>();

				if (v != null)
				{
					MovieClient client = new MovieClient();

					string expression = "<a href=\"\\/(?<link>.+)\" title=\"film (?<film>.+)\">";
					TextReader tr = new StreamReader(v);
					string content = tr.ReadToEnd();

					Regex regex = new Regex(expression, RegexOptions.IgnoreCase);
					MatchCollection mc = regex.Matches(content);

					foreach (Match match in mc)
					{
						responses.Add(client.GetMovieInfo(match.Groups["film"].Value));
						var res = client.GetMovieInfo(match.Groups["film"].Value);
						if (res != null)
						{
							res.DownloadbleUrl = match.Groups["link"].Value.Replace(".htm", string.Empty);
							Movies.Add(res);
						}
					}
				}
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK);
				var result = NewtonsoftJsonSerializer.Default.Serialize(Movies);
				hrm.Content = new StringContent(result);
				hrm.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

				return hrm;
			}
			catch (Exception e)
			{
				HttpResponseMessage hrmKo = new HttpResponseMessage(HttpStatusCode.OK);
				hrmKo.Content = new StringContent(e.Message);
				return hrmKo;
			}
		}

		[HttpGet]
		[Route("{link}")]
		public HttpResponseMessage GetOptions(string link)
		{
			//WebClient wc = new WebClient();
			//wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
			//var v = wc.OpenRead(new Uri("http://www.voirfilms.ws/" + link + ".htm"));

			//if (v != null)
			//{
			//	TextReader tr = new StreamReader(v);
			//	string content = tr.ReadToEnd();
			//}

			//var response = new HttpResponseMessage();
			//response.Content = new StringContent("<iframe name=\"filmPlayer\" style=\"background:#000;\" id=\"filmPlayer\" width=\"703\" height=\"400\" frameborder=\"0\" scrolling=\"no\" allowfullscreen=\"\" src=\"http://www.voirfilms.ws/video.php?p=2&c=WWtSb2JWbFtVUa2xXZaWtKcllucFJQUT09&sig=902\"></iframe>");
			//response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
			//return response;
			HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK);
			hrm.Content = new StringContent("Hello salut sale batard");
			return hrm;
		}
	}
}
