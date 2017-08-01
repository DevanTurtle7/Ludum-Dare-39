using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiMaster : MonoBehaviour {

	public GameObject player;
	public RectTransform energyBar;
	public RectTransform healthBar;

	void Update() {
		energyBar.sizeDelta = Vector2.Lerp(energyBar.sizeDelta, new Vector2(4.3f * player.GetComponent<playerScript>().energy, 20), .1f);
		healthBar.sizeDelta = Vector2.Lerp(healthBar.sizeDelta, new Vector2(4.3f * player.GetComponent<playerScript>().health, 20), .1f);
	}
}
