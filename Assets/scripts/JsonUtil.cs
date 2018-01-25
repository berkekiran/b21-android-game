using System.Collections;
using Boomlagoon.JSON;
using System;

public class JsonUtil {

	public static string CollectionToJsonString<T>(T arr, string jsonKey) where T : IList
	{
		JSONObject jObject = new JSONObject();
		JSONArray jArray = new JSONArray();
		for (int i = 0; i < arr.Count; i++)
			jArray.Add(new JSONValue(arr[i].ToString()));
		jObject.Add(jsonKey, jArray);
		return jObject.ToString();
	}

	public static T[] JsonStringToArray<T>(string jsonString, string jsonKey,
		Func<string, T> parser)
	{
		JSONObject jObject = JSONObject.Parse(jsonString);
		JSONArray jArray = jObject.GetArray(jsonKey);

		T[] convertedArray = new T[jArray.Length];
		for (int i = 0; i < jArray.Length; i++)
			convertedArray[i] = parser(jArray[i].Str.ToString());
		return convertedArray;
	}

}