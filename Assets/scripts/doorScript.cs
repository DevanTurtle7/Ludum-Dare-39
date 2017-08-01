using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour {

	public bool doorOpen = false;
	public Sprite doorOpenSprite;

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			if (!doorOpen) {
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
				this.GetComponent<SpriteRenderer>().sprite = doorOpenSprite;
				this.GetComponent<SpriteRenderer> ().flipX = true;
				doorOpen = true;
			}
		}
	}
}
