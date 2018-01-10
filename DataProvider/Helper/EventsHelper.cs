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

		public static AddEventsResponse AddEvents(List<Event> es)
		{
			if (es == null)
				return new AddEventsResponse { Message = "request is null", Status = HttpStatusCode.BadRequest, ErrorEvents = new List<Event>() };

			try
			{
				List<Event> errors = new List<Event>();
				foreach(var e in es)
				{
					var dbResponse = AddEvent(e);

					if(dbResponse == null || 
						dbResponse.Status != HttpStatusCode.OK)
					{
						errors.Add(e);
					}
				}

				if(errors.Count == es.Count)
					return new AddEventsResponse
					{
						Message = errors.Count == 0 ? "Done" : "Done With Errors",
						Status = HttpStatusCode.BadRequest,
						ErrorEvents = errors
					};

				return new AddEventsResponse { 
					Message = errors.Count == 0 ? "Done" : "Done With Errors",
					Status = HttpStatusCode.OK,
					ErrorEvents = errors
				};
			}
			catch (Exception)
			{
				return new AddEventsResponse { Message = "SQLError", Status = HttpStatusCode.InternalServerError, ErrorEvents = new List<Event>() };
			}
		}
	}
}
