using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudVariables : MonoBehaviour {

	// public static int mode 0, sound 1, notification 2, quickstart 3, speed 4, extraLife 5, size 6, bestScore 7;
	public static int[] ImportantValues = new int[8];

	public static void SetImportantValues(int index, int value)
	{
		ImportantValues[index] = value;
	}

}
