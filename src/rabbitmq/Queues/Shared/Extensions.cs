using System.Text;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace System
{
	public static class Extensions
	{
		public static byte[] Serialize(this object value)
		{
			if (value == null)
			{
				return null;
			}

			string json = JsonConvert.SerializeObject(value);
			return Encoding.Default.GetBytes(json);
		}

		public static T Deserialize<T>(this byte[] value)
		{
			if (value == null)
			{
				return default(T);
			}

			return JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(value));
		}
	}
}