using UnityEngine;
using System.Collections;

public class CoinTracker : MonoBehaviour {
	public bool ghostCoin = false;
	private bool disabled = false;
	private static int remainingGhostCoins;
	private static int remainingPlayerCoins;

	void Awake() {
		remainingGhostCoins = 0;
		remainingPlayerCoins = 0;
	}

	void Start() {
		if (ghostCoin) {
			remainingGhostCoins++;
		} else {
			remainingPlayerCoins++;
		}
		Debug.Log ("Remaining ghost coins: " + remainingGhostCoins + ", player coins: " + remainingPlayerCoins);

	}

//	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other){
		if (disabled) { //ensure we can only take once;
			return;
		}

		if (ghostCoin && other.tag.Equals ("Ghost")) {
			takeCoin ();
		} else if (!ghostCoin && other.tag.Equals ("Player")) {
			takeCoin ();
		}
//		Debug.Log("Something has entered this zone." + other.tag);  
	} 

	void takeCoin() {
		GameObject.Destroy (gameObject);
		disabled = true;
		if (ghostCoin) {
			remainingGhostCoins--;
		} else {
			remainingPlayerCoins--;
		}
		Debug.Log ("Remaining ghost coins: " + remainingGhostCoins + ", player coins: " + remainingPlayerCoins);
		if (remainingGhostCoins == 0 && remainingPlayerCoins == 0) {
			GameObject master = GameObject.FindGameObjectWithTag("MasterGameObject");
			master.GetComponent<GameFlowController> ().allCoinsCollected ();
		}
	}
}
