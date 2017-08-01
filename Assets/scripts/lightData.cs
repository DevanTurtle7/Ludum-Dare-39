using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightData : MonoBehaviour {

	public float lightLevel = 0f;
	public float subtraction = .01f;
	public float waitTime = 1f;

	private IEnumerator coroutine;

	void Start() {
		coroutine = fadeLight ();
		StartCoroutine (coroutine);
	}

	private IEnumerator fadeLight() {
		while (true) {
			yield return new WaitForSeconds (waitTime);
			lightLevel += subtraction;
		}
	}
}
