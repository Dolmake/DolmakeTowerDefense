using UnityEngine;
using System.Collections;

public class InputServer : MonoBehaviour {

	static InputServer _instance = null;
	public static InputServer SINGLETON {
		get {
			return _instance;
		}
	}
	
	void Awake ()
	{
		_instance = this;
		Input.simulateMouseWithTouches = true;
	}

	public event System.Action<RaycastHit> OnTouchCollider;



	void Update ()
	{
		foreach (Touch touch in Input.touches) {
			foreach (Camera cam in Camera.allCameras) {
				if (touch.phase == TouchPhase.Began)
					RayForPosition (cam, touch.position);
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			foreach (Camera cam in Camera.allCameras) {
				RayForPosition (cam, Input.mousePosition);
			}
		}
	}

	void RayForPosition (Camera cam, Vector2 screenPos)
	{
		RaycastHit hit;
		Vector3 pos_VP = cam.ScreenToViewportPoint (screenPos);
		Ray ray = cam.ViewportPointToRay (pos_VP);
		if (Physics.Raycast (ray, out hit, cam.farClipPlane)) {			
			OnTouchCollider (hit);			
		}
	}
}
