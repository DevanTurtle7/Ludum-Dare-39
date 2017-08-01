using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlicker : MonoBehaviour {

	public GameObject text;
	public GameObject textGlow;
	public GameObject unlitText;
	public GameObject subTitle;
	public GameObject subTitleGlow;
	public GameObject unlitSubTitle;
	public Light blueLight;
	public AudioSource neonSound;

	private IEnumerator coroutine;

	void Start() {
		coroutine = flicker ();
		StartCoroutine (coroutine);
	}

	private IEnumerator flicker() {
		while (true) {
			yield return new WaitForSeconds (Random.Range (.1f, 5f));
			text.SetActive (false);
			textGlow.SetActive (false);
			unlitText.SetActive (true);
			subTitle.SetActive (false);
			subTitleGlow.SetActive (false);
			unlitSubTitle.SetActive (true);
			neonSound.enabled = false;
			blueLight.intensity = .2f;
			yield return new WaitForSeconds (.1f);
			text.SetActive (true);
			textGlow.SetActive (true);
			unlitText.SetActive (false);
			subTitle.SetActive (true);
			subTitleGlow.SetActive (true);
			unlitSubTitle.SetActive (false);
			neonSound.enabled = true;
			blueLight.intensity = .4f;
		}
	}
}
