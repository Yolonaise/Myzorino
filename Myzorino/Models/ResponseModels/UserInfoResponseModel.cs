using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myzorino.Models.ResponseModels
{
	public class UserInfoResponseModel : BasicResponseModel
	{
		[JsonProperty(PropertyName = "username")]
		public string Username { get; set; }

		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }

		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }
	}
}