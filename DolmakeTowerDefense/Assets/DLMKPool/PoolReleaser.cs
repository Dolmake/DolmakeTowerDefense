using UnityEngine;
using System.Collections;


namespace DLMKPool
{
    public class PoolReleaser : MonoBehaviour
    {
        //Delegate Pattern
    
        public delegate bool ReleaseDelegate(object o);
        public ReleaseDelegate OnReleaseDelegate;

        object _parameters;
        public void mSetDelegate(ReleaseDelegate realaseMethod, object parameters)
        {
            _parameters = parameters;
            OnReleaseDelegate = realaseMethod; 
        }

        //End Delegate Pattern

        System.Action<object> _callback;
        object _callbackParam;
        public void mRelease(System.Action<object> callback = null, object callbackParam = null)
        {
            _Release(callback, callbackParam);
        }

       
        public void mReleaseAt(float seconds, System.Action<object> callback = null, object callbackParam = null)
        {
            _callbackParam = callbackParam;
            _callback = callback;
            this.Invoke("_ReleaseAt", seconds);          
        }


        void _ReleaseAt()
        {
            _Release(_callback, _callbackParam);
            _callback = null;
            _callbackParam = null;
        }

        void _Release(System.Action<object> callback, object callbackParam = null)
        {
            if (OnReleaseDelegate != null)
            {
                if (OnReleaseDelegate(_parameters))
                {
                    if (callback != null)
                        callback(callbackParam);
                }               
            }
        }

    }
}