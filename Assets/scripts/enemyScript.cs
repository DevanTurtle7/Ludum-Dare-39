using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	public float speed = 3f;
	public GameObject player;
	public Rigidbody2D enemyRigidbody;
	public SpriteRenderer enemySprite;
	public float health = 100f;
	public Animator enemyAnimator;
	public Object self;
	public float enemyKillPoints = 50f;

	private bool attackDebounce = false;
	private Object closestPlatform;
	private IEnumerator coroutine;
	private bool colliding;
	private IEnumerator coroutine2;
	private bool dying = false;

	void Start() {
		coroutine2 = resetCollisions ();
		StartCoroutine (coroutine2);
	}

	void Update() {
		//print ("on same plane");
		//find player

		if (health > 0) {
			if (player.transform.position.x - transform.position.x > transform.position.x - player.transform.position.x) {
				//	print ("moving towards player");
				enemyRigidbody.velocity = new Vector2 (speed, enemyRigidbody.velocity.y);
				enemySprite.flipX = true;
			} else {
				enemyRigidbody.velocity = new Vector2 (-speed, enemyRigidbody.velocity.y);
				enemySprite.flipX = false;
			}
		} else {
			if (!dying) {
				enemyAnimator.SetInteger ("state", 1);
				dying = true;
				coroutine = die ();
				StartCoroutine (coroutine);
			}
		}

		if (colliding) {
			if (player.gameObject.GetComponent<playerScript> ().isAttacking == true) {
				if (!attackDebounce) {
					attackDebounce = true;
					health -= player.GetComponent<playerScript> ().attackDamage;
					resetCollisions ();
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "attack") {
			colliding = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == "attack") {
			colliding = false;
		}
	}

	private IEnumerator die() {
		player.GetComponent<playerScript> ().energy += 25f;
		player.GetComponent<playerScript> ().score += enemyKillPoints;
		yield return new WaitForSeconds (1f);
		Destroy (self);
	}

	private IEnumerator resetCollisions() {
		while (true) {
			if (colliding) {
				yield return new WaitForSeconds (1f);
				attackDebounce = false;
			}
			yield return new WaitForSeconds (.1f);
		}
	}
}
