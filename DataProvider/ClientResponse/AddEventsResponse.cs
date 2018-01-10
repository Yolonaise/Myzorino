using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.ClientResponse
{
	public class AddEventsResponse : BasicDataResponse
	{
		public List<Event> ErrorEvents { get; set; }
	}
}
