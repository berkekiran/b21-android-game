using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameIn : MonoBehaviour {

	public GameObject lifePoints, status, gameInCanvas, touchEnd, touchIn, quickstart, notification, sound, speed, size, extraLife, gameInExtraCanvas, sec, special, lbt, lbb, mbt, mbb, rbt, rbb, pausedCanvas, settingsCanvas;
	Sprite touchE_texture0, touchE_texture1, touchE_texture2, touchE_texture3, touchE_texture4, touchE_texture5, touchE_texture6, blank, selectedTop, selectedBottom;
	float sizeX = 0, sizeY = 0, sizeZ = 0, currentTime = 0.86f, secTime = 60f, slowdown = 0f;
	int lifePoints2, holdControl = 0, pointsdown = 0, tcounter = 0;
	bool canTouch = false, isCountingDown, isCountingDown1, isCountingDown2 = true, canWork = false, lock0 = false, isSecDown, lock1 = false;
	public static int scorePoints2 = 0;
	double end = 0.835;

	void Start () {
		PlayGamesScript.Instance.LoadData ();
		touchIn.transform.localScale = new Vector3 (sizeX, sizeY, sizeZ);
		touchE_texture0 = Resources.Load <Sprite>("touchEnd50");
		touchE_texture1 = Resources.Load <Sprite>("touchEnd40");
		touchE_texture2 = Resources.Load <Sprite>("touchEnd30");
		touchE_texture3 = Resources.Load <Sprite>("touchEnd20");
		touchE_texture4 = Resources.Load <Sprite>("touchEnd10");
		touchE_texture5 = Resources.Load <Sprite>("touchEnd5");
		touchE_texture6 = Resources.Load <Sprite>("touchEnd1");
		selectedBottom = Resources.Load <Sprite>("gameInExtraBottomButtonClick");
		selectedTop = Resources.Load <Sprite>("gameInExtraTopButtonClick"); 
		blank = Resources.Load <Sprite>("blank");
		Text speed1 = speed.GetComponent<Text>();
		speed1.text = "x" + CloudVariables.ImportantValues[4].ToString();
		Text size1 = size.GetComponent<Text>();
		size1.text = "x" + CloudVariables.ImportantValues[6].ToString();
		Text extraLife1 = extraLife.GetComponent<Text>();
		extraLife1.text = "x" + CloudVariables.ImportantValues[5].ToString();
		if (CloudVariables.ImportantValues [3] == 1)
			quickstart.GetComponent<Toggle> ().isOn = true;
		else
			quickstart.GetComponent<Toggle> ().isOn = false;
		if (CloudVariables.ImportantValues [2] == 1)
			notification.GetComponent<Toggle> ().isOn = true;
		else
			notification.GetComponent<Toggle> ().isOn = false;
		if (CloudVariables.ImportantValues [1] == 1) {
			sound.GetComponent<Toggle> ().isOn = true;
			AudioListener.volume = 0.0f;
		}
		else{
			AudioListener.volume = 1.0f;
			sound.GetComponent<Toggle> ().isOn = false;
		}
	}

	void Update () {
		Text lifePoints1 = lifePoints.GetComponent<Text> ();
		Text status1 = status.GetComponent<Text> ();
		status1.text = "READY";

		if (isCountingDown)
			on_timer_timeout0 ();

		if (isCountingDown1)
			on_timer_timeout1 ();

		if (isCountingDown2)
			open ();
		else
			canTouch = true;

		if(lifePoints2 == 4 || lifePoints2 == 3){
			tcounter++;
			if(tcounter == 3)
				PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQCQ");
			if(tcounter == 10)
				PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQCg");
			if(tcounter == 80)
				PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQCw");
			if(tcounter == 240)
				PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQDA");
			if(tcounter == 360)
				PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQDQ");
		}

		if(gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("touchEndPow") && 
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length < 
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).normalizedTime){
			if (lock0) {
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOn", -1, 0f);
				lock1 = true;
				lock0 = false;
			}
		}

		if(gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("gameInExtraOn") && 
			gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length < 
			gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).normalizedTime){
			if (lock1) {
				if (scorePoints2 >= 0) {
					secTime = 60f + slowdown;
				}
				if (scorePoints2 >= (80 + pointsdown)) {
					secTime = 30f + slowdown;
				}
				if (scorePoints2 >= (240 + pointsdown)) {
					secTime = 12f + slowdown;
				}
				if (scorePoints2 >= (360 + pointsdown)) {
					secTime = 6f + slowdown;
				}
				if (scorePoints2 >= (480 + pointsdown)) {
					secTime = 4f + slowdown;
				}
				if (scorePoints2 >= (640 + pointsdown)) {
					secTime = 3f + slowdown;
				}
				if (scorePoints2 >= (820 + pointsdown)) {
					secTime = 2f + slowdown;
				}
				isSecDown = true;
				lock1 = false;
			}
		}

		if (isSecDown)
			secCounter ();

		if (Input.touchCount > 0 && canWork == true){
			if (Input.GetTouch(0).phase == TouchPhase.Began && sizeX <= 0.001 && sizeY <= 0.001 && sizeZ <= 0.001 && canTouch == true) {
				holdControl = 1;
				isCountingDown = true;
				isCountingDown1 = false;
			} else if (Input.GetTouch(0).phase == TouchPhase.Ended && canTouch == true) {
				isCountingDown1 = true;
				isCountingDown = false;
				if (holdControl == 1 && sizeX <= 1 && sizeX >= end && sizeY <= 1 && sizeY >= end && sizeZ <= 1 && sizeZ >= end) {
					gameInCanvas.GetComponent<Animator> ().Play ("touchEndPow", -1, 0f);
					lock0 = true;
					holdControl = 0;				
				} else if (holdControl == 1 && sizeX >= 0.41 && sizeY >= 0.41 && sizeZ >= 0.41) {
					lifePoints2 -= 1;
					lifePoints1.text = "x" + lifePoints2.ToString ();
					gameInCanvas.GetComponent<Animator> ().Play ("lifePointsDown", -1, 0f);
					holdControl = 0;
					if (lifePoints2 == 0) {
						bestScoreControl ();
						gameInCanvas.GetComponent<Animator> ().Play ("gameInOff", -1, 0f);
					}
				}
				if (scorePoints2 >= 0) {
					PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQAg");
					end = 0.835;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture0;
				}
				if (scorePoints2 >= (80 + pointsdown)) {
					PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQAQ");
					end = 0.87;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture1;
				}
				if (scorePoints2 >= (240 + pointsdown)) {
					PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQBA");
					end = 0.9035;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture2;
				}
				if (scorePoints2 >= (360 + pointsdown)) {
					PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQBQ");
					end = 0.936;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture3;
				}
				if (scorePoints2 >= (480 + pointsdown)) {
					PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQBg");
					end = 0.969;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture4;
				}
				if (scorePoints2 >= (640 + pointsdown)) {
					PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQBw");
					end = 0.985;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture5;
				}
				if (scorePoints2 >= (820 + pointsdown)) {
					PlayGamesScript.UnlockAchievement ("CgkIlsbB96oXEAIQAw");
					end = 0.997;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture6;
				}
				canWork = false;
			}
		}
	}

	void secCounter(){
		secTime -= Time.deltaTime;
		int secTime1 = Mathf.RoundToInt(secTime);
		Text secInside1 = sec.GetComponent<Text>();
		secInside1.text = secTime1.ToString();

		if (secTime < 0)
		{
			secTime = 0;
			secInside1.text = secTime1.ToString ();
			isSecDown = false;
		}
	}

	void on_timer_timeout0(){
		Text status1 = status.GetComponent<Text>();
		sizeX += 0.01F;
		sizeY += 0.01F;
		sizeZ += 0.01F;
		touchIn.transform.localScale = new Vector3 (sizeX, sizeY, sizeZ);
		status1.text = "HOLD";
	}

	void on_timer_timeout1(){
		Text status1 = status.GetComponent<Text>();
		if (sizeX >= 0.001 && sizeY >= 0.001 && sizeZ >= 0.001) {	
			sizeX -= 0.01F;
			sizeY -= 0.01F;
			sizeZ -= 0.01F;
			touchIn.transform.localScale = new Vector3 (sizeX, sizeY, sizeZ);
			if (sizeX == 0 && sizeY == 0) {
				status1.text = "READY";
			} else {
				status1.text = "WAIT";
			}
		}
	}

	public void bestScoreControl(){
		if (scorePoints2 >= CloudVariables.ImportantValues[7]){
			CloudVariables.SetImportantValues (7, scorePoints2);
		}
	}

	void open(){
		currentTime -= Time.deltaTime;
		if (currentTime < 0)
		{
			currentTime = 0;
			isCountingDown2 = false;
		}
	}

	public void workControl(){
		canWork = true;
	}

	public void lbtClick(){
		lbt.GetComponent<Image> ().sprite = selectedTop;
	}

	public void lbbClick(){
		lbb.GetComponent<Image> ().sprite = selectedBottom;
	}

	public void mbtClick(){
		mbt.GetComponent<Image> ().sprite = selectedTop;
	}

	public void mbbClick(){
		mbb.GetComponent<Image> ().sprite = selectedBottom;
	}

	public void rbtClick(){
		rbt.GetComponent<Image> ().sprite = selectedTop;
	}

	public void rbbClick(){
		rbb.GetComponent<Image> ().sprite = selectedBottom;
	}

	public void pausedClick(){
		if (canTouch)
			pausedCanvas.GetComponent<Animator> ().Play ("pausedOn");
	}

	public void pausedClick1(){
		pausedCanvas.GetComponent<Animator> ().Play ("pausedOff");			
	}

	public void settingsClick(){
		if (canTouch)
			settingsCanvas.GetComponent<Animator> ().Play ("settingsOn");
	}

	public void settingsClick1(){
		settingsCanvas.GetComponent<Animator> ().Play ("settingsOff");			
	}

}
