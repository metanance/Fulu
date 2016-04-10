using UnityEngine;
using System.Collections;

public class GameFlowController : MonoBehaviour {
	GameObject blackHole;
	public int currentLevel;
	static int numLevels = 5;

	// Use this for initialization
	void Start () {
		blackHole = GameObject.FindGameObjectWithTag ("ExitHole");
		blackHole.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("r")) {
			Application.LoadLevel ("level0" + currentLevel);
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void allCoinsCollected() {
		Debug.Log ("All coins collected");
		blackHole.SetActive (true);
	}

	public void levelComplete() {
		Debug.Log ("Level complete");
		if (currentLevel + 1 > numLevels) {
			Application.LoadLevel ("Won");
		} else {
			Application.LoadLevel ("level" + (currentLevel + 1));
		}
	}
}
