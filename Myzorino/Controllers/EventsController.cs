using DataProvider;
using DataProvider.Helper;
using DataProvider.Models;
using Myzorino.Models.RequestModels;
using Myzorino.Models.ResponseModels;
using Myzorino.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tools;

namespace Myzorino.Controllers
{
	[RegistrationRequired]
	[RoutePrefix("api/Events")]
	public class EventsController : ApiController
	{
		[HttpPost]
		[Route("Add")]
		public HttpResponseMessage AddEvents(AddEventRequestModel request)
		{
			if (request == null)
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Request is empty.",
					Status = HttpStatusCode.BadRequest
				});

			int creatorIdTemp;
			if (!int.TryParse(request.CreatorId, out creatorIdTemp))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Creator id is wrong.",
					Status = HttpStatusCode.BadRequest
				});

			var dbPlayerResponse = AccountHelper.ExistsWithId(creatorIdTemp);

			if(dbPlayerResponse == null ||
				dbPlayerResponse.Status != HttpStatusCode.OK)
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "No user found with this Id",
					Status = HttpStatusCode.BadRequest
				});

			DateTime start;
			if(!DateTime.TryParse(request.StartDate, out start))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Start date is wrong.",
					Status = HttpStatusCode.BadRequest
				});

			DateTime end = start;
			if (!string.IsNullOrEmpty(request.EndDate) &&
				!DateTime.TryParse(request.EndDate, out end))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "End date is wrong.",
					Status = HttpStatusCode.BadRequest
				});

			var dbResponse = EventsHelper.AddEvent(new Event
				{
					created_date = DateTime.Now,
					creator_id = creatorIdTemp,
					end_date = end,
					start_date = start
				});

			if(dbResponse == null)
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "An Error has occured.",
					Status = HttpStatusCode.InternalServerError
				});

			switch (dbResponse.Status)
			{
				case HttpStatusCode.OK:
					return ToolsBoxResponse.OK(new BasicResponseModel
							{
								Message = "OK",
								Status = HttpStatusCode.OK
							});
				default:
					return ToolsBoxResponse.OK(new BasicResponseModel
					{
						Message = dbResponse.Message,
						Status = dbResponse.Status
					});

			}
		}
	
		[HttpPost]
		[Route("GetByUserId")]
		public HttpResponseMessage GetByUserId(GetUserEventsRequest request)
		{
			if(request == null)
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Request is empty",
					Status = HttpStatusCode.BadRequest
				});

			int userIdTemp;
			if (!int.TryParse(request.CreatorId, out userIdTemp))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "User id is not valid.",
					Status = HttpStatusCode.BadRequest
				});

			DateTime start = DateTime.MinValue; 
			if(!string.IsNullOrEmpty(request.StartDate) && 
				!DateTime.TryParse(request.StartDate, out start))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Start date is not valid.",
					Status = HttpStatusCode.BadRequest
				});

			DateTime end = string.IsNullOrEmpty(request.StartDate) ? DateTime.MaxValue : start;
			if (!string.IsNullOrEmpty(request.EndDate) &&
				!DateTime.TryParse(request.EndDate, out end))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "End date is not valid.",
					Status = HttpStatusCode.BadRequest
				});

			request.Number = request.Number < 0 ? int.MaxValue : request.Number;
			
			var dbResponse = EventsHelper.GetEvents(userIdTemp, start, end, request.Number);

			switch (dbResponse.Status)
			{
				case HttpStatusCode.OK:
					return ToolsBoxResponse.OK(new GetEventsResponseModel
					{
						Status = HttpStatusCode.OK,
						Message = "Ok",
						Events = dbResponse.Events
					});
				default:
					return ToolsBoxResponse.OK(new BasicResponseModel
					{
						Message = dbResponse.Message,
						Status = dbResponse.Status
					});
			}
		}
	}
}
