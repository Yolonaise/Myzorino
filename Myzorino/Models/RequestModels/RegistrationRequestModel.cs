using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myzorino.Models.RequestModels
{
	public class RegistrationRequestModel
	{
		public string username { get; set; }
		public string password { get; set; }
		public string email { get; set; }
	}
}