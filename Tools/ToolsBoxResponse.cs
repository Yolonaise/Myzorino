using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
	public class ToolsBoxResponse
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static HttpResponseMessage OK(object json, [CallerMemberName] string callerName = "")
		{
			var jsonString = NewtonsoftJsonSerializer.Default.Serialize(json);
			var res = new HttpResponseMessage(HttpStatusCode.OK);
			res.Content = new StringContent(jsonString);

			logger.Debug("[" + callerName + "]" + jsonString);
			return res;
		}
	}
}
