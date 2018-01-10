using DataProvider.Models;
using System.Collections.Generic;

namespace DataProvider.ClientResponse
{
	public class GetEventsResponse : BasicDataResponse
	{
		public List<Event> Events { get; set; }
	}
}
