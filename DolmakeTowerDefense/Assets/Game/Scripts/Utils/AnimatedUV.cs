using UnityEngine;
using System.Collections;


public class AnimatedUV : MonoBehaviour {

    public Vector2 Mask = new Vector2(1, 1);
	
	// Update is called once per frame
	void Update () {
        Vector2 uv = this.GetComponent<Renderer>().material.mainTextureOffset;
        uv.x += Time.deltaTime * (1f - Mask.x);
        uv.y += Time.deltaTime * (1f - Mask.y);
        uv.x += uv.x > 1f ? -1f : 0f;
        uv.y += uv.y > 1f ? -1f : 0f;
        this.GetComponent<Renderer>().material.mainTextureOffset = uv;
	}
}
