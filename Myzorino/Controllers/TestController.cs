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
	[RoutePrefix("api/Registration")]
    public class TestController : ApiController
    {
		[HttpGet]
		[Route("TestConnection")]
		public HttpResponseMessage GetStatus()
		{
			return ToolsBoxResponse.OK(new BasicResponseModel
			{
				Message = "OK",
				Status = HttpStatusCode.OK
			});
		}
    }
}
