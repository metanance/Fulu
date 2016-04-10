using UnityEngine;
using System.Collections;
// SCRIPT FROM: https://www.youtube.com/watch?v=5E5_Fquw7BM
public class Parallaxing : MonoBehaviour {
	float smoothing = 100f; // parallax amount
	public Transform[] layers;

	private float[] parallaxScales; // proportion of cams movement to move background by
	private Transform cam;
	private Vector3 previousCamPos;

	void Awake () {
//		Camera mycam = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>();
		cam = Camera.main.transform;
			
	}

	// Use this for initialization
	void Start () {
		previousCamPos = cam.position;

		parallaxScales = new float[layers.Length];
		for (int i = 0; i < layers.Length; i++) {
			
//			parallaxScales [i] = (100-layers [i].position.z) * -1;
			parallaxScales [i] = layers [i].position.z/-100;
//			Debug.Log ("parallaxScales[" + i + "] for position "+layers[i].position.z+": " + parallaxScales [i]);
		} 
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < layers.Length; i++) {
			float camDiff = previousCamPos.x - cam.position.x;
			float parallax = camDiff * parallaxScales [i];

			float backgroundTargetPosX = (layers [i].position.x + parallax);

//			if (camDiff != 0) {
//				Debug.Log ("camDiff: " + camDiff + "\n" +
//					"parallaxScales[" + i + "]:  " + parallaxScales [i] + "\n" +
//					"parallax: " + parallax + "\n" + 
//					"layers [" + i + "].position.x: " + layers [i].position.x + "\n" +
//					"backgroundTargetPosX: " + backgroundTargetPosX + "\n" + 
//					"campos: " + cam.position.x);
//			}
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, layers [i].position.y, layers [i].position.z);

			layers [i].position = Vector3.Lerp (layers [i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		previousCamPos = cam.position;
//		Debug.Log ("previous cam pos set to: " + previousCamPos.x);
	
	}
}
