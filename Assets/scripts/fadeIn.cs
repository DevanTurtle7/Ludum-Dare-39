using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeIn : MonoBehaviour {

	public CanvasGroup fadeObject;

	private IEnumerator coroutine;

	void Start() {
		coroutine = fade ();
		StartCoroutine (coroutine);
	}

	private IEnumerator fade() {
		for (float i = 1f; i > 0f; i -= .01f) {
			fadeObject.alpha = i;
			yield return new WaitForSeconds (.01f);
		}
	}
}
