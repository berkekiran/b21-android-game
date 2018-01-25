using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayGamesScript : MonoBehaviour
{
	public static PlayGamesScript Instance { get; private set;}

	const string SAVE_NAME = "MainSave";
	bool isSaving;
	bool isCloudDataLoaded = false;

	void Start()
	{
		Instance = this;
		if (!PlayerPrefs.HasKey(SAVE_NAME))
			PlayerPrefs.SetString(SAVE_NAME, string.Empty);
		if (!PlayerPrefs.HasKey("IsFirstTime"))
			PlayerPrefs.SetInt("IsFirstTime", 1);

		if (PlayerPrefs.GetInt ("IsFirstTime") == 1) {
			PlayerPrefs.SetInt ("IsFirstTime", 0);
			CloudVariables.SetImportantValues (0, 0);
			CloudVariables.SetImportantValues (1, 1);
			CloudVariables.SetImportantValues (2, 1);
			CloudVariables.SetImportantValues (3, 0);
			CloudVariables.SetImportantValues (4, 0);
			CloudVariables.SetImportantValues (5, 0);
			CloudVariables.SetImportantValues (6, 0);
			CloudVariables.SetImportantValues (7, 0);
		}

		LoadLocal();

		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			.EnableSavedGames().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();

		SignIn();
	}

	void SignIn()
	{
		Social.localUser.Authenticate(success => { LoadData(); });
	}

	#region Saved Games

	string GameDataToString()
	{
		return JsonUtil.CollectionToJsonString(CloudVariables.ImportantValues, "SaveKey");
	}

	void StringToGameData(string cloudData, string localData)
	{
		if (cloudData == string.Empty)
		{
			StringToGameData(localData);
			isCloudDataLoaded = true;
			return;
		}
		int[] cloudArray = JsonUtil.JsonStringToArray(cloudData, "SaveKey", str => int.Parse(str));

		if (localData == string.Empty)
		{
			CloudVariables.ImportantValues = cloudArray;
			PlayerPrefs.SetString(SAVE_NAME, cloudData);
			isCloudDataLoaded = true;
			return;
		}
		int[] localArray = JsonUtil.JsonStringToArray(localData, "SaveKey", str => int.Parse(str));

		if (PlayerPrefs.GetInt("IsFirstTime") == 1)
		{
			PlayerPrefs.SetInt("IsFirstTime", 0);
			for (int i = 0; i < cloudArray.Length; i++)
				if (cloudArray[i] > localArray[i]) 
				{
					PlayerPrefs.SetString(SAVE_NAME, cloudData);
				}
		}
		else
		{
			for (int i = 0; i < cloudArray.Length; i++)
				if (localArray[i] > cloudArray[i])
				{
					CloudVariables.ImportantValues = localArray;
					isCloudDataLoaded = true;
					SaveData();
					return;
				}
		}
		CloudVariables.ImportantValues = cloudArray;
		isCloudDataLoaded = true;
	}

	void StringToGameData(string localData)
	{
		if (localData != string.Empty)
			CloudVariables.ImportantValues = JsonUtil.JsonStringToArray(localData, "SaveKey",
				str => int.Parse(str));
	}

	public void LoadData()
	{
		if (Social.localUser.authenticated)
		{
			isSaving = false;
			((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME,
				DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
		}
		else
		{
			LoadLocal();
		}
	}

	private void LoadLocal()
	{
		StringToGameData(PlayerPrefs.GetString(SAVE_NAME));
	}

	public void SaveData()
	{
		if (!isCloudDataLoaded)
		{
			SaveLocal();
			return;
		}
		if (Social.localUser.authenticated)
		{
			isSaving = true;
			((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME,
				DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
		}
		else
		{
			SaveLocal();
		}
	}

	private void SaveLocal()
	{
		PlayerPrefs.SetString(SAVE_NAME, GameDataToString());
	}

	private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData,
		ISavedGameMetadata unmerged, byte[] unmergedData)
	{
		if (originalData == null)
			resolver.ChooseMetadata(unmerged);
		else if (unmergedData == null)
			resolver.ChooseMetadata(original);
		else
		{
			string originalStr = Encoding.ASCII.GetString(originalData);
			string unmergedStr = Encoding.ASCII.GetString(unmergedData);

			int[] originalArray = JsonUtil.JsonStringToArray(originalStr, "SaveKey", str => int.Parse(str));
			int[] unmergedArray = JsonUtil.JsonStringToArray(unmergedStr, "SaveKey", str => int.Parse(str));

			for (int i = 0; i < originalArray.Length; i++)
			{
				if (originalArray[i] > unmergedArray[i])
				{
					resolver.ChooseMetadata(original);
					return;
				}
				else if (unmergedArray[i] > originalArray[i])
				{
					resolver.ChooseMetadata(unmerged);
					return;
				}
			}
			resolver.ChooseMetadata(original);
		}
	}

	private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			if (!isSaving)
				LoadGame(game);
			else
				SaveGame(game);
		}
		else
		{
			if (!isSaving)
				LoadLocal();
			else
				SaveLocal();
		}
	}

	private void LoadGame(ISavedGameMetadata game)
	{
		((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
	}

	private void SaveGame(ISavedGameMetadata game)
	{
		string stringToSave = GameDataToString();
		PlayerPrefs.SetString(SAVE_NAME, stringToSave);

		byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);
		SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
		((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave,
			OnSavedGameDataWritten);
	}

	private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			string cloudDataString;
			if (savedData.Length == 0)
				cloudDataString = string.Empty;
			else
				cloudDataString =  Encoding.ASCII.GetString(savedData);

			string localDataString = PlayerPrefs.GetString(SAVE_NAME);

			StringToGameData(cloudDataString, localDataString);
		}
	}

	//callback for CommitUpdate
	private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
	{

	}
	#endregion /Saved Games

	#region Achievements
	public static void UnlockAchievement(string id)
	{
		Social.ReportProgress(id, 100, success => { });
	}

	public static void ShowAchievementsUI()
	{
		Social.ShowAchievementsUI();
		Debug.Log ("ShowAchievementsUI");
	}
	#endregion /Achievements

	#region Leaderboards
	public static void AddScoreToLeaderboard(string leaderboardId, long score)
	{
		Social.ReportScore(score, leaderboardId, success => { });
	}

	public static void ShowLeaderboardsUI()
	{
		Social.ShowLeaderboardUI();
		Debug.Log ("ShowLeaderboardUI");
	}
	#endregion /Leaderboards

}