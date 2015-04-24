using UnityEngine;
using System.Collections;
using SGame.Common;

namespace SGame.Towers
{
    public class Tower : MonoBehaviour, ISpawnable
    {

        #region Components

        public EntityLife _life;
        EntityLife LifeComponent
        {
            get
            {
                if (_life == null)
                    _life = GetComponent<EntityLife>();
                return _life;
            }
        }
        public DesctructableInPool _destroyTower;//public to assing by Editor
        DesctructableInPool DestroyTower
        {
            get
            {
                if (_destroyTower == null)
                    _destroyTower = GetComponent<DesctructableInPool>();
                return _destroyTower;
            }
        }
        #endregion


        void OnEnable()
        {
            LifeComponent.OnNoLife += new System.Action<ILife>(Life_OnNoLife);
        }      

        void OnDisable()
        {
            LifeComponent.OnNoLife -= new System.Action<ILife>(Life_OnNoLife);
        }

		void Life_OnNoLife(ILife obj)
		{
			DestroyTower.mDestroy();
		}
        



        #region Messages

        public void mOnSpawn(MonoBehaviour towerManager)
        {
			if (this.enabled)
				LifeComponent.ResetLife();
        }

        public void mOnImpact(int strenght)
        {
            Debug.Log("Tower impacted " + strenght);
            LifeComponent.Life -= strenght;
           
        }
        #endregion


        
    }
}
