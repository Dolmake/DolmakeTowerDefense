using UnityEngine;

namespace DLMKPool
{
    public static class DLMKUtils
    {       

        public static void PoolRelease(this GameObject go, System.Action<object> callback = null, object callbackParam = null)
        {
            //go.SendMessage("mRelease", SendMessageOptions.DontRequireReceiver);
            go.GetComponent<PoolReleaser>().mRelease(callback, callbackParam);
        }

        public static void PoolRelease(this GameObject go, float seconds, System.Action<object> callback = null, object callbackParam = null)
        {
            // go.SendMessage("mReleaseAt",seconds, SendMessageOptions.DontRequireReceiver);
            go.GetComponent<PoolReleaser>().mReleaseAt(seconds, callback, callbackParam);
        }

       
    }
}
