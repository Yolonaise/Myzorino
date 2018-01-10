using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Myzorino.Modules;
 

namespace Myzorino.Controllers
{
    public class CategoryController : ApiController
    {
	    public IEnumerable<string> GetCategories()
	    {
			return new List<string> {
				"Drama",
				"Horror",
				"Biopic"
			};
	    }
    }
}
