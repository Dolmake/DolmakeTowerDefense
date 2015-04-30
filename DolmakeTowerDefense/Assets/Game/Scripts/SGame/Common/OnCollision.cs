using UnityEngine;
using System.Collections;
using SGame.Common;

public class OnCollision : MonoBehaviour {

	
		
	
			public Entity _entity;//public to assing by Editor
	public Entity Owner
	        {
	            get
	            {
			if (_entity == null)
				_entity = GetComponent<Entity>();
			return _entity;
	            }
	        }


        void OnTriggerEnter(Collider other)
        {
			if (!this.enabled) return;
            Debug.Log("Trigger on the Entity");

            Entity otherEntity = other.GetComponent<Entity>();
            if (otherEntity == null || otherEntity.EntityType == EntityEnum.None)
                 Owner.mOnImpact(1);
            else if (otherEntity.EntityType != Owner.EntityType)
				 Owner.mOnHardImpact();
        }
}
