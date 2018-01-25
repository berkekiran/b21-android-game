using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paused : MonoBehaviour {

	public GameObject pausedCanvas;

	void Start () {
		pausedCanvas.GetComponent<Animator> ().Play ("pausedOn", -1, 0f);
	}
	
	public void pausedClick(){
		pausedCanvas.GetComponent<Animator> ().Play ("pausedOff", -1, 0f);

	}
}
