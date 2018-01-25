using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TwitterKit.Unity;
using System.Runtime.InteropServices;
using TappxSDK;

public class replay : MonoBehaviour {

	public GameObject replayCanvas, bestScorePoints, scorePoints, home, shop, playButton, shareButton;
	bool buttonClicked, buttonClicked1;

	public void Start() {
		PlayGamesScript.AddScoreToLeaderboard (GPGSIds.leaderboard_leaderboard, CloudVariables.ImportantValues[7]);
		PlayGamesScript.Instance.SaveData ();
		Text bestscorepoints = bestScorePoints.GetComponent<Text>();
		bestscorepoints.text = CloudVariables.ImportantValues[7].ToString();
		Text scorepoints = scorePoints.GetComponent<Text>();
		scorepoints.text = gameIn.scorePoints2.ToString();
	}

	public void home_Click(){
		replayCanvas.GetComponent<Animator> ().Play ("replayOff", -1, 0f);
		buttonClicked = true;
		buttonClicked1 = false;
	}	

	public void shop_Click(){	
		replayCanvas.GetComponent<Animator> ().Play ("replayOff", -1, 0f);
		buttonClicked = true;
		buttonClicked1 = true;
	}

	public void play_Click(){
		replayCanvas.GetComponent<Animator> ().Play ("replayOff", -1, 0f);
		buttonClicked = false;
	}

	void Update(){
		if(replayCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("replayOff") && 
			replayCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length < 
			replayCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).normalizedTime){
			if (!replayCanvas.GetComponent<AudioSource> ().isPlaying){
				if (buttonClicked) {
					if (buttonClicked1) {
						SceneManager.LoadScene ("shop");
					} else if (!buttonClicked1) {
						SceneManager.LoadScene ("menu");
					}
				} else if (!buttonClicked) {
					if (CloudVariables.ImportantValues [3] == 0)
						SceneManager.LoadScene ("extras");
					else if (CloudVariables.ImportantValues [3] == 1)
						SceneManager.LoadScene ("gameIn");
				}
			}
		}
	}

	public void startLogin() {
		if (!replayCanvas.GetComponent<AudioSource> ().isPlaying) {
			UnityEngine.Debug.Log ("startLogin()");
			Twitter.Init ();

			Twitter.LogIn (LoginCompleteWithCompose, (ApiError error) => {
				UnityEngine.Debug.Log (error.message);
			});
		}
	}

	public void LoginCompleteWithEmail (TwitterSession session) {
		UnityEngine.Debug.Log ("LoginCompleteWithEmail()");
		Twitter.RequestEmail (session, RequestEmailComplete, (ApiError error) => { UnityEngine.Debug.Log (error.message); });
	}

	public void RequestEmailComplete (string email) {
		UnityEngine.Debug.Log ("email=" + email);
		LoginCompleteWithCompose ( Twitter.Session );
	}

	public void LoginCompleteWithCompose(TwitterSession session) {
		ScreenCapture.CaptureScreenshot("Screenshot.png");
		UnityEngine.Debug.Log ("Screenshot location=" + Application.persistentDataPath + "/Screenshot.png");
		string imageUri = "file://" + Application.persistentDataPath + "/Screenshot.png";
		Twitter.Compose (session, imageUri, "My new best score is "+CloudVariables.ImportantValues [7].ToString()+"!" ,new string[]{"#B21"},
			(string tweetId) => { UnityEngine.Debug.Log ("Tweet Success, tweetId=" + tweetId); },
			(ApiError error) => { UnityEngine.Debug.Log ("Tweet Failed " + error.message); },
			() => { Debug.Log ("Compose cancelled"); }
		);
	}
}
