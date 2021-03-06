﻿using Myzorino.RequestHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Myzorino
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
			config.MessageHandlers.Add(new ApiKeyRequestHandler());
			config.MessageHandlers.Add(new SecurityCheckRequestHandler());

            config.MapHttpAttributeRoutes();
			//JSON formatter by default.
			//config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
