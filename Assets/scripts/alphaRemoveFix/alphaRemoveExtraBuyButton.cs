using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class alphaRemoveExtraBuyButton : MonoBehaviour {
	
	public float AlphaThreshold = 0.1f;

	void Start()
	{
		this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
