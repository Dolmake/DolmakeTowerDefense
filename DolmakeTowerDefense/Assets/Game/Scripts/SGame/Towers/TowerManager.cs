using UnityEngine;
using System.Collections;
using Utils;
using SMain.Input;

namespace SGame.Towers
{
    public class TowerManager : MonoBehaviour
    {
        GameObjectPool _poolTowers = new GameObjectPool();

        public GameObject Prefab_Tower;

        //public int MaxTowers = 5;//Max towers allowed to deploy
        public int _maxTowers = 10;
        public int MaxTowers
        {
            get
            {
                return _maxTowers;
            }
            set
            {
                _maxTowers = value;                
                _towersDeployed = _poolTowers.UsedObjects.Count;
            }
        }

        int _towersDeployed = 0;
        public int TowersDeployed
        {
            get { return _towersDeployed; }
            set { _towersDeployed = value; }
        }

        /// <summary>
        /// Number of towers player can deploy
        /// </summary>
        public int TowersLeft
        {
            get
            {
                return MaxTowers - TowersDeployed;
            }
        }

        
        

        void OnEnable()
        {
            if (Prefab_Tower != null)
            {
                _poolTowers.Initialize(this.transform, Prefab_Tower, MaxTowers);  
            }
            InputServer.SINGLETON.OnRayAtPress += SINGLETON_OnRayAtPress;
        }

        void OnDisable()
        {
            InputServer.SINGLETON.OnRayAtPress -= SINGLETON_OnRayAtPress;
        }
   

        void OnDestroy()
        {
            _poolTowers.Deinitialize();
        }

        #region MISC


        void SINGLETON_OnRayAtPress(ref RaycastHit const_hit, Press touch, Camera camera, ref Ray const_ray)
        {
            OnScreenPressed(ref const_hit);
        }    

        private void OnScreenPressed(ref RaycastHit const_hit)
        {
            if (IsMe(ref const_hit) && CanDeploy())
            {
                SetATowerAt(const_hit.point);
            }
        }

        private bool CanDeploy()
        {
            //return _poolTowers.UsedObjects.Count < MaxTowers;
            return _towersDeployed < MaxTowers;
        }
        private bool IsMe(ref RaycastHit const_hit)
        {
            return const_hit.collider == this.collider;
        }

        private void SetATowerAt(Vector3 postion_WS)
        {
            GameObject tower = _poolTowers.Get();
            if (tower != null)
            {
                _towersDeployed++;
                tower.SendMessage("mOnSpawn",this, SendMessageOptions.DontRequireReceiver);
                postion_WS.y = BattleServer.SINGLETON.BattleY;
                tower.transform.position = postion_WS;
                //Debug.Log("Deploying Tower at " + postion_WS);
            }
        }

        #endregion
    }
}
