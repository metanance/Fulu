using UnityEngine;
using System.Collections;

public class BlackHoleDetector : MonoBehaviour {
	private bool disabled = false;

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other){
		if (disabled) { //ensure we can only take once;
			return;
		}

		if (other.tag.Equals ("Player")) {
			disabled = true;
			GameObject master = GameObject.FindGameObjectWithTag("MasterGameObject");
			master.GetComponent<GameFlowController> ().levelComplete();
		}
	}
}
