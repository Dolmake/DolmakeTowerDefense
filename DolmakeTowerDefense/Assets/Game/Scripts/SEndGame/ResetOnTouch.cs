using UnityEngine;
using System.Collections;
using Scripts.SMain;

public class ResetOnTouch : MonoBehaviour {

    public string SceneToGo;

	/*
    void OnEnable()
    {
        InputServer.SINGLETON.OnRayAtPress += SINGLETON_OnRayAtPress;
    }
    
    void OnDisable()
    {
        InputServer.SINGLETON.OnRayAtPress -= SINGLETON_OnRayAtPress;
    }

    void SINGLETON_OnRayAtPress(ref RaycastHit const_hit, Press touch, Camera camera, ref Ray const_ray)
    {
        Core.SINGLETON.GoBack(SceneToGo);
    }  

*/
	void Update()
	{
		if (Input.GetMouseButton(0)){
			Core.SINGLETON.GoBack(SceneToGo);
		}
	}
}
