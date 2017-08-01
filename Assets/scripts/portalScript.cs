using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalScript : MonoBehaviour {

	public GameObject[] enemies;
	public GameObject player;

	private IEnumerator coroutine;

	void Start() {
		coroutine = spawnEnemy ();
		StartCoroutine (coroutine);
	}

	private IEnumerator spawnEnemy() {
		while (true) {
			yield return new WaitForSeconds (Random.Range (5, 10));
			GameObject newEnemy = Instantiate (enemies [Random.Range (0, enemies.Length - 1)]);
			newEnemy.GetComponent<enemyScript> ().player = player;
		}
	}
}
