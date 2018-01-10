using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
	public class ToolsBoxResponse
	{
		//public static HttpResponseMessage BadRequest(object json)
		//{
		//	var res = new HttpResponseMessage(HttpStatusCode.BadRequest);
		//	res.Content = new StringContent(NewtonsoftJsonSerializer.Default.Serialize(json));

		//	return res;
		//}

		//public static HttpResponseMessage InternalError(object json)
		//{
		//	var res = new HttpResponseMessage(HttpStatusCode.InternalServerError);
		//	res.Content = new StringContent(NewtonsoftJsonSerializer.Default.Serialize(json));

		//	return res;
		//}

		//public static HttpResponseMessage Forbidden(object json)
		//{
		//	var res = new HttpResponseMessage(HttpStatusCode.BadRequest);
		//	res.Content = new StringContent(NewtonsoftJsonSerializer.Default.Serialize(json));

		//	return res;
		//}

		public static HttpResponseMessage OK(object json)
		{
			var res = new HttpResponseMessage(HttpStatusCode.OK);
			res.Content = new StringContent(NewtonsoftJsonSerializer.Default.Serialize(json));

			return res;
		}
	}
}
