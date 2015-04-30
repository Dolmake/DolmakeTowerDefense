using UnityEngine;
using System.Collections;
using SGame.Common;

namespace SGame.Towers
{
    public class Tower : Entity
    {


		#region Components
        public Range _range;//public to assing by Editor
		public Range RangeComponent
        {
            get
            {
				if (_range == null)
					_range = GetComponent<Range>();
				return _range;
            }
        }

		public LaserFire _laserFire;//public to assing by Editor
		public LaserFire Laser
        {
            get
            {
				if (_laserFire == null)
					_laserFire = GetComponent<LaserFire>();
				return _laserFire;
            }
        }
        #endregion


        public override void OnEnable()
        {
        	base.OnEnable();
			RangeComponent.OnEntityInRange += EntityOnRange;
            
        }      

        public override void OnDisable()
        {
        	base.OnDisable();
			RangeComponent.OnEntityInRange -= EntityOnRange;
           
        }


		void EntityOnRange (Entity entity)
		{
			if (entity.EntityType != EntityEnum.None && entity.EntityType != this.EntityType)
				Laser.Fire(entity.TransformCached.position);
		}


		public override void mOnHardImpact ()
		{
			//base.mOnHardImpact ();
			mOnImpact(1);
		}
        



    


        
    }
}
