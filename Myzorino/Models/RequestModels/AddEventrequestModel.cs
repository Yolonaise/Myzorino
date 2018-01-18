using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myzorino.Models.RequestModels
{
	public class AddEventRequestModel
	{
		[JsonProperty(PropertyName = "creatorId")]
		public int CreatorId { get; set; }

		[JsonProperty(PropertyName = "startDate")]
		public string StartDate { get; set; }

		[JsonProperty(PropertyName = "endDate")]
		public string EndDate { get; set; }

		[JsonProperty(PropertyName = "titre")]
		public string Titre { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description{ get; set; }
	}
}