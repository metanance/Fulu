using UnityEngine;
using System.Collections;

public class GhostControlScript : MonoBehaviour {
	PlayerScript ghostScript;

	bool ghostPaused;

	// Use this for initialization
	void Start () {
		ghostPaused = false;
		ghostScript = gameObject.GetComponent<PlayerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("g")) {
			ghostPaused = !ghostPaused;
			ghostScript.togglePause(ghostPaused);
		}
	}

	public void pauseGhost() {
		ghostPaused = true;
		ghostScript.togglePause(ghostPaused);
	}
}
