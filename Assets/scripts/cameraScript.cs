using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {
	public Transform target;
	public float smoothTime = .3f;
	private Vector3 velocity = Vector3.zero;

	void Update() {
		Vector3 targetPosition = target.transform.position + new Vector3 (0, 0, -10);
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime);
	}
}