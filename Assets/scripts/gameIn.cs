using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameIn : MonoBehaviour {

	public GameObject lifePoints, status, gameInCanvas, touchEnd, touchIn, quickstart, notification, sound, speed, size, extraLife, gameInExtraCanvas, sec, special, lbt, lbb, mbt, mbb, rbt, rbb, pausedCanvas, settingsCanvas, s1t, s1b, s2t, s2b, s3t, s3b, scorePoints, featureStatus, featureTime, featureStatusExtra, featureTimeExtra, getreward, cancel;
	Sprite touchE_texture0, touchE_texture1, touchE_texture2, touchE_texture3, touchE_texture4, touchE_texture5, touchE_texture6, blank, selectedTop, selectedBottom;
	float sizeX = 0, sizeY = 0, sizeZ = 0, currentTime = 0.86f, secTime = 60f, secTime1 = 90f, slowdown = 0f;
	int lifePoints2, holdControl = 0, pointsdown = 0, tcounter = 0, indexA = 0, indexN = 0, extraLifePoint = 0;
	bool canTouch = false, isCountingDown, isCountingDown1, isCountingDown2 = true, canWork = false, lock0 = false, isSecDown, isSecDown1 = true, lock1 = false, lock2 = false, lock3 = false, lock4 = false, lock5 = true, selected0 = false, selected1 = false, selected2 = false, selectOne = false, lostPoints, reward = false;
	public static int scorePoints2 = 0;
	public static bool rewardVideoDid = false;
	double end = 0.835;
	string[] Alphabet = new string[25] {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","R","S","T","U","V","W","X","Y","Z"};
	string[] Numbers = new string[10] {"0","1","2","3","4","5","6","7","8","9"};
	string[] Numbers1 = new string[6] {"0","1","2","3","4","5"};
	string[] Numbers2 = new string[5] {"1","2","3","4","5"};
	string[] Specials = new string[3] {"-","+","="};
	string special1, special2, special3, special4, special5, special6, special7, randomNumbertoSelect, nTnumber, startCounter = "1";
	public const string achievement1 = "CgkIlsbB96oXEAIQCQ";
	public const string achievement2 = "CgkIlsbB96oXEAIQCg";
	public const string achievement3 = "CgkIlsbB96oXEAIQCw";
	public const string achievement4 = "CgkIlsbB96oXEAIQDA";
	public const string achievement5 = "CgkIlsbB96oXEAIQDQ";
	public const string achievement6 = "CgkIlsbB96oXEAIQAg";
	public const string achievement7 = "CgkIlsbB96oXEAIQAQ";
	public const string achievement8 = "CgkIlsbB96oXEAIQBA";
	public const string achievement9 = "CgkIlsbB96oXEAIQBQ";
	public const string achievement10 = "CgkIlsbB96oXEAIQBg";
	public const string achievement11 = "CgkIlsbB96oXEAIQBw";
	public const string achievement12 = "CgkIlsbB96oXEAIQCA";
	public const string achievement13 = "CgkIlsbB96oXEAIQAw";

	void Start () {
		PlayGamesScript.Instance.LoadData ();
		nTnumber = Numbers2 [Random.Range (0, Numbers2.Length)];
		scorePoints2 = 0;
		sizeX = 0;
		sizeY = 0;
		sizeZ = 0;
		secTime = 60f; 
		slowdown = 0f;
		end = 0.835;
		if (CloudVariables.ImportantValues [0] == 3)
			extraLifePoint = 1;
		lifePoints2 = 3 + extraLifePoint;
		Text lifePoints1 = lifePoints.GetComponent<Text> ();
		lifePoints1.text = "x" + lifePoints2.ToString ();
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
		/* if (CloudVariables.ImportantValues [2] == 1)
			notification.GetComponent<Toggle> ().isOn = true;
		else*/
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

		if (gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("gameInOn") &&
		    gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length <
		    gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime) {
			lock3 = true;
			lock4 = true;
		}

		if (gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("featureExtra") &&
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length <
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime) {
			Text featureStatus1 = featureStatus.GetComponent<Text> ();
			featureStatus1.text = "";
			Text featureTime1 = featureTime.GetComponent<Text> ();
			featureTime1.text = "";
			Text featureStatusExtra1 = featureStatusExtra.GetComponent<Text>();
			featureStatusExtra1.text = "";
			Text featureTimeExtra1 = featureTimeExtra.GetComponent<Text>();
			featureTimeExtra1.text = "";
		}

		if (gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("getrewardoff") &&
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length <
			gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime) {
			if (!gameInCanvas.GetComponent<AudioSource> ().isPlaying) {
				if (reward)
					mobileAds.Instance.GameOver ();
				SceneManager.LoadScene ("replay");
			}
		}



		if (gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("gameInOff") &&
		    gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length <
		    gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime) {
			if (startCounter == nTnumber) {
				rewardVideoDid = true;
				gameInCanvas.GetComponent<Animator> ().Play ("getreward", -1, 0f);
			} else {
				rewardVideoDid = false;
				SceneManager.LoadScene ("replay");
			}
		}


		if (gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("touchEndPow") &&
		    gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length <
		    gameInCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime) {
			if (lock0) {
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOn", -1, 0f);
				lock1 = true;
				lock0 = false;
			}
		}

		if (gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("gameInExtraOff") &&
		    gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length <
		    gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime) {
			isSecDown = false;
			if (lostPoints && lock2) {
				lifePoints2 -= 1;
				lifePoints.GetComponent<AudioSource> ().Play();
				gameInCanvas.GetComponent<Animator> ().Play ("lifePointsDown", -1, 0f);
				lifePoints1.text = "x" + lifePoints2.ToString ();
				if (lifePoints2 <= 0) {
					bestScoreControl ();
					gameInCanvas.GetComponent<Animator> ().Play ("gameInOff", -1, 0f);
				}
				lock2 = false;
			} else if (!lostPoints && lock2) {
				scorePoints.GetComponent<AudioSource> ().Play();
				scorePoints2 += 5;
				Text scorePoints1 = scorePoints.GetComponent<Text> ();
				tcounter++;
				gameInCanvas.GetComponent<Animator> ().Play ("pointUp", -1, 0f);
				lock2 = false;
				scorePoints1.text = scorePoints2.ToString ();
				if (lifePoints2 == 4 || lifePoints2 == 3) {
					if (tcounter == 3)
						PlayGamesScript.UnlockAchievement (achievement1);
					if (tcounter == 10)
						PlayGamesScript.UnlockAchievement (achievement2);
					if (tcounter == 80)
						PlayGamesScript.UnlockAchievement (achievement3);
					if (tcounter == 240)
						PlayGamesScript.UnlockAchievement (achievement4);
					if (tcounter == 360)
						PlayGamesScript.UnlockAchievement (achievement5);
				}
			}
		}

		if (gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("gameInExtraOn") &&
		    gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length <
		    gameInExtraCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime) {
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

				Text s1t1 = s1t.GetComponent<Text> ();
				Text s1b1 = s1b.GetComponent<Text> ();
				Text s2t1 = s2t.GetComponent<Text> ();
				Text s2b1 = s2b.GetComponent<Text> ();
				Text s3t1 = s3t.GetComponent<Text> ();
				Text s3b1 = s3b.GetComponent<Text> ();

				special1 = Alphabet [Random.Range (0, Alphabet.Length)];
				special2 = Numbers [Random.Range (0, Numbers.Length)];
				special3 = Specials [Random.Range (0, Specials.Length)];

				if (special1 == "A" && special3 == "-") {
					string[] Specials1 = new string[2] { "+", "=" };
					special3 = Specials1 [Random.Range (0, Specials1.Length)];
				} 

				if (special1 == "Z" && special3 == "+") {
					string[] Specials1 = new string[2] { "-", "=" };
					special3 = Specials1 [Random.Range (0, Specials1.Length)];
				} 

				if (special2 == "0" && special3 == "-") {
					string[] Specials1 = new string[2] { "+", "=" };
					special3 = Specials1 [Random.Range (0, Specials1.Length)];
				} 

				if (special2 == "9" && special3 == "+") {
					string[] Specials1 = new string[2] { "-", "=" };
					special3 = Specials1 [Random.Range (0, Specials1.Length)];
				} 

				indexA = System.Array.IndexOf (Alphabet, special1);
				indexN = System.Array.IndexOf (Numbers, special2);

				if (special3 == "-") {
					if(Alphabet [indexA] == Alphabet [0])
						special4 = Alphabet [0];
					else
						special4 = Alphabet [indexA - 1];
					
					if(Numbers [indexN] == Numbers [0])
						special5 = Numbers [0];
					else
						special5 = Numbers [indexN - 1];
					
				} else if (special3 == "+") {
					if (Alphabet [indexA] == Alphabet [23])
						special6 = Alphabet [23];
					else
						special6 = Alphabet [indexA + 1];
					
					if(Numbers [indexN] == Numbers [9])	
						special7 = Numbers [9];
					else
						special7 = Numbers [indexN + 1];
				}

				Text special0 = special.GetComponent<Text> ();
				special0.text = special1 + " " + special2 + " " + special3;

				randomNumbertoSelect = Numbers1 [Random.Range (0, Numbers1.Length)];

				if (randomNumbertoSelect == "0") {
					if (special3 == "=")
						s1t1.text = special1 + special2;
					else if (special3 == "-")
						s1t1.text = special4 + special5;
					else if (special3 == "+")
						s1t1.text = special6 + special7;
					s1b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s2t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s2b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s3t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s3b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
				} else if (randomNumbertoSelect == "1") {
					if (special3 == "=")
						s1b1.text = special1 + special2;
					else if (special3 == "-")
						s1b1.text = special4 + special5;
					else if (special3 == "+")
						s1b1.text = special6 + special7;
					s1t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s2t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s2b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s3t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s3b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
				} else if (randomNumbertoSelect == "2") {
					if (special3 == "=")
						s2t1.text = special1 + special2;
					else if (special3 == "-")
						s2t1.text = special4 + special5;
					else if (special3 == "+")
						s2t1.text = special6 + special7;
					s1t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s1b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s2b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s3t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s3b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
				} else if (randomNumbertoSelect == "3") {
					if (special3 == "=")
						s2b1.text = special1 + special2;
					else if (special3 == "-")
						s2b1.text = special4 + special5;
					else if (special3 == "+")
						s2b1.text = special6 + special7;
					s1t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s1b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s2t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s3t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s3b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
				} else if (randomNumbertoSelect == "4") {
					if (special3 == "=")
						s3t1.text = special1 + special2;
					else if (special3 == "-")
						s3t1.text = special4 + special5;
					else if (special3 == "+")
						s3t1.text = special6 + special7;
					s1t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s1b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s2t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s2b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s3b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
				} else if (randomNumbertoSelect == "5") {
					if (special3 == "=")
						s3b1.text = special1 + special2;
					else if (special3 == "-")
						s3b1.text = special4 + special5;
					else if (special3 == "+")
						s3b1.text = special6 + special7;
					s1t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s1b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s2t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s2b1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
					s3t1.text = Alphabet [Random.Range (0, Alphabet.Length)] + Numbers [Random.Range (0, Numbers.Length)];
				}

				isSecDown = true;
				lock1 = false;

				if (special3 == "-") {
					selected0 = true;
					selected1 = false;
					selected2 = false;
				} else if (special3 == "+") {
					selected0 = false;
					selected1 = true;
					selected2 = false;
				} else if (special3 == "=") {
					selected0 = false;
					selected1 = false;
					selected2 = true;
				}

				lbt.GetComponent<Image> ().sprite = blank;
				lbb.GetComponent<Image> ().sprite = blank;
				mbt.GetComponent<Image> ().sprite = blank;
				mbb.GetComponent<Image> ().sprite = blank;
				rbt.GetComponent<Image> ().sprite = blank;
				rbb.GetComponent<Image> ().sprite = blank;

				selectOne = true;
			}
		}

		if (lock3) {
			if (CloudVariables.ImportantValues [0] == 0) {
				Text featureStatus1 = featureStatus.GetComponent<Text> ();
				featureStatus1.text = "";
				Text featureTime1 = featureTime.GetComponent<Text> ();
				featureTime1.text = "";
			}
			if (CloudVariables.ImportantValues [0] == 1) {
				Text featureStatus1 = featureStatus.GetComponent<Text> ();
				featureStatus1.text = "Slow Speed";
				slowdown = 30f;
			}
			if (CloudVariables.ImportantValues [0] == 2) {
				Text featureStatus1 = featureStatus.GetComponent<Text> ();
				featureStatus1.text = "Size Points";
				pointsdown = 120;
			}
			if (CloudVariables.ImportantValues [0] == 3) {
				Text featureStatus1 = featureStatus.GetComponent<Text> ();
				featureStatus1.text = "Extra Life";
				extraLifePoint = 1;
			}
			lock3 = false;
		}

		if (quickstart.GetComponent<Toggle> ().isOn) {
			CloudVariables.SetImportantValues (3, 1);
			PlayGamesScript.Instance.SaveData ();
		} else {
			CloudVariables.SetImportantValues (3, 0);
			PlayGamesScript.Instance.SaveData ();
		}
		if (notification.GetComponent<Toggle> ().isOn) {
			CloudVariables.SetImportantValues (2, 1);
			PlayGamesScript.Instance.SaveData ();
		} else {
			CloudVariables.SetImportantValues (2, 0);
			PlayGamesScript.Instance.SaveData ();
		}
		if (sound.GetComponent<Toggle> ().isOn) {
			AudioListener.volume = 1.0f;
			CloudVariables.SetImportantValues (1, 1);
			PlayGamesScript.Instance.SaveData ();
		} else {
			AudioListener.volume = 0.0f;
			CloudVariables.SetImportantValues (1, 0);
			PlayGamesScript.Instance.SaveData ();
		}
		if (isSecDown1 && lock4 && lifePoints2 != 0 && CloudVariables.ImportantValues [0] != 0) {
			secCounter1 ();
		}

		if(!isSecDown1)
			lock4 = false;

		if (!isSecDown1 && lock5 && !lock4 && CloudVariables.ImportantValues [0] == 3) {
			if (lifePoints2 != 0) {
				lifePoints2 -= 1;
				lifePoints1.text = "x" + lifePoints2.ToString ();
				gameInCanvas.GetComponent<Animator> ().Play ("lifePointsDown", -1, 0f);
			} else {
				gameInCanvas.GetComponent<Animator> ().Play ("featureExtra", -1, 0f);
			}
			lock5 = false;
		}
		if (!isSecDown1 && lock5 && !lock4 && lifePoints2 != 0 && CloudVariables.ImportantValues [0] == 1) {
			Text featureStatusExtra1 = featureStatusExtra.GetComponent<Text>();
			featureStatusExtra1.text = "Speed Up";
			Text featureTimeExtra1 = featureTimeExtra.GetComponent<Text>();
			featureTimeExtra1.text = "-30 sec";
			slowdown = 0f;
			gameInCanvas.GetComponent<Animator> ().Play ("featureExtra", -1, 0f);
			lock5 = false;
		}
		if (!isSecDown1 && lock5 && !lock4 && lifePoints2 != 0 && CloudVariables.ImportantValues [0] == 2) {
			Text featureStatusExtra1 = featureStatusExtra.GetComponent<Text>();
			featureStatusExtra1.text = "Points Down";
			Text featureTimeExtra1 = featureTimeExtra.GetComponent<Text>();
			featureTimeExtra1.text = "-120 points";
			gameInCanvas.GetComponent<Animator> ().Play ("featureExtra", -1, 0f);
			pointsdown = 0;
			lock5 = false;
		}

		if (isSecDown)
			secCounter ();

		if (Input.touchCount > 0 && canWork == true){
			if (Input.GetTouch(0).phase == TouchPhase.Began && sizeX <= 0.001 && sizeY <= 0.001 && sizeZ <= 0.001 && canTouch == true) {
				touchIn.GetComponent<AudioSource> ().Play();
				holdControl = 1;
				isCountingDown = true;
				isCountingDown1 = false;
			} else if (Input.GetTouch(0).phase == TouchPhase.Ended && canTouch == true) {
				touchIn.GetComponent<AudioSource> ().Stop();
				isCountingDown1 = true;
				isCountingDown = false;
				if (holdControl == 1 && sizeX <= 1 && sizeX >= end && sizeY <= 1 && sizeY >= end && sizeZ <= 1 && sizeZ >= end) {
					touchEnd.GetComponent<AudioSource> ().Play();
					gameInCanvas.GetComponent<Animator> ().Play ("touchEndPow", -1, 0f);
					lock0 = true;
					holdControl = 0;				
				} else if (holdControl == 1 && sizeX >= 0.41 && sizeY >= 0.41 && sizeZ >= 0.41) {
					lifePoints2 -= 1;
					lifePoints.GetComponent<AudioSource> ().Play();
					gameInCanvas.GetComponent<Animator> ().Play ("lifePointsDown", -1, 0f);
					lifePoints1.text = "x" + lifePoints2.ToString ();
					holdControl = 0;
					if (lifePoints2 <= 0) {
						bestScoreControl ();
						gameInCanvas.GetComponent<Animator> ().Play ("gameInOff", -1, 0f);
					}
				}

				if (scorePoints2 >= 0) {
					PlayGamesScript.UnlockAchievement (achievement6);
					end = 0.835;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture0;
				}
				if (scorePoints2 >= (80 + pointsdown)) {
					PlayGamesScript.UnlockAchievement (achievement7);
					end = 0.87;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture1;
				}
				if (scorePoints2 >= (240 + pointsdown)) {
					PlayGamesScript.UnlockAchievement (achievement8);
					end = 0.9035;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture2;
				}
				if (scorePoints2 >= (360 + pointsdown)) {
					PlayGamesScript.UnlockAchievement (achievement9);
					end = 0.936;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture3;
				}
				if (scorePoints2 >= (480 + pointsdown)) {
					PlayGamesScript.UnlockAchievement (achievement10);
					end = 0.969;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture4;
				}
				if (scorePoints2 >= (640 + pointsdown)) {
					PlayGamesScript.UnlockAchievement (achievement11);
					end = 0.985;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture5;
				}
				if (scorePoints2 >= (820 + pointsdown)) {
					PlayGamesScript.UnlockAchievement (achievement12);
					end = 0.997;
					touchEnd.GetComponent<Image> ().sprite = touchE_texture5;
				}
				if (scorePoints2 >= (1000 + pointsdown)) {
					PlayGamesScript.UnlockAchievement (achievement13);
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
			lostPoints = true;
			lock2 = true;
			gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
			isSecDown = false;
		}
	}

	void secCounter1(){
		secTime1 -= Time.deltaTime;
		int secTime2 = Mathf.RoundToInt(secTime1);
		Text secInside2 = featureTime.GetComponent<Text>();
		secInside2.text = secTime2.ToString() + " sec";

		if (secTime1 < 0)
		{
			secTime1 = 0;
			secInside2.text = secTime2.ToString () + " sec";
			isSecDown1 = false;
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
		if (selectOne) {
			lbt.GetComponent<Image> ().sprite = selectedTop;
			if (selected2) {
				if (s1t.GetComponent<Text> ().text == special1 + special2) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected2 = false; 
			}

			if (selected0) {
				if (s1t.GetComponent<Text> ().text == special4 + special5) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected0 = false; 
			}

			if (selected1) {
				if (s1t.GetComponent<Text> ().text == special6 + special7) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected1 = false; 
			}
			selectOne = false;
		}
	}

	public void lbbClick(){
		if (selectOne) {
			lbb.GetComponent<Image> ().sprite = selectedBottom;
			if (selected2) {
				if (s1b.GetComponent<Text> ().text == special1 + special2) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected2 = false; 
			}

			if (selected0) {
				if (s1b.GetComponent<Text> ().text == special4 + special5) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected0 = false; 
			}

			if (selected1) {
				if (s1b.GetComponent<Text> ().text == special6 + special7) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected1 = false; 
			}
			selectOne = false;
		}
	}

	public void mbtClick(){
		if (selectOne) {
			mbt.GetComponent<Image> ().sprite = selectedTop;
			if (selected2) {
				if (s2t.GetComponent<Text> ().text == special1 + special2) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected2 = false; 
			}

			if (selected0) {
				if (s2t.GetComponent<Text> ().text == special4 + special5) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected0 = false; 
			}

			if (selected1) {
				if (s2t.GetComponent<Text> ().text == special6 + special7) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected1 = false; 
			}
			selectOne = false;
		}
	}

	public void mbbClick(){
		if (selectOne) {
			mbb.GetComponent<Image> ().sprite = selectedBottom;
			if (selected2) {
				if (s2b.GetComponent<Text> ().text == special1 + special2) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected2 = false; 
			}

			if (selected0) {
				if (s2b.GetComponent<Text> ().text == special4 + special5) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected0 = false; 
			}

			if (selected1) {
				if (s2b.GetComponent<Text> ().text == special6 + special7) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected1 = false; 
			}
			selectOne = false;
		}
	}

	public void rbtClick(){
		if (selectOne) {
			rbt.GetComponent<Image> ().sprite = selectedTop;
			if (selected2) {
				if (s3t.GetComponent<Text> ().text == special1 + special2) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected2 = false; 
			}

			if (selected0) {
				if (s3t.GetComponent<Text> ().text == special4 + special5) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected0 = false; 
			}

			if (selected1) {
				if (s3t.GetComponent<Text> ().text == special6 + special7) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected1 = false; 
			}
			selectOne = false;
		}
	}

	public void rbbClick(){
		if (selectOne) {
			rbb.GetComponent<Image> ().sprite = selectedBottom;
			if (selected2) {
				if (s3b.GetComponent<Text> ().text == special1 + special2) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected2 = false; 
			}

			if (selected0) {
				if (s3b.GetComponent<Text> ().text == special4 + special5) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected0 = false; 
			}

			if (selected1) {
				if (s3b.GetComponent<Text> ().text == special6 + special7) {
					lostPoints = false;
				} else
					lostPoints = true;
				lock2 = true;
				gameInExtraCanvas.GetComponent<Animator> ().Play ("gameInExtraOff", -1, 0f);
				selected1 = false; 
			}
			selectOne = false;
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

	public void getrewardClick(){
		reward = true;
		gameInCanvas.GetComponent<Animator> ().Play ("getrewardoff");
	}

	public void cancelClick(){
		reward = false;
		gameInCanvas.GetComponent<Animator> ().Play ("getrewardoff");
	}

}
