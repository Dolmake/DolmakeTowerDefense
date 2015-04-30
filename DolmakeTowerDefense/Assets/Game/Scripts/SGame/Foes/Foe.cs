using UnityEngine;
using System.Collections;
using SGame.Common;
using Utils;

namespace SGame.Foes
{
    public class Foe : Entity
    {  
        FoeSpawnerManager SpawnManager;//Owner

		



        #region Components

        IMovement _movementComponent;
        public IMovement MovementComponent
        {
            get
            {
                if (_movementComponent == null)
                    _movementComponent = this.GetInterface<IMovement>();
                return _movementComponent;
            }
        }

        #endregion

        #region Messages

       
        public override void mOnSpawn(MonoBehaviour invoker)
        {
        	base.mOnSpawn(invoker);
            SpawnManager = invoker as FoeSpawnerManager;
            SpawnManager.IncreaseAliveFoes(this);
            MovementComponent.Speed = SpawnManager.FoeSpeedFactor;
        }
        #endregion

        public override void Life_OnNoLife (ILife obj)
		{
			base.Life_OnNoLife (obj);
			SpawnManager.IncreaseDeadFoes(this);
		}

       
    }
}
