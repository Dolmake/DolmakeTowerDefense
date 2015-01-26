using UnityEngine;
using System.Collections;

namespace SMain.Input
{
    public struct Press
    {
        public TouchPhase phase;
        public int fingerId;
        public Vector2 Position2D;

        public void Initialize()
        {
            phase = TouchPhase.Canceled;
        }
       
        public void UpdateWithTouch(Touch touch)
        {
            this.fingerId = touch.fingerId;          
            phase = touch.phase;

            this.Position2D = touch.position;
        }

        public void UpdateWithMouse()
        {
            this.fingerId =0;
            
            phase = UnityEngine.Input.GetMouseButtonDown(0) ? TouchPhase.Began
                : UnityEngine.Input.GetMouseButtonUp(0) ? TouchPhase.Ended:
                UnityEngine.Input.GetMouseButton(0)? TouchPhase.Moved :
                TouchPhase.Canceled;

           
            this.Position2D.x = UnityEngine.Input.mousePosition.x;
            this.Position2D.y = UnityEngine.Input.mousePosition.y;
        }





       
    }
}
