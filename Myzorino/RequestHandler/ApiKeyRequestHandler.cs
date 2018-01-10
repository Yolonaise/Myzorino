using DataProvider;
using DataProvider.Models;
using Myzorino.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Tools;

namespace Myzorino.RequestHandler
{
	public class ApiKeyRequestHandler : DelegatingHandler
	{
		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			//TODO : implement HMAC for apiKey validation!
			try
			{
				IEnumerable<string> values;
				if (!request.Headers.TryGetValues("apikey", out values)||
					values.Count() == 0 ||
					string.IsNullOrEmpty(values.ToList()[0]))

					return ToolsBoxResponse.OK(new BasicResponseModel
					{
						Message = "Api key not allowed",
						Status = HttpStatusCode.Forbidden
					});

				var dbResponse = ApiKeysHelper.Exists(new ApiKey { Value = values.ToList()[0] });

				switch (dbResponse.Status)
				{
					case HttpStatusCode.Forbidden:
						return ToolsBoxResponse.OK(new BasicResponseModel
						{
							Message = dbResponse.Message,
							Status = HttpStatusCode.Forbidden
						});
					case HttpStatusCode.InternalServerError:
						return ToolsBoxResponse.OK(new BasicResponseModel
						{
							Message = dbResponse.Message,
							Status = HttpStatusCode.InternalServerError
						});
				}
			}
			catch (Exception)
			{
				return ToolsBoxResponse.OK(new BasicResponseModel
				{
					Message = "Api key not provided",
					Status = HttpStatusCode.Forbidden
				});
			}

			var response = await base.SendAsync(request, cancellationToken);
			return response;
		}
	}
}