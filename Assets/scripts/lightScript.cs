//Devan Kavalchek and Melissa Kazazic
//Ludum Dare 39: Running out of power

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightScript : MonoBehaviour {

	public Light light;

	private float energy = 100f;
	private IEnumerator coroutine;

	void Start() {
		coroutine = lightsDown ();
		StartCoroutine (coroutine);
	}

	private IEnumerator lightsDown() {
		while (true) {
			light.intensity = (energy / 10);
			print (energy / 10);
			energy -= 1;

			yield return new WaitForSeconds (.1f);
		}
	}
}
