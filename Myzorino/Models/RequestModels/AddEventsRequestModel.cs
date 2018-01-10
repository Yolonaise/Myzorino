using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myzorino.Models.RequestModels
{
	public class AddEventsRequestModel
	{
		[JsonProperty(PropertyName = "events")]
		public List<AddEventRequestModel> Events;
	}
}