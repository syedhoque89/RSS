using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace RSS.Dto
{
	public partial class UkNewsDto
	{
		[J("?xml")] public Xml Xml { get; set; }
		[J("?xml-stylesheet")] public string XmlStylesheet { get; set; }
		[J("rss")] public Rss Rss { get; set; }
	}

	public partial class Rss
	{
		[J("@xmlns:dc")] public Uri XmlnsDc { get; set; }
		[J("@xmlns:content")] public Uri XmlnsContent { get; set; }
		[J("@xmlns:atom")] public Uri XmlnsAtom { get; set; }
		[J("@version")] public string Version { get; set; }
		[J("@xmlns:media")] public Uri XmlnsMedia { get; set; }
		[J("channel")] public Channel Channel { get; set; }
	}

	public partial class Channel
	{
		[J("title")] public Content Title { get; set; }
		[J("description")] public Content Description { get; set; }
		[J("link")] public Uri Link { get; set; }
		[J("image")] public Image Image { get; set; }
		[J("generator")] public string Generator { get; set; }
		[J("lastBuildDate")] public string LastBuildDate { get; set; }
		[J("copyright")] public Content Content { get; set; }
		[J("language")] public Content Language { get; set; }

		[J("ttl")]
		[JsonConverter(typeof(FluffyParseStringConverter))]
		public long Ttl { get; set; }

		[J("item")] public List<NewsItem> NewsItem { get; set; }
	}

	public partial class Content
	{
		[J("#cdata-section")] public string Text { get; set; }
	}

	public partial class Image
	{
		[J("url")] public Uri Url { get; set; }
		[J("title")] public string Title { get; set; }
		[J("link")] public Uri Link { get; set; }
	}

	public partial class NewsItem
	{
		[J("title")]           public Content Title { get; set; }
		[J("description")]     public Content Description { get; set; }
		[J("link")]            public Uri Link { get; set; }
		[J("guid")]            public GuidClass Guid { get; set; }
		[J("pubDate")]         public string PubDate { get; set; }
		[J("media:thumbnail")] public MediaThumbnail MediaThumbnail { get; set; }
	}

	public partial class GuidClass
	{
		[J("@isPermaLink")]
		[JsonConverter(typeof(PurpleParseStringConverter))]
		public bool IsPermaLink { get; set; }

		[J("#text")]                                                           public Uri Text { get; set; }
	}

	public partial class MediaThumbnail
	{
		[J("@width")]
		[JsonConverter(typeof(FluffyParseStringConverter))]
		public long Width { get; set; }

		[J("@height")]
		[JsonConverter(typeof(FluffyParseStringConverter))]
		public long Height { get; set; }

		[J("@url")] public string Url { get; set; }
	}

	public partial class Xml
	{
		[J("@version")]  public string Version { get; set; }
		[J("@encoding")] public string Encoding { get; set; }
	}

	public partial class UkNewsDto
	{
		public static UkNewsDto FromJson(string json) =>
			JsonConvert.DeserializeObject<UkNewsDto>(json, Converter.Settings);
	}

	public static class Serialize
	{
		public static string ToJson(this UkNewsDto self) =>
			JsonConvert.SerializeObject(self, Converter.Settings);
	}

	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}

	internal class PurpleParseStringConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			bool b;
			if (Boolean.TryParse(value, out b))
			{
				return b;
			}

			throw new Exception("Cannot unmarshal type bool");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}

			var value = (bool) untypedValue;
			var boolString = value ? "true" : "false";
			serializer.Serialize(writer, boolString);
			return;
		}

		public static readonly PurpleParseStringConverter Singleton = new PurpleParseStringConverter();
	}

	internal class FluffyParseStringConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			long l;
			if (Int64.TryParse(value, out l))
			{
				return l;
			}

			throw new Exception("Cannot unmarshal type long");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}

			var value = (long) untypedValue;
			serializer.Serialize(writer, value.ToString());
			return;
		}

		public static readonly FluffyParseStringConverter Singleton = new FluffyParseStringConverter();
	}
}