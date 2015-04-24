using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

namespace SGame
{
    public class DrawInputOnTouch : MonoBehaviour
    {

        public GameObject PrefabTouchPointer;

		/*

        GameObjectPool _pointers = new GameObjectPool();
        Dictionary<int, GameObject> _pointerDic = new Dictionary<int, GameObject>();

        void Awake()
        {
            _pointers.Initialize(this.transform, PrefabTouchPointer, 3);
        }

        void OnDestroy()
        {
            _pointers.Deinitialize();
            _pointerDic.Clear();
        }


        void OnEnable()
        {           
            InputServer.SINGLETON.OnRayWhilePress += new InputServer.InputRaycasthitHandler(SINGLETON_OnRayWhilePress);
            InputServer.SINGLETON.OnRayAtLeave += new InputServer.InputRaycasthitHandler(SINGLETON_OnRayAtLeave);
        }

        void SINGLETON_OnRayAtLeave(ref RaycastHit const_hit, Press touch, Camera camera, ref Ray const_ray)
        {
            //Debug.Log("leave");
            if (_pointerDic.ContainsKey(touch.fingerId))
            {
                _pointerDic[touch.fingerId].PoolRelease();
                _pointerDic.Remove(touch.fingerId);
            }
        }

        void SINGLETON_OnRayWhilePress(ref RaycastHit const_hit, Press touch, Camera camera, ref Ray const_ray)
        {
            //Debug.Log("SINGLETON_OnRayWhilePress");
            if (!_pointerDic.ContainsKey(touch.fingerId))
            {
                _pointerDic.Add(touch.fingerId, _pointers.Get());                
            }
            UpdatePointer(_pointerDic[touch.fingerId],ref const_hit);
        }

        void OnDisable()
        {          
            InputServer.SINGLETON.OnRayWhilePress -= new InputServer.InputRaycasthitHandler(SINGLETON_OnRayWhilePress);
            InputServer.SINGLETON.OnRayAtLeave -= new InputServer.InputRaycasthitHandler(SINGLETON_OnRayAtLeave);
        }

       

        private void UpdatePointer(GameObject o, ref RaycastHit const_hit)
        {
            //Debug.Log("UpdatePointer");
            o.transform.position = const_hit.point;
        }
        */

    }
}
