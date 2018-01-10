using Myzorino.Models.RequestModels;
using Myzorino.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Myzorino.Controllers
{
	[RegistrationRequired]
	[RoutePrefix("api/Events")]
    public class EventsController : ApiController
    {
		[HttpPost]
		[Route("AddRange")]
		public HttpResponseMessage AddEvents(AddEventsRequestModel request)
		{
			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		[HttpPost]
		[Route("Add")]
		public HttpResponseMessage AddEvents(AddEventRequestModel request)
		{
			return new HttpResponseMessage(HttpStatusCode.OK);
		}
    }
}
