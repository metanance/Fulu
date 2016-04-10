using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	private Vector2 velocity;
	private float smoothTime;
	private float viewSwitchTimeLeft;
	private bool singleView;

	private float zoomVelocity;
	public float zoomSmoothTime = 0.1f;

	private float buffer = 0.9f;
	private float minSize, maxSize;

	private GameObject player;
	private GameObject ghost;

	public enum CameraFollowTypes { cameraZoom, semiFollowBoth };
	public CameraFollowTypes followType; // = CameraFollowTypes.semiFollowBoth;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		ghost = GameObject.FindGameObjectWithTag ("Ghost");
		singleView = false;
		minSize = Camera.main.orthographicSize;
		maxSize = Camera.main.orthographicSize * 200f;
	}

	void FixedUpdate () {
		if (followType == CameraFollowTypes.semiFollowBoth) {
			updateSemiFollowBoth ();
		} else {
			updateCameraZoom ();
		}
	}

	void updateSemiFollowBoth() {
		Vector2 playerDists = new Vector2 (Mathf.Abs (player.transform.position.x - ghost.transform.position.x),
										   Mathf.Abs (player.transform.position.y - ghost.transform.position.y));

		float middlePointX = (player.transform.position.x + ghost.transform.position.x) / 2;
		float middlePointY = (player.transform.position.y + ghost.transform.position.y) / 2;
		Vector2 desiredCameraCenter = new Vector2 (middlePointX, middlePointY);
		smoothTime = 0.05f;

		Vector2 currentCamSize = new Vector2 (Camera.main.orthographicSize * 2f * Camera.main.aspect,
			Camera.main.orthographicSize * 2f);

		if (currentCamSize.x * buffer < playerDists.x || currentCamSize.y * buffer < playerDists.y) {
			if (!singleView) { // trigger switch to single player view
				viewSwitchTimeLeft = 2f;
				singleView = true;
				GhostControlScript s = ghost.GetComponent<GhostControlScript> ();
				s.pauseGhost ();
			}
			desiredCameraCenter = player.transform.position;	
		} else if (singleView) {
			viewSwitchTimeLeft = 2f;
			singleView = false;
		}

		if (viewSwitchTimeLeft > 0) {
			smoothTime = 0.4f;
			viewSwitchTimeLeft -= Time.deltaTime;
		}

		float posX = Mathf.SmoothDamp (transform.position.x, desiredCameraCenter.x, ref velocity.x, smoothTime);
		float posY = Mathf.SmoothDamp (transform.position.y, desiredCameraCenter.y-0.5f, ref velocity.y, smoothTime);

		transform.position = new Vector3 (posX, posY, transform.position.z);
	}

	void updateCameraZoom() {
		Vector2 playerDists = new Vector2 (
			Mathf.Abs (player.transform.position.x - ghost.transform.position.x),
			Mathf.Abs (player.transform.position.y - ghost.transform.position.y));


		//		Debug.Log ("player.transform.position.x: " + player.transform.position.x);
		float middlePointX = (player.transform.position.x + ghost.transform.position.x) / 2;
		float middlePointY = (player.transform.position.y + ghost.transform.position.y) / 2;
		Vector2 desiredCameraCenter = new Vector2 (middlePointX, middlePointY);
		smoothTime = 0.05f;

		Vector2 currentCamSize = new Vector2 (Camera.main.orthographicSize * 2f * Camera.main.aspect,
			Camera.main.orthographicSize * 2f);

//			if (currentCamSize.x * buffer < playerDists.x || currentCamSize.y * buffer < playerDists.y) {
		if (currentCamSize.x * buffer < playerDists.x) { // || currentCamSize.y * buffer < playerDists.y) {
			float zoom = Mathf.SmoothDamp (Camera.main.orthographicSize, maxSize, ref zoomVelocity, zoomSmoothTime);
			Camera.main.orthographicSize = zoom;
		} else if (currentCamSize.x * (2.5-buffer) > playerDists.x) { // || currentCamSize.y * (0.9f - buffer) > playerDists.y) {
			float zoom = Mathf.SmoothDamp (Camera.main.orthographicSize, minSize, ref zoomVelocity, zoomSmoothTime);
			Camera.main.orthographicSize = zoom;
		}

//		if (currentCamSize.x * buffer < playerDists.x || currentCamSize.y * buffer < playerDists.y) {
//			if (!singleView) { // trigger switch to single player view
//				viewSwitchTimeLeft = 2f;
//				singleView = true;
//				GhostControlScript s = ghost.GetComponent<GhostControlScript> ();
//				s.pauseGhost ();
//			}
//			desiredCameraCenter = player.transform.position;	
//		} else if (singleView) {
//			viewSwitchTimeLeft = 2f;
//			singleView = false;
//		}
//
//		if (viewSwitchTimeLeft > 0) {
//			smoothTime = 0.4f;
//			viewSwitchTimeLeft -= Time.deltaTime;
//		}

		//		Debug.Log("timeleft" + viewSwitchTimeLeft);

		//		Vector2 desiredCameraCenter = new Vector2 (middlePointX, middlePointY);
		float posX = Mathf.SmoothDamp (transform.position.x, desiredCameraCenter.x, ref velocity.x, smoothTime);
		float posY = Mathf.SmoothDamp (transform.position.y, desiredCameraCenter.y-0.5f, ref velocity.y, smoothTime);

		transform.position = new Vector3 (posX, posY, transform.position.z);
	}
}
