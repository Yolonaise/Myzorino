using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MovieApi
{
	public class ResponseModel
	{
		[JsonProperty(PropertyName = "Title")]
		public string Title { get; set; }
		
		[JsonProperty(PropertyName = "Year")]
		public string Year { get; set; }
		
		[JsonProperty(PropertyName = "Runtime")]
		public string Runtime { get; set; }

		[JsonProperty(PropertyName = "Genre")]
		public string Genre { get; set; }

		[JsonProperty(PropertyName = "Poster")]
		public string Poster { get; set; }

		public string DownloadbleUrl { get; set; }
	}
}
