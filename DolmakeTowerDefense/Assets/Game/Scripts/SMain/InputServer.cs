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
		bool contact = false;
		foreach (Touch touch in Input.touches) {
			Camera[] cameras = Camera.allCameras;
			int i = cameras.Length -1;
			do
			{
				Camera cam = cameras[i];
				if (touch.phase == TouchPhase.Began)
					contact = RayForPosition (cam, touch.position);

				if ( contact ) return;
				i--;
			}while ( i >= 0);
		}
		if (Input.GetMouseButtonDown (0)) {
			Camera[] cameras = Camera.allCameras;
			int i = cameras.Length -1;
			do 
			{
				contact = RayForPosition (cameras[i], Input.mousePosition);

				if ( contact ) return;
				i--;

			}while (i >= 0);
		}
	}

	bool RayForPosition (Camera cam, Vector2 screenPos)
	{
		RaycastHit hit;
		Vector3 pos_VP = cam.ScreenToViewportPoint (screenPos);
		Ray ray = cam.ViewportPointToRay (pos_VP);
		if (Physics.Raycast (ray, out hit, cam.farClipPlane)) {			
			OnTouchCollider (hit);	
			return true;		
		}
		else return false;
	}
}
