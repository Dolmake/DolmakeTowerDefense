using P8Core.P8Common;
using UnityEngine;
using System.Collections;
using Utils;


namespace SGame.Towers
{
    public class TowerManager : MonoBehaviour
    {
	
		DLMKPool.GameObjectPool _poolTowers = new DLMKPool.GameObjectPool();

        public GameObject Prefab_Tower;
		Collider _collider = null;
		public Collider Collider{
			get{
				if (_collider == null)
					_collider = GetComponent<Collider>();
				return _collider;
			}
		}

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

        
        

        void OnEnable ()
		{
			if (Prefab_Tower != null) {
				_poolTowers.Initialize (this.transform, Prefab_Tower, 0);  
			}
           
			InputServer.SINGLETON.OnTouchCollider += OnTouchCollider;
		}

        void OnDisable()
        {
            InputServer.SINGLETON.OnTouchCollider -= OnTouchCollider;
        }

		void OnTouchCollider (RaycastHit hit)
		{
			OnScreenPressed(ref hit);
		}
   

        void OnDestroy()
        {
            _poolTowers.Deinitialize();
        }

        /*
		//float _time = 5f;
		void Update()
		{
			//If touch the screen...
			if (Input.GetMouseButton(0))
			{
				RaycastHit hit;
				Vector3 pos_VP = Camera.main.ScreenToViewportPoint(Input.mousePosition);
				Ray ray = Camera.main.ViewportPointToRay(pos_VP);
				if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
				{
					OnScreenPressed(ref hit);
				}
			}
		}
		*/

        #region MISC


       
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
			return const_hit.collider == this.Collider;
        }

        private void SetATowerAt(Vector3 postion_WS)
        {
			Debug.Log("Setting tower at :" + postion_WS);
            GameObject tower = _poolTowers.Get();
            if (tower != null)
            {
                _towersDeployed++;
                
                postion_WS.y = BattleServer.SINGLETON != null ? BattleServer.SINGLETON.BattleY : postion_WS.y;
                tower.transform.position = postion_WS;
                Debug.Log("Tower deployed at " + postion_WS);
				tower.SendMessage("mOnSpawn",this, SendMessageOptions.DontRequireReceiver);
            }
        }

        #endregion
    }
}
