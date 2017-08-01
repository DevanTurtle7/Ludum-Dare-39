using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScript : MonoBehaviour {

	public CanvasGroup fadeObject;

	private IEnumerator coroutine;

	void Start() {
		coroutine = fade ();
		StartCoroutine (coroutine);
	}

	private IEnumerator fade() {
		while (true) {
			if (Input.GetAxis ("Vertical") > 0) {
				for (float i = 0f; i < 1f; i += .01f) {
					fadeObject.alpha = i;
					yield return new WaitForSeconds (.01f);
				}
				Application.LoadLevel ("game");
				Application.UnloadLevel ("tutorial");
			}
			yield return new WaitForSeconds (.01f);
		}
	}
}
