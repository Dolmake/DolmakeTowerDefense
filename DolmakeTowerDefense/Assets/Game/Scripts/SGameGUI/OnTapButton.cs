using UnityEngine;
using System.Collections;

public class OnTapButton : MonoBehaviour {

	void OnEnable()
    {
        InputServer.SINGLETON.OnTouchCollider += HandleOnTouchCollider;
    }

    void OnDisable ()
	{
		InputServer.SINGLETON.OnTouchCollider -= HandleOnTouchCollider;
	}

	void HandleOnTouchCollider (RaycastHit obj)
	{
		if (obj.collider.transform == this.transform) { //Is me
			Debug.Log("Button tapped");
		}
	}
}
