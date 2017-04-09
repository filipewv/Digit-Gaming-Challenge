using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensor : MonoBehaviour {
	public PlayerMovements player;
	public string tagHit;
	public bool colInfo;
	//public Text txt;

	void Start () {
		// Get player script to send info
		player = GameObject.Find("Player").GetComponent<PlayerMovements>();
	}

	void OnTriggerEnter2D(Collider2D other){
		// pass the sensor collider to AI manage
		if (other.tag != "Untagged") {
			tagHit = other.tag;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag != "Untagged") {
			// pass the sensor collider to AI ( specific to trap )
			if (other.tag == "Trap") {
				colInfo = other.gameObject.GetComponent<Trap>().up;
			}	
		}
	}

	void OnTriggerExit2D(Collider2D other){
		// Clear sensors
		if (other.tag == tagHit) {
			tagHit = "";
		}
	}
}
