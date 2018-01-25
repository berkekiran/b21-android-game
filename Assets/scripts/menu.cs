using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TwitterKit.Unity;

public class menu : MonoBehaviour {

	public GameObject menuCanvas, menuPlayButton, AchievementsButton, LeaderboardsButton, ShareButton, GooglePlayButton;

	void Start(){
		PlayGamesScript.Instance.LoadData ();
	}

	public void PlayButton(){
			menuCanvas.GetComponent<Animator> ().Play ("menuOff");
	}

	public void GooglePlayButton_Click(){ 
		if (!menuCanvas.GetComponent<AudioSource> ().isPlaying)
			Application.OpenURL ("market://details?id=com.berkekiran.b21");
	}

	void Update(){
		if (menuCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("menuOff") &&
		   menuCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length <
		   menuCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime) {
			if (!menuCanvas.GetComponent<AudioSource> ().isPlaying) {
				if (CloudVariables.ImportantValues [3] == 1)
					SceneManager.LoadScene ("gameIn");
				else
					SceneManager.LoadScene ("extras");
			}
		}
	}

	public void Show_Achievements()
	{
		if (!menuCanvas.GetComponent<AudioSource> ().isPlaying)
			PlayGamesScript.ShowAchievementsUI ();
	}

	public void Show_Leaderboards()
	{
		if (!menuCanvas.GetComponent<AudioSource> ().isPlaying)
			PlayGamesScript.ShowLeaderboardsUI ();
	}
		
	public void startLogin() {
		if (!menuCanvas.GetComponent<AudioSource> ().isPlaying) {
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
		Twitter.Compose (session, "", "I'm playing" ,new string[]{"#B21"},
			(string tweetId) => { UnityEngine.Debug.Log ("Tweet Success, tweetId=" + tweetId); },
			(ApiError error) => { UnityEngine.Debug.Log ("Tweet Failed " + error.message); },
			() => { Debug.Log ("Compose cancelled"); }
		);
	}
}
