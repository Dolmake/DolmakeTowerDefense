using UnityEngine;
using System.Collections;
using Utils;
using DLMKPool;

namespace SGame.Common
{
    public class DesctructableInPool : MonoBehaviour
    {

        public void mDestroy()
        {
            if (this.enabled)
            {
                Debug.Log("Destroying");

				this.gameObject.PoolRelease();

            }
        }
    }
}
