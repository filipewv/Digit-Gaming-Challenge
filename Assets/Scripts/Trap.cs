using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
	public Transform rangeA, rangeB;
	public bool up = false;
	public float speed = 2;

	void Start(){
		if (up) {
			transform.position = new Vector2 (transform.position.x, rangeB.position.y);
		}
	}
	// Update is called once per frame
	void Update () {
		// Controller of trap movement
		if (transform.position.y <= rangeA.position.y) {
			up = true;
		} else if (transform.position.y >= rangeB.position.y) {
			up = false;
		}

		if (up) {
			transform.Translate (Vector2.up * Time.deltaTime * speed);	
		} else {
			transform.Translate(Vector2.down * Time.deltaTime * speed);
		}

	}
}
