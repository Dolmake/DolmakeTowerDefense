using UnityEngine;
using System.Collections;
using Utils;

namespace SGame.Lasers
{
    public class TowerLaserFire : MonoBehaviour
    {       
        float _accumTime = 0f;
        bool _readyToFire = false;

        public GameObject RangeGameObject;//Gameobject to Draw the range
        public float Range = 5f;
        public float LoadingFireTime = 3f;

        #region Components
        public Transform _transform;//public to assing by Editor
        Transform TransformCached
        {
            get
            {
                if (_transform == null)
                    _transform = this.transform;
                return _transform;
            }
        }
        #endregion


        void OnEnable()
        {
            _accumTime = 0;
            if (RangeGameObject != null)
                RangeGameObject.transform.localScale = Vector3.one * Range;
            
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

            if (_readyToFire)
            {
                GameObject foe = EnemyInRange();
                if (foe != null )            
                    Fire(foe);            
            }
        }

      

        #region MISC
        private GameObject EnemyInRange()
        {
            GameObject foe = null;

            for (int i = 0; foe == null && i < BattleServer.SINGLETON.FoesPlaying.Count; ++i)
            {
                GameObject foeToCheck = BattleServer.SINGLETON.FoesPlaying[i];
                if (Utils.HelperClass.DistanceLessThan(foeToCheck.transform.position, TransformCached.position, this.Range))
                {
                    foe = foeToCheck;
                }
            }
            return foe;
        }


        private void Fire(GameObject foe)
        {
            GameObject laser = BattleServer.SINGLETON.LaserPool.Get();
            Vector3 pos_WS = this.TransformCached.position;
            pos_WS.y = BattleServer.SINGLETON.BattleY;
            laser.transform.position = pos_WS;
            Vector3 laserDirection = foe.transform.position - this.transform.position;
            laser.SendMessage("mOnSpawn", this, SendMessageOptions.DontRequireReceiver);
            laser.SendMessage("mSetDirection", laserDirection.normalized, SendMessageOptions.DontRequireReceiver);
            _readyToFire = false;
        }

        #endregion


    }
}
