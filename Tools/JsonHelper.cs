﻿using System.IO;
using Newtonsoft.Json;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Tools
{
	public class NewtonsoftJsonSerializer : ISerializer
	{
		private Newtonsoft.Json.JsonSerializer serializer;

		public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
		{
			this.serializer = serializer;
		}

		public string ContentType
		{
			get { return "application/json"; } // Probably used for Serialization?
			set { }
		}

		public string DateFormat { get; set; }

		public string Namespace { get; set; }

		public string RootElement { get; set; }

		public string Serialize(object obj)
		{
			using (var stringWriter = new StringWriter())
			{
				using (var jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					serializer.Serialize(jsonTextWriter, obj);

					return stringWriter.ToString();
				}
			}
		}

		public static NewtonsoftJsonSerializer Default
		{
			get
			{
				return new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer()
				{
					NullValueHandling = NullValueHandling.Ignore,
					DateFormatString = "yyyy-MM-dd hh:mm:ss"
				});
			}
		}
	}

	public class NewtonsoftJsonDeserializer : IDeserializer
	{
		private Newtonsoft.Json.JsonSerializer serializer;

		public NewtonsoftJsonDeserializer(Newtonsoft.Json.JsonSerializer serializer)
		{
			this.serializer = serializer;
		}

		public string ContentType
		{
			get { return "application/json"; } // Probably used for Serialization?
			set { }
		}

		public string DateFormat { get; set; }

		public string Namespace { get; set; }

		public string RootElement { get; set; }


		public static NewtonsoftJsonDeserializer Default
		{
			get
			{
				return new NewtonsoftJsonDeserializer(new Newtonsoft.Json.JsonSerializer()
				{
					NullValueHandling = NullValueHandling.Ignore,
					DateFormatString = "yyyy-MM-dd hh:mm:ss"
				});
			}
		}


		public T Deserialize<T>(RestSharp.IRestResponse response)
		{
			var content = response.Content;

			using (var stringReader = new StringReader(content))
			{
				using (var jsonTextReader = new JsonTextReader(stringReader))
				{
					return serializer.Deserialize<T>(jsonTextReader);
				}
			}
		}
	}
}
