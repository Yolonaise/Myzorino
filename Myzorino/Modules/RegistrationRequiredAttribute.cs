using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myzorino.Modules
{
	[AttributeUsage(AttributeTargets.Class)]
	public class RegistrationRequiredAttribute : Attribute
	{
		public const string Name = "Myzorino.Modules.RegistrationRequired";
	}
}