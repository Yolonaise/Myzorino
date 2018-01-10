using DataProvider;
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
	[RoutePrefix("api/User")]
	public class UserController : ApiController
	{
		[HttpPost]
		[Route("GetUserInfo/ById/{id}")]
		public HttpResponseMessage GetUserInfoById(string id)
		{
			if (string.IsNullOrEmpty(id))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Request is Empty",
					Status = HttpStatusCode.BadRequest
				});


			int idInt;
			if (!int.TryParse(id, out idInt))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Id is not correct",
					Status = HttpStatusCode.BadRequest
				});

			var dbResponse = AccountHelper.GetUserInfoById(idInt);
			switch (dbResponse.Status)
			{
				case HttpStatusCode.OK:
					return ToolsBoxResponse.OK(new UserInfoResponseModel
					{
						Status = HttpStatusCode.OK,
						Message = "OK",
						Id = dbResponse.Account.ID.ToString(),
						Username = dbResponse.Account.username,
						Email = dbResponse.Account.email
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
		[Route("GetUserInfo/ByUsername/{username}")]
		public HttpResponseMessage GetUserInfoByUsername(string username)
		{
			if (string.IsNullOrEmpty(username))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Request is Empty",
					Status = HttpStatusCode.BadRequest
				});

			var dbResponse = AccountHelper.GetUserInfoByUsername(username);
			switch (dbResponse.Status)
			{
				case HttpStatusCode.OK:
					return ToolsBoxResponse.OK(new UserInfoResponseModel
					{
						Status = HttpStatusCode.OK,
						Message = "OK",
						Id = dbResponse.Account.ID.ToString(),
						Username = dbResponse.Account.username,
						Email = dbResponse.Account.email
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

