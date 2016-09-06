using System.Text;
using Newtonsoft.Json;

namespace StandardQueue
{
	internal static class Extensions
	{
		internal static byte[] Serialize(this object value)
		{
			if (value == null)
			{
				return null;
			}

			string json = JsonConvert.SerializeObject(value);
			return Encoding.Default.GetBytes(json);
		}

		internal static T Deserialize<T>(this byte[] value)
		{
			if (value == null)
			{
				return default(T);
			}

			return JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(value));
		}
	}
}