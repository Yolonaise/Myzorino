using DataProvider.ClientResponse;
using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
	public class ApiKeysHelper
	{
		public static BasicDataResponse Exists(ApiKey request)
		{
			try
			{
				using (var dbb = new popopopoEntities())
				{
					var keys = dbb.ApiKeys.Where(ak => ak.Value == request.Value);

					if (keys == null)
						return new BasicDataResponse { Message = "Internal Error", Status = HttpStatusCode.InternalServerError };

					if (keys.Count() == 0)
						return new BasicDataResponse { Message = "Api key not registered", Status = HttpStatusCode.Forbidden };

					//TODO : try to remove this condition !
					if (keys.ToList()[0].Value != request.Value)
						return new BasicDataResponse { Message = "Api key not registered", Status = HttpStatusCode.Forbidden };

					return new BasicDataResponse { Message = "OK", Status = HttpStatusCode.OK };
				}
			}
			catch(Exception)
			{
				return new BasicDataResponse { Message = "Can't check api authorization", Status = HttpStatusCode.InternalServerError };
			}
		}
	}
}
