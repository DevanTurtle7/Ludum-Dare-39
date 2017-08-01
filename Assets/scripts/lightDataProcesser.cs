using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightDataProcesser : MonoBehaviour {

	public GameObject lightDataObject;

	void Update() {
		GetComponent<CanvasGroup>().alpha = lightDataObject.GetComponent<lightData> ().lightLevel;
	}
}
