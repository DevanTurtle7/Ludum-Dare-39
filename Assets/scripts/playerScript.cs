//Devan Kavalchek and Melissa Kazazic
//Ludum Dare 39: Running out of power

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour {
	public GameObject player;
	public Rigidbody2D playerRB;
	public float speed = 5f;
	public float jumpSpeed = 5f;
	public Animator controller;
	public GameObject rightLight;
	public GameObject leftLight;
	public Animator attackController;
	public float energy = 100f;
	public float attackCost = 15f;
	public float health = 100;
	public float attackDamage = 50;
	public bool isAttacking = false;
	public CanvasGroup fadeUI;
	public GameObject lightDataObject;
	public CanvasGroup gameOverText;
	public float enemyDamage = 20f;
	public float score = 0;
	public GameObject scoreLabel;

	public bool isGrounded = false;
	private IEnumerator coroutine;
	private IEnumerator coroutine2;
	private IEnumerator coroutine3;
	private IEnumerator coroutine4;
	private IEnumerator coroutine5;
	public GameObject[] currentCollisions;
	private bool beingAttacked = false;

	//-.472, .26, 1
	//.55, .26, 1

	void Start() {
		coroutine = attack ();
		StartCoroutine (coroutine);
		coroutine4 = enemyAttack ();
		StartCoroutine (coroutine4);
		coroutine5 = timeScore ();
		StartCoroutine (coroutine5);
		currentCollisions = new GameObject[0];
	}

	void Update() {
		if (currentCollisions.Length > 0) {
			if (currentCollisions [0] != null) {
				isGrounded = true;
			} else {
				isGrounded = false;
			}
		} else {
			isGrounded = false;
		}

		scoreLabel.GetComponent<Text>().text = score.ToString();

		if (health > 0) {
			playerRB.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), playerRB.velocity.y);

			if (Input.GetAxis ("Horizontal") < 0) {
				player.GetComponent<SpriteRenderer> ().flipX = true;
				if (controller.GetInteger ("state") != 2 && controller.GetInteger ("state") != 4) {
					controller.SetInteger ("state", 1);
				}
				leftLight.GetComponent<Light> ().enabled = true;
				rightLight.GetComponent<Light> ().enabled = false;
			} else if (Input.GetAxis ("Horizontal") > 0) {
				player.GetComponent<SpriteRenderer> ().flipX = false;
				if (controller.GetInteger ("state") != 2 && controller.GetInteger ("state") != 4) {
					controller.SetInteger ("state", 1);
				}
				leftLight.GetComponent<Light> ().enabled = false;
				rightLight.GetComponent<Light> ().enabled = true;
			} else {
				if (controller.GetInteger ("state") != 2 && controller.GetInteger ("state") != 4) {
					controller.SetInteger ("state", 0);
				}
			}

			if (Input.GetAxis ("Vertical") > 0 && isGrounded) {
				isGrounded = false;
				playerRB.velocity = new Vector2 (playerRB.velocity.x, jumpSpeed);
				controller.SetInteger ("state", 2);
			}
		} else {
			coroutine2 = die ();
			StartCoroutine (coroutine2);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		controller.SetInteger ("state", 1);
		pushArray(collision.gameObject, true);
	}

	void OnCollisionExit2D(Collision2D collision) {
		pushArray(collision.gameObject, false);
	}

	private IEnumerator attack() {
		while (true) {
			if (Input.GetButtonDown ("Attack") && energy >= attackCost && health > 0) {
				energy -= attackCost;
				attackController.SetInteger ("state", 1);
				controller.SetInteger ("state", 4);
				isAttacking = true;
				yield return new WaitForSeconds (.5f);
				attackController.SetInteger ("state", 0);
				controller.SetInteger ("state", 0);
				isAttacking = false;
			}
			yield return new WaitForSeconds (.1f);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "enemy") {
			collider.gameObject.GetComponent<Animator> ().SetInteger ("state", 2);
			beingAttacked = true;
		} else if (collider.gameObject.tag == "electric") {
			energy -= 10f;
			lightDataObject.GetComponent<lightData>().lightLevel -= .1f;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == "enemy") {
			beingAttacked = false;
			collider.gameObject.GetComponent<Animator> ().SetInteger ("state", 0);
		}
	}

	private IEnumerator enemyAttack() {
		while (true) {
			while (beingAttacked) {
				health -= enemyDamage;
				print ("damaged");
				yield return new WaitForSeconds (1f);
			}
			yield return new WaitForSeconds (.1f);
		}
	}

	private IEnumerator die() {
		controller.SetInteger ("state", 3);
		playerRB.velocity = new Vector2 (0, playerRB.velocity.y);
		playerRB.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
		for (float i = 0; i < 1; i += .01f) {
			if (fadeUI.alpha < i) {
				fadeUI.alpha = i;
			}
			gameOverText.alpha = i;
			yield return new WaitForSeconds (.01f);
		}
		yield return new WaitForSeconds (3f);
		Application.LoadLevel ("game");
	}

	void pushArray(GameObject obj, bool push) {
		if (push == true) {
			if (currentCollisions.Length > 0) {
				if (currentCollisions [0] != null) {
					GameObject[] tempList = new GameObject[currentCollisions.Length + 1];

					for (int i = 0; i < currentCollisions.Length; i += 1) {
						tempList [i] = currentCollisions [i];
					}

					tempList [tempList.Length - 1] = obj;
					currentCollisions = tempList;
				} else {
					currentCollisions = new GameObject[2];
					currentCollisions [0] = obj;
				}
			} else {
				currentCollisions = new GameObject[2];
				currentCollisions [0] = obj;
			}
		} else {
			if (currentCollisions.Length > 0) {
				if (currentCollisions [0] != null) {
					GameObject[] tempList = new GameObject[currentCollisions.Length - 1];
					bool found = false;

					for (int i = 0; i < currentCollisions.Length; i += 1) {
						if (currentCollisions [i] != obj) {
							if (found) {
								tempList [i - 1] = currentCollisions [i];
							} else {
								tempList [i] = currentCollisions [i];
							}
						} else {
							found = true;
						}
					}
					currentCollisions = tempList;
				}
			}
		}
	}

	private IEnumerator timeScore() {
		while (true) {
			yield return new WaitForSeconds (1f);
			score += 1f;
		}
	}
}
