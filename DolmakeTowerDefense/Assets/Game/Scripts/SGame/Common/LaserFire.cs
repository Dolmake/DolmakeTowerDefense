using UnityEngine;
using System.Collections;
using SGame;

namespace SGame.Common
{
public class LaserFire : MonoBehaviour {


		float _accumTime = 0f;
        bool _readyToFire = false;

       
        public float LoadingFireTime = 3f;

	

	public void Fire (Vector3 target_WS)
		{
			if (_readyToFire) {
				GameObject laser = BattleServer.SINGLETON.LaserPool.Get ();
				Vector3 pos_WS = this.transform.position;
				pos_WS.y = BattleServer.SINGLETON.BattleY;
				laser.transform.position = pos_WS;
				Vector3 laserDirection = target_WS - this.transform.position;
				laser.SendMessage ("mOnSpawn", this, SendMessageOptions.DontRequireReceiver);
				laser.SendMessage ("mSetDirection", laserDirection.normalized, SendMessageOptions.DontRequireReceiver);
				_readyToFire = false;
			}
        }

	// Update is called once per frame
        void Update()
        {
            //Loading fire
            _accumTime += Time.deltaTime;
            if (_accumTime >= LoadingFireTime)
            {
                _accumTime = 0f;
                _readyToFire = true;
            }

           
        }
}
        }
