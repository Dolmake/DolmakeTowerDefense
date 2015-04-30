using UnityEngine;
using System.Collections;

namespace SGame.Common
{
	public abstract class Entity : MonoBehaviour 
	{
			public EntityEnum EntityType = EntityEnum.None;

			public Transform _transform;//public to assing by Editor
	        public Transform TransformCached
	        {
	            get
	            {
	                if (_transform == null)
	                    _transform = this.transform;
	                return _transform;
	            }
	        }

			public EntityLife _lifeComponent;//public to assing by Editor
			public EntityLife LifeComponent
	        {
	            get
	            {
				if (_lifeComponent == null)
					_lifeComponent = GetComponent<EntityLife>();
				return _lifeComponent;
	            }
	        }

			public DesctructableInPool _destroyer;//public to assing by Editor
		    public DesctructableInPool Destroyer
	        {
	            get
	            {
					if (_destroyer == null)
						_destroyer = GetComponent<DesctructableInPool>();
					return _destroyer;
	            }
	        }
		

	

	        public virtual void OnEnable ()
			{
				if (EntityType != EntityEnum.None)
					BattleServer.SINGLETON.AddEntity (this);

				if (LifeComponent != null) {
					LifeComponent.ResetLife();	
					LifeComponent.OnNoLife +=	Life_OnNoLife;
				}
			}
			public virtual void OnDisable ()
			{
				BattleServer.SINGLETON.RemoveEntity(this);
				if (LifeComponent != null)
					LifeComponent.OnNoLife -=	Life_OnNoLife;
			}

			public virtual void mOnImpact (int strenght)
			{
				if (LifeComponent != null) {
					Debug.Log ("Entity Life," + LifeComponent + " OnImpact " + strenght);
					LifeComponent.Life -= strenght;           
				}
	        }  
	        public virtual void mOnHardImpact ()
			{
				mOnImpact(int.MaxValue);
			}   

			public virtual void mOnSpawn(MonoBehaviour towerManager)
        	{
				if (this.enabled)
					LifeComponent.ResetLife();
        	}

			public virtual void Life_OnNoLife(ILife obj)
			{
				if (Destroyer)	
					Destroyer.mDestroy();
			}
          
	}
}
