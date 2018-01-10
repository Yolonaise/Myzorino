using Myzorino.Models.ResponseModels;
using Myzorino.Modules;
using Myzorino.Provider.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Tools;

namespace Myzorino.RequestHandler
{
	public class SecurityCheckRequestHandler : DelegatingHandler
	{
		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var tokenResponse = ProcessTokenCheck(request);
			if (tokenResponse != null)
				return tokenResponse;

			var response = await base.SendAsync(request, cancellationToken);
			return response;
		}

		private HttpResponseMessage ProcessTokenCheck(HttpRequestMessage request)
		{
			var config = GlobalConfiguration.Configuration;
			var controllerSelector = new DefaultHttpControllerSelector(config);

			var descriptor = controllerSelector.SelectController(request);

			if (System.Attribute.GetCustomAttributes(descriptor.ControllerType)
				.FirstOrDefault(attr => attr.TypeId.ToString().Contains(RegistrationRequiredAttribute.Name)) != null)
			{ 
				IEnumerable<string> values;
				if (!request.Headers.TryGetValues("token", out values) || 
					values.Count() == 0 || 
					string.IsNullOrEmpty(values.ToList()[0]) )
					return ToolsBoxResponse.OK(new BasicResponseModel
					{
						Message = "Token required",
						Status = HttpStatusCode.Forbidden
					});

				var result = TokenProvider.CheckToken(values.ToList()[0]);

				switch (result)
				{
					case TokenProvider.TokenStatus.WrongToken:
						return ToolsBoxResponse.OK(new BasicResponseModel
						{
							Message = "Wrong token",
							Status = HttpStatusCode.Forbidden
						});
					case TokenProvider.TokenStatus.Expired:
						return ToolsBoxResponse.OK(new BasicResponseModel
						{
							Message = "Token expired",
							Status = HttpStatusCode.Forbidden
						});
				}
			}

			return null;
		}
	}
}