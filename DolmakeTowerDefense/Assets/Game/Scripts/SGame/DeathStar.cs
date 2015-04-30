using UnityEngine;
using System.Collections;
using SGame.Common;
using SGame;

public class DeathStar : Entity {

	public LaserFire _Laser;
	public LaserFire Laser{
			get{
				if (_Laser == null)
					_Laser = GetComponent<LaserFire>();
			return _Laser;
			}
		}

	public override void OnEnable ()
	{
		base.OnEnable ();
		InputServer.SINGLETON.OnTouchCollider += HandleOnTouchCollider;
	}

	public override void OnDisable ()
	{
		InputServer.SINGLETON.OnTouchCollider -= HandleOnTouchCollider;
		base.OnDisable ();
	}

	void HandleOnTouchCollider (RaycastHit obj)
	{
		if (obj.collider.transform == this.transform) {//Is me
			if (Laser) {
				foreach (Entity entity in BattleServer.Entities) {
				if (entity.EntityType != EntityType)
					Laser.Fire(entity.TransformCached.position);
				}
			}
		}
	}
}
