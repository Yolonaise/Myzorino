using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Myzorino.Models.ResponseModels
{
	public class RegistrationResponseModel : BasicResponseModel
	{
		[JsonProperty(PropertyName = "token")]
		public string Token;

		[JsonProperty(PropertyName = "id")]
		public string Id;
	}
}