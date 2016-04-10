using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public bool isPaused;
	float maxSpeed = 5f;

	// grounded related variables;
	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.15f;
	public LayerMask whatIsGround;
	public LayerMask whatIsGhost;

	private Rigidbody2D rb2d;
	private Animator anim;

	private PlayerScript ghostScript;

	void OnTriggerEnter (Collider other) {
		Debug.Log ("Collided");
	}

	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponentInChildren<Animator> ();
		ghostScript = GameObject.FindGameObjectWithTag ("Ghost").GetComponentInChildren<PlayerScript> ();
	}

	void Update() {
		if (isPaused) {
			return;
		}

		if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.1) {
			transform.localScale = new Vector3 (Input.GetAxis ("Horizontal") < 0 ? -1 : 1, 1, 1);
		}

		// jumping
//		if (grounded && Input.GetAxis ("Vertical") > 0.1) {
		if (grounded && Input.GetKeyDown (KeyCode.UpArrow)) {
			anim.SetBool ("Grounded", false);
			rb2d.velocity = new Vector2 (rb2d.velocity.x, 12f);

//			rb2d.AddForce (Vector2.up * jumpPower);
		}

		if (Input.GetKeyDown ("g")) {
			anim.SetTrigger ("TriggerSummon");
			Debug.Log ("Trigger summon");
		}
//		Debug.Log (Input.GetAxis ("Vertical") == 1);
		// limiting the jump:
//		if (rb2d.velocity.y > maxJumpPower) {
//			Debug.Log ("power: " + rb2d.velocity.y);
//			rb2d.velocity = new Vector2 (rb2d.velocity.x, maxJumpPower);
//		}
	}
		
	void FixedUpdate () {
		//check if grounded: 
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		if (!grounded) {
			grounded = (Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGhost) && ghostScript.isPaused);
		}
		anim.SetBool ("Grounded", grounded);

//		Debug.Log ("Grounded: " + grounded);
		if (isPaused) {
			return;
		}

		float move = Input.GetAxis ("Horizontal");
		// trigger animation
		anim.SetFloat ("speed", Mathf.Abs(move));
	
		// moving the player
		rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);


	}

	public void togglePause(bool pause) {
		this.isPaused = pause;
		if (pause) {
			rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
		} else {
			rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}
