using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	public string lvl = "level2";
	void OnTriggerEnter2D(Collider2D other){
		// when player touch portal, move to 'lvl'
		if (other.tag == "PlayerBody") {
			Application.LoadLevel(lvl);
		}
	}
}
