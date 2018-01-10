using DataProvider.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myzorino.Models.ResponseModels
{
	public class GetEventsResponseModel : BasicResponseModel
	{
		[JsonProperty(PropertyName = "events")]
		public List<Event> Events { get; set; }
	}
}