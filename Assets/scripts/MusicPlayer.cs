using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
	
public class MusicPlayer : MonoBehaviour {

	private static MusicPlayer _instance ;
	public AudioClip Music;
	AudioSource audioSource;
	private float currentTime = 4f;
	private bool isCountingDown = false, lock0 = true, isCountingDown1 = false, lock01 = true;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = Music;
		audioSource.Play();
		audioSource.loop = true;
	}

	void Update(){
		Scene currentScene = SceneManager.GetActiveScene ();
		int buildIndex = currentScene.buildIndex;

		if (buildIndex == 4 && lock0) {
			isCountingDown = true;
			lock01 = true;
		}
		else if (buildIndex != 4 && lock01) {
			isCountingDown1 = true;
			lock0 = true;
		}
		if (isCountingDown)
			UpdateCounter ();

		if (isCountingDown1)
			UpdateCounter1 ();
	}

	void UpdateCounter()
	{
		currentTime -= Time.deltaTime;

		if (audioSource.volume >= 0.24f)
			audioSource.volume -= 0.01f;
		
		if (currentTime < 0)
		{
			audioSource.volume = 0.24f;
			currentTime = 0;
			isCountingDown = false;
			lock0 = false;
		}
	}

	void UpdateCounter1()
	{
		currentTime -= Time.deltaTime;

		if (audioSource.volume <= 1f)
			audioSource.volume += 0.01f;

		if (currentTime < 0)
		{
			audioSource.volume = 1f;
			currentTime = 0;
			isCountingDown1 = false;
			lock01 = false;
		}
	}

	void Awake()
	{
		if(!_instance)
			_instance = this ;
		else
			Destroy(this.gameObject) ;

		DontDestroyOnLoad(this.gameObject) ;
	}

}
