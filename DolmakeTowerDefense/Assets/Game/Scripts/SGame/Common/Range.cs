using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SGame.Common
{
	public class Range : MonoBehaviour, IRange
	{	

		
		#region IRange implementation
		public float _range = 5f;
		public float RangeDistance { get {return _range;}}

		public event System.Action<Entity> OnEntityInRange;

		#endregion


		List<Entity> _entities = new List<Entity>();
		// Update is called once per frame
		void Update ()
		{
			_entities.Clear();
			_entities.AddRange(BattleServer.Entities);
			foreach (Entity entity in _entities)
			 {
				if (entity.enabled &&
				 entity.EntityType != EntityEnum.None && 
				entity.transform != this.transform && 
				Utils.HelperClass.DistanceLessThan(entity.transform.position, this.transform.position, this.RangeDistance))
				{
					if (OnEntityInRange != null)
						OnEntityInRange(entity);
				}
			}
			_entities.Clear();

		}

	}
}