using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {
	//private base vars
	private bool moveR, moveL, moveRPlat, trap = false;
	private bool jump = true;
	private bool jumping = false;
	private Rigidbody2D rb;
	//public base vars
	public float speed, jumpPower, jumpColdown = 1;
	public GameObject plataformLeft;
	public Sensor sensLt, sensT, sensRt, sensR, sensRb, sensB, sensLb, sensL, sensFoot;
	public Animator animator;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		moveR = true;
	}

	void FixedUpdate () {
		// Movement controller
		MoveController();
		CharacterAI();
		JumpColdown ();
		//Jump ("right");
		if (Input.GetKey("escape"))
			Application.Quit();
		
	}

	private void MoveController(){
		if (!jumping) {
			// move to Right
			if (moveR) {
				transform.Translate ((Vector2.right * speed) * Time.deltaTime);
				animator.SetBool ("walk", true);
				animator.SetBool ("iddle", false);
			} else {
				animator.SetBool ("iddle", true);
				animator.SetBool ("walk", false);
			}
		}
	}

	private void JumpColdown(){
		// if player in some of these tags he can jump
		if (jumpColdown == 0 && (sensFoot.tagHit == "Ground" || sensFoot.tagHit == "Plataform" || sensFoot.tagHit == "PlataformFix")) {
			jump = true;
			jumping = false;
		}
	}

	private void CountDown(){
		jumpColdown = 0;
	}


	private void Jump(string side = ""){
		// verify if jump coldown complete and if player in ground or platform
		if (jump && !jumping && (sensFoot.tagHit == "Ground" || sensFoot.tagHit == "Plataform" || sensFoot.tagHit == "PlataformFix") && (sensB.tagHit == "Hole" || sensB.tagHit == "Plataform" || sensB.tagHit == "PlataformFix" || sensFoot.tagHit == "Ground")) {
			jumping = true;
			if (side == "right") {
				//set anim state
				animator.SetTrigger ("jump");
				animator.SetBool ("walk", false);
				// Add force to Jump
				rb.AddForce (Vector2.up * (jumpPower * 10000) * Time.deltaTime);	
				rb.AddForce (Vector2.right * (jumpPower * 4500) * Time.deltaTime);
				// Set coldown of jump and call his funtion
				jumpColdown = 1;
				Invoke ("CountDown", 1);
			}
			jump = false;
		}
	}

	public void CharacterAI(){
		if (sensL.tagHit == "Spin") {
			jumping = false;
			moveR = false;
			Jump ("right");
		}
		// if player not in a trap and inside of a ground or plataform he can walk 
		if ( sensB.tagHit == "Ground" || sensB.tagHit == "PlataformFix" || sensB.tagHit == "Plataform") {
			if (!trap) {
				moveR = true;
			}
			jumping = false;
		}else if (sensB.tagHit == "Hole") {
			// if player close enought of Hole he stop walking
			moveR = false;
			if (sensRb.tagHit == "Ground") {
				// if ground close enought to jump
				Jump ("right");
			} else if (sensR.tagHit == "PlataformFix" || sensRb.tagHit == "PlataformFix") {
				// if Plataform close enought to jump
				Jump ("right");
			} else {
				Jump ("right");
			}
		}
		if (sensRt.tagHit == "Trap") {
			// Verify if player close of a trap
			trap = true;
			if (sensRt.colInfo && sensL.tagHit != "Trap") {
				// safe you can go
				moveR = true;
				trap = false;
			} else {
				// stop until is safe
				trap = false;
				moveR = false;
			}
		}
	}


}
