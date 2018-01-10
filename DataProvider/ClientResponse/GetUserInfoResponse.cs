using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DataProvider.Models;

namespace DataProvider.ClientResponse
{
	public class AccountResponse : BasicDataResponse
	{
		public Account Account { get; set; }
	}
}
