using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.ClientResponse
{
	public class BasicDataResponse
	{
		public string Message { get; set; }
		public HttpStatusCode Status { get; set; }
	}
}
