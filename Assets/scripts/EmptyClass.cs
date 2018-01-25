using System;

namespace AssemblyCSharp
{
	public class EmptyClass
	{
		/*
	public GameObject featureStatus1, featureTime1, featureStatusExtra1, featureTimeExtra1, touchIn, status1, sizeC, speedC, extraLifeC, lifePoints1, scorePoints1, targetPoints1, nPoints1, size1, speed1, extraLife1, saveSystem, touchEnd, gameInCanvas, pausedCanvas, settingsCanvas, gameInExtraCanvas, quickstart, notification, sound;
	bool isCountingDown2 = true,  isCountingDown3 = true, canTouch = false, canWork = false, lock1 = true, lock2 = false, lock3 = false, lock4 = false, lock5 = false;
	float sizeX = 0, sizeY = 0, sizeZ = 0, currentTime = 0.86f, currentTime1 = 5f;
	double speedT = 0.01, end = 0.835, speedT1 = 0.01;
	int lifePoints2, nPoints2 = 0, pointsdown = 0, targetPoints2 = 3, holdControl = 0, isCountingDown = 2, isCountingDown1 = 2, numberColorTemp, extraLifePoint1 = 0;
	public static int scorePoints2 = 0;
	Sprite touchE_texture0, touchE_texture1, touchE_texture2, touchE_texture3, touchE_texture4, touchE_texture5, touchE_texture6, blank, selectedTop, selectedBottom;

	void Start () {

		PlayGamesScript.Instance.LoadData ();

		if (CloudVariables.ImportantValues [0] == 3)
			extraLifePoint1 = 1;

		lifePoints2 = 3 + extraLifePoint1;

		Text lifePoints = lifePoints1.GetComponent<Text> ();
		lifePoints.text = "x" + lifePoints2.ToString ();

		pausedCanvas.GetComponent<Canvas> ().enabled = false;

		Text targetPoints = targetPoints1.GetComponent<Text>();
		targetPoints2 = (int)( end / speedT );
		targetPoints.text = targetPoints2.ToString();

		Text speed = speed1.GetComponent<Text>();
		speed.text = "x" + CloudVariables.ImportantValues[4].ToString();
		Text size = size1.GetComponent<Text>();
		size.text = "x" + CloudVariables.ImportantValues[6].ToString();
		Text extraLife = extraLife1.GetComponent<Text>();
		extraLife.text = "x" + CloudVariables.ImportantValues[5].ToString();

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

	public void workControl(){
		canWork = true;
	}

	void UpdateCounter2(){
		currentTime -= Time.deltaTime;
		if (currentTime < 0)
		{
			currentTime = 0;
			isCountingDown2 = false;
		}
	}

	void UpdateCounter3(){
		currentTime1 -= Time.deltaTime;
		int currentTime2 = Mathf.RoundToInt(currentTime1);
		Text featureTime = featureTime1.GetComponent<Text>();
		featureTime.text = currentTime2.ToString() + " sec";

		if (currentTime1 < 0)
		{
			currentTime1 = 0;
			featureTime.text = currentTime1.ToString() + " sec";
			isCountingDown3= false;
		}
	}

	void timer0()
	{
		speedT1 = speedT;
		speedT1 -= Time.deltaTime;
		if (speedT1 < 0)
		{
			speedT1 = 0;
			isCountingDown = 1;
		}
	}

	void timer1()
	{
		speedT1 = speedT;
		speedT1 -= Time.deltaTime;
		if (speedT1 < 0)
		{
			speedT1 = 0;
			isCountingDown1 = 1;
		}
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

	void Update () {

		if(gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("gameInOn") && 
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length < 
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).normalizedTime){
			lock3 = true;
			lock2 = true;
		}

		if (lock3) {
			if (CloudVariables.ImportantValues [0] == 0) {
				Text featureStatus = featureStatus1.GetComponent<Text> ();
				featureStatus.text = "";
				Text featureTime = featureTime1.GetComponent<Text> ();
				featureTime.text = "";
			}
			if (CloudVariables.ImportantValues [0] == 1) {
				Text featureStatus = featureStatus1.GetComponent<Text> ();
				featureStatus.text = "Slow Speed";
				lock4 = true;
			}
			if (CloudVariables.ImportantValues [0] == 2) {
				Text featureStatus = featureStatus1.GetComponent<Text> ();
				featureStatus.text = "Size Points";
				pointsdown = 500;
			}
			if (CloudVariables.ImportantValues [0] == 3) {
				Text featureStatus = featureStatus1.GetComponent<Text> ();
				featureStatus.text = "Extra Life";
			}
			lock3 = false;
		}

		Text scorePoints = scorePoints1.GetComponent<Text> ();
		Text lifePoints = lifePoints1.GetComponent<Text> ();

		if(gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("gameInOff") && 
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length < 
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).normalizedTime){
			SceneManager.LoadScene("replay");
		}

		if(gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("touchEndPow") && 
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length < 
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).normalizedTime){
			if(lock5)
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOn", -1, 0f);
		}

		if(gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("gameInExtraOf") && 
			gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length < 
			gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).normalizedTime){
			lock5 = false;
		}

		if(gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("featureExtra") && 
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length < 
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).normalizedTime){
			if(CloudVariables.ImportantValues [0] == 3){
				Text featureStatus = featureStatus1.GetComponent<Text> ();
				featureStatus.text = "";
				Text featureTime = featureTime1.GetComponent<Text> ();
				featureTime.text = "";
				gameInCanvas.GetComponent<Animator> ().Play ("featureExtra", -1, 0f);
			}
		}

		if (quickstart.GetComponent<Toggle> ().isOn)
			CloudVariables.SetImportantValues (3, 1);
		else
			CloudVariables.SetImportantValues (3, 0);

		if (notification.GetComponent<Toggle> ().isOn)
			CloudVariables.SetImportantValues (2, 1);
		else
			CloudVariables.SetImportantValues (2, 0);

		if (sound.GetComponent<Toggle> ().isOn)
			CloudVariables.SetImportantValues (1, 1);
		else
			CloudVariables.SetImportantValues (1, 0);
		
		if (isCountingDown2)
			UpdateCounter2 ();
		else
			canTouch = true;
		
		Text status = status1.GetComponent<Text> ();
		status.text = "READY";

		if (isCountingDown == 0 && isCountingDown1 == 2)
			timer0 ();
		else if (isCountingDown == 1)
			on_timer_timeout0 ();
		
		if (isCountingDown1 == 0 && isCountingDown == 2)
			timer1 ();
		else if (isCountingDown1 == 1)
			on_timer_timeout1 ();

		if (scorePoints2 >= 5 && !lock4) {
			speedT = 0.0135;
		} else {
			speedT = 0.01;
		}

		if (isCountingDown3 && lock2 && lifePoints2 != 0 && CloudVariables.ImportantValues [0] != 0) {
			UpdateCounter3 ();
		}

		if(!isCountingDown3)
			lock2 = false;
		
		if (!isCountingDown3 && lock1 && !lock2 && CloudVariables.ImportantValues [0] == 3) {
			if (lifePoints2 != 0) {
				lifePoints2 -= 1;
				lifePoints.text = "x" + lifePoints2.ToString ();
				gameInCanvas.GetComponent<Animator> ().Play ("lifePointsDown", -1, 0f);
			} else {
				gameInCanvas.GetComponent<Animator> ().Play ("featureExtra", -1, 0f);
			}
			lock1 = false;
		}
		if (!isCountingDown3 && lock1 && !lock2 && lifePoints2 != 0 && CloudVariables.ImportantValues [0] == 1) {
			Text featureStatusExtra = featureStatusExtra1.GetComponent<Text>();
			featureStatusExtra.text = "Speed Up";
			Text featureTimeExtra = featureTimeExtra1.GetComponent<Text>();
			featureTimeExtra.text = "+0.035";
			gameInCanvas.GetComponent<Animator> ().Play ("featureExtra", -1, 0f);
			lock4 = false;
			lock1 = false;
		}
		if (!isCountingDown3 && lock1 && !lock2 && lifePoints2 != 0 && CloudVariables.ImportantValues [0] == 2) {
			Text featureStatusExtra = featureStatusExtra1.GetComponent<Text>();
			featureStatusExtra.text = "Points Down";
			Text featureTimeExtra = featureTimeExtra1.GetComponent<Text>();
			featureTimeExtra.text = "-500";
			gameInCanvas.GetComponent<Animator> ().Play ("featureExtra", -1, 0f);
			pointsdown = 0;
			lock1 = false;
		}

		if (Input.touchCount > 0 && canWork == true){
			if (Input.GetTouch(0).phase == TouchPhase.Began && sizeX <= speedT && sizeY <= speedT && sizeZ <= speedT && canTouch == true) {
				holdControl = 1;
				isCountingDown = 0;
				isCountingDown1 = 2;
			} else if (Input.GetTouch(0).phase == TouchPhase.Ended && canTouch == true) {
				isCountingDown1 = 0;
				isCountingDown = 2;
				if (holdControl == 1 && sizeX <= 1 && sizeX >= end && sizeY <= 1 && sizeY >= end && sizeZ <= 1 && sizeZ >= end) {
					scorePoints2 += 5;
					scorePoints.text = scorePoints2.ToString ();
					gameInCanvas.GetComponent<Animator> ().Play ("touchEndPow", -1, 0f);
					lock5 = true;
					holdControl = 0;				
				} else if (holdControl == 1 && sizeX >= 0.41 && sizeY >= 0.41 && sizeZ >= 0.41) {
					lifePoints2 -= 1;
					lifePoints.text = "x" + lifePoints2.ToString ();
					gameInCanvas.GetComponent<Animator> ().Play ("lifePointsDown", -1, 0f);
					holdControl = 0;
					if (lifePoints2 == 0) {
						bestScoreControl ();
						gameInCanvas.GetComponent<Animator> ().Play ("gameInOff", -1, 0f);
					}
				}
				if (scorePoints2 >= 0) {
					end = 0.835;
					targetPoints2 = (int)(end / speedT);
					touchEnd.GetComponent<Image> ().sprite = touchE_texture0;
				}
				if (scorePoints2 >= (500 + pointsdown)) {
					end = 0.87;
					targetPoints2 = (int)(end / speedT);
					touchEnd.GetComponent<Image> ().sprite = touchE_texture1;
				}
				if (scorePoints2 >= (2000 + pointsdown)) {
					end = 0.9035;
					targetPoints2 = (int)(end / speedT);
					touchEnd.GetComponent<Image> ().sprite = touchE_texture2;
				}
				if (scorePoints2 >= (4000 + pointsdown)) {
					end = 0.936;
					targetPoints2 = (int)(end / speedT);
					touchEnd.GetComponent<Image> ().sprite = touchE_texture3;
				}
				if (scorePoints2 >= (6000 + pointsdown)) {
					end = 0.969;
					targetPoints2 = (int)(end / speedT);
					touchEnd.GetComponent<Image> ().sprite = touchE_texture4;
				}
				if (scorePoints2 >= (8000 + pointsdown)) {
					end = 0.985;
					targetPoints2 = (int)(end / speedT);
					touchEnd.GetComponent<Image> ().sprite = touchE_texture5;
				}
				if (scorePoints2 >= (10000 + pointsdown)) {
					end = 0.997;
					targetPoints2 = (int)(end / speedT);
					touchEnd.GetComponent<Image> ().sprite = touchE_texture6;
				}
				canWork = false;

			}
		}
	}

	void on_timer_timeout0(){
		Text status = status1.GetComponent<Text>();
		Text nPoints = nPoints1.GetComponent<Text>();

		nPoints2 += 1;
		nPoints.text = nPoints2.ToString();
		sizeX += (float) speedT;
		sizeY += (float) speedT;
		sizeZ += (float) speedT;
		touchIn.transform.localScale = new Vector3 (sizeX, sizeY, sizeZ);
		status.text = "HOLD";
	}

	void on_timer_timeout1(){
		Text status = status1.GetComponent<Text>();
		Text nPoints = nPoints1.GetComponent<Text>();

		if (sizeX >= speedT && sizeY >= speedT && sizeZ >= speedT) {	
			nPoints2 -= 1;
			nPoints.text = nPoints2.ToString ();
			sizeX -= (float)speedT;
			sizeY -= (float)speedT;
			sizeZ -= (float)speedT;
			touchIn.transform.localScale = new Vector3 (sizeX, sizeY, sizeZ);
			if (sizeX == 0 && sizeY == 0) {
				status.text = "READY";
			} else {
				status.text = "WAIT";
			}
		}
	}

	public void bestScoreControl(){
		if (scorePoints2 >= CloudVariables.ImportantValues[7]){
			CloudVariables.SetImportantValues (7, scorePoints2);
		}
	}
*/
	}
}

