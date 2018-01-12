using DataProvider;
using Myzorino.Models;
using Myzorino.Models.RequestModels;
using Myzorino.Models.ResponseModels;
using Myzorino.Provider.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tools;
	
namespace Myzorino.Controllers
{
	[RoutePrefix("api/Registration")]
    public class RegistrationController : ApiController
    {
		[HttpPost]
		[Route("CreateAccount")]
		public HttpResponseMessage CreateAccount(RegistrationRequestModel account)
		{
			if (account == null)
				return ToolsBoxResponse.OK(new BasicResponseModel
				{ 
					Message = "Request is empty",
					Status = HttpStatusCode.BadRequest
				});

			if (!ToolsBox.IsEmailValid(account.email))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "email not valid",
					Status = HttpStatusCode.BadRequest
				});

			if (string.IsNullOrEmpty(account.password))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "No password provided",
					Status = HttpStatusCode.BadRequest
				});

			if (string.IsNullOrEmpty(account.username))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "No username provided",
					Status = HttpStatusCode.BadRequest
				});

			var response = AccountHelper.AddAccount(new DataProvider.Models.Account
			{
				email = account.email,
				password = account.password,
				username = account.username
			});
			
			if(response == null)
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Account can't be created, retry later.",
					Status = HttpStatusCode.InternalServerError
				});

			switch (response.Status)
			{
				case HttpStatusCode.OK:
					return ToolsBoxResponse.OK(new RegistrationResponseModel
					{
						Token = TokenProvider.Generate(account.username, account.password),
						Id = response.Id,
						Status = HttpStatusCode.OK
					});
				case HttpStatusCode.BadRequest:
					return ToolsBoxResponse.OK(new BasicResponseModel
					{
						Message = response.Message,
						Status = HttpStatusCode.BadRequest
					});
				default:
					return ToolsBoxResponse.OK(new BasicResponseModel
					{
						Message = response.Message,
						Status = HttpStatusCode.InternalServerError
					});
			}
		}

		[HttpPost]
		[Route("Login")]
		public HttpResponseMessage Login(RegistrationRequestModel account)
		{
			if (account == null)
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Request is empty",
					Status = HttpStatusCode.BadRequest
				});

			if (string.IsNullOrEmpty(account.password))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "No password provided",
					Status = HttpStatusCode.BadRequest
				});

			if(string.IsNullOrEmpty(account.username) && string.IsNullOrEmpty(account.email))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "No login provided",
					Status = HttpStatusCode.BadRequest
				});

			if (!string.IsNullOrEmpty(account.username))
				return LoginWithUsername(account);

			if (!string.IsNullOrEmpty(account.email))
				return LoginWithEmail(account);

			return ToolsBoxResponse.OK(new BasicResponseModel
			{
				Message = "Can't connect with current credentials",
				Status = HttpStatusCode.BadRequest
			});
		}

		private HttpResponseMessage LoginWithUsername(RegistrationRequestModel account)
		{
			var dbResponse = AccountHelper.GetUserInfoByUsername(account.username, account.password);

			switch (dbResponse.Status)
			{
				case HttpStatusCode.OK:
					return ToolsBoxResponse.OK(new RegistrationResponseModel
					{
						Id = dbResponse.Account.ID,
						Token = TokenProvider.Generate(account.username, account.password),
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

		private HttpResponseMessage LoginWithEmail(RegistrationRequestModel account)
		{
			if (!ToolsBox.IsEmailValid(account.email))
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "email not valid",
					Status = HttpStatusCode.BadRequest
				});

			var dbResponse = AccountHelper.GetUserInfoByEmail(account.email, account.password);

			switch (dbResponse.Status)
			{
				case HttpStatusCode.OK:
					return ToolsBoxResponse.OK(new RegistrationResponseModel
					{
						Id = dbResponse.Account.ID,
						Token = TokenProvider.Generate(account.username, account.password),
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
    }
}
