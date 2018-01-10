using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DataProvider;
using System.Net;

namespace Myzorino.Provider.Security
{
	public class TokenProvider
	{
		//Infinite token : eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFybmF1ZCIsImV4cGlyeURhdGUiOiI5OTk5LTEyLTMxVDIzOjU5OjU5Ljk5OTk5OTkifQ.c_Ys0GCk4E9JH1hCQqr7OcdI8I8DE_Yp6XnmreqMEy8
		public enum TokenStatus
		{
			WrongToken = 0,
			Expired = 1,
			Ok = 2
		}

		private const int secondsTokenValidity = 3600;

		public static string Generate(string username, string password)
		{
			var key = " 78AsWaghzeaETtIY456138ZEoloNaISe789461657zaea8AE4A8";

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
			var header = new JwtHeader(credentials);
			var payload = new JwtPayload { { "username", username }, { "expiryDate", DateTime.Now.AddSeconds(secondsTokenValidity).ToString() } };
			var secToken = new JwtSecurityToken(header, payload);
			var handler = new JwtSecurityTokenHandler();

			return handler.WriteToken(secToken);
		}

		public static TokenStatus CheckToken(string token)
		{
			try
			{
				var handler = new JwtSecurityTokenHandler();
				var tokenObj = handler.ReadJwtToken(token);

				if (!tokenObj.Payload.ContainsKey("username") ||
					!tokenObj.Payload.ContainsKey("expiryDate"))
					return TokenStatus.WrongToken;

				object stringTemp;
				if (!tokenObj.Payload.TryGetValue("username", out stringTemp))
					return TokenStatus.WrongToken;

				if (AccountHelper.ExistsWithUsername(stringTemp as string).Status != HttpStatusCode.OK)
					return TokenStatus.WrongToken;

				if (!tokenObj.Payload.TryGetValue("expiryDate", out stringTemp))
					return TokenStatus.WrongToken;

				try
				{
					var date = Convert.ToDateTime(stringTemp);
					if (date < DateTime.Now)
						return TokenStatus.Expired;
				}
				catch (Exception)
				{
					return TokenStatus.WrongToken;
				}
			}
			catch (Exception)
			{
				return TokenStatus.WrongToken;
			}
			

			return TokenStatus.Ok;
		}
	}
}