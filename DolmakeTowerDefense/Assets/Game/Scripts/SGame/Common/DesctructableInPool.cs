using UnityEngine;
using System.Collections;
using Utils;
using DLMKPool;

namespace SGame.Common
{
    public class DesctructableInPool : MonoBehaviour
    {
    	
    	public bool playExplosion = true;

        public void mDestroy()
        {
            if (this.enabled)
            {
                Debug.Log("Destroying");

				if (playExplosion)
				    BattleServer.SINGLETON.PlayExplosionAt(this.transform.position);

				this.gameObject.PoolRelease();

            }
        }
    }
}
