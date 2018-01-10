using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace Myzorino.Models.ResponseModels
{
	public class BasicResponseModel
	{
		[JsonProperty(PropertyName = "status")]
		public HttpStatusCode Status;

		[JsonProperty(PropertyName = "message")]
		public string Message;
	}
}