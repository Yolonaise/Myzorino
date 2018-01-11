using DataProvider.ClientResponse;
using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DataProvider.Helper
{
	public class EventsHelper
	{
		public static BasicDataResponse AddEvent(Event e)
		{
			if (e == null)
				return new BasicDataResponse() { Message = "request is null", Status = HttpStatusCode.BadRequest };

			try
			{
				using (var bdd = new popopopoEntities())
				{
					bool alreadyHasId = false;
					do
					{
						e.id = ToolsBox.GetNewId();
						alreadyHasId = bdd.Events.Where(ev => ev.id == e.id).Count() != 0;
					} while (alreadyHasId);

					bdd.Events.Add(e);
					bdd.SaveChanges();

					return new BasicDataResponse { Message = "Done", Status = HttpStatusCode.OK};
				}
			}
			catch(Exception)
			{
				return new BasicDataResponse { Message = "SQLError", Status = HttpStatusCode.InternalServerError };
			}
		}

		public static GetEventsResponse GetEvents(int userId, DateTime start, DateTime end, int nb)
		{
			try
			{
				using (var bdd = new popopopoEntities())
				{
					var results = bdd.Events.Where(e => e.creator_id == userId && 
														e.start_date >= start &&
														e.end_date <= end)
														.OrderBy(e => e.start_date)
														.Take(nb)
														.ToList();

					return new GetEventsResponse { Message = "Done", Status = HttpStatusCode.OK, Events = results };
				}
			}
			catch (Exception)
			{
				return new GetEventsResponse { Message = "SQLError", Status = HttpStatusCode.InternalServerError, Events = new List<Event>()};
			}
		}
	}
}
