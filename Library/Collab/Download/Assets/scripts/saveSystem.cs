using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class SaveSystem : MonoBehaviour {

	public string mode;
	public string quickstart;
	public string notification;
	public string sound;
	public string extraLife;
	public string slowSpeed;
	public string slowSize;
	public string bestScore;

	public string jsonString;
	public JsonData gameData;

	public void save_data () {
		string[] saveData = { mode, quickstart, notification, sound, extraLife, slowSpeed, slowSize, bestScore};
		gameData = JsonMapper.ToJson (saveData);
		File.WriteAllText (Application.dataPath + "/data.json", gameData.ToString());
	}

	public void load_data () {
		jsonString = File.ReadAllText (Application.dataPath + "/data.json");
		gameData = JsonMapper.ToObject (jsonString);
		mode = (string) gameData ["mode"];
		quickstart = (string) gameData ["quickstart"];
		notification = (string) gameData ["notification"];
		sound = (string) gameData ["sound"];
		extraLife = (string) gameData ["extraLife"];
		slowSpeed = (string) gameData ["slowSpeed"];
		slowSize = (string) gameData ["slowSize"];
		bestScore = (string) gameData ["bestScore"];
	}

}