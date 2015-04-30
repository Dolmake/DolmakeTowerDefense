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

			public EntityLife _EntityLifeComponent;//public to assing by Editor
			public EntityLife EntityLifeComponent
	        {
	            get
	            {
				if (_EntityLifeComponent == null)
					_EntityLifeComponent = GetComponent<EntityLife>();
				return _EntityLifeComponent;
	            }
	        }

			public DesctructableInPool _DestructableInPool;//public to assing by Editor
		    public DesctructableInPool DestructableInPoolComponent
	        {
	            get
	            {
					if (_DestructableInPool == null)
						_DestructableInPool = GetComponent<DesctructableInPool>();
					return _DestructableInPool;
	            }
	        }
		

	

	        public virtual void OnEnable ()
			{
				if (EntityType != EntityEnum.None)
					BattleServer.AddEntity (this);

				if (EntityLifeComponent != null) {
					EntityLifeComponent.ResetLife();	
					EntityLifeComponent.OnNoLife +=	Life_OnNoLife;
				}
			}
			public virtual void OnDisable ()
			{
				BattleServer.RemoveEntity(this);
				if (EntityLifeComponent != null)
					EntityLifeComponent.OnNoLife -=	Life_OnNoLife;
			}

			public virtual void mOnImpact (int strenght)
			{
				if (EntityLifeComponent != null) {
					Debug.Log ("Entity Life," + EntityLifeComponent + " OnImpact " + strenght);
					EntityLifeComponent.Life -= strenght;           
				}
	        }  
	        public virtual void mOnHardImpact ()
			{
				mOnImpact(int.MaxValue);
			}   

			public virtual void mOnSpawn(MonoBehaviour towerManager)
        	{
				if (this.enabled)
					EntityLifeComponent.ResetLife();
        	}

			public virtual void Life_OnNoLife(ILife obj)
			{
				if (DestructableInPoolComponent)	
					DestructableInPoolComponent.mDestroy();
			}
          
	}
}
