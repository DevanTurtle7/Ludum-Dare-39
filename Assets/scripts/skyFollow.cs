using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyFollow : MonoBehaviour {

	public GameObject camera;

	void Update() {
		transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y, transform.position.z);
	}
}
