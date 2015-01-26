using UnityEngine;
using System.Collections;
using SGame.Common;
using Utils;

namespace SGame.Foes
{
    public class Foe : MonoBehaviour, ISpawnable
    {  
        FoeSpawnerManager SpawnManager;//Owner

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
            SpawnManager.IncreaseDeadFoes(this);
            DestroyFoe.mDestroy();
        }

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

        ILife _life;
        public ILife LifeComponent
        {
            get
            {
                if (_life == null)
                    _life = this.GetInterface<ILife>();
                return _life;
            }
        }


        public  DesctructableInPool _destroyFoe;//public to assing by Editor
        DesctructableInPool DestroyFoe
        {
            get
            {
                if (_destroyFoe == null)
                    _destroyFoe = GetComponent<DesctructableInPool>();
                return _destroyFoe;
            }
        }
        #endregion

        #region Messages

        public void mOnImpact(int strenght)
        {
            Debug.Log("Foe Life,"+ LifeComponent + " OnImpact " + strenght);
            LifeComponent.Life -= strenght;           
        }       

        public void mOnSpawn(MonoBehaviour invoker)
        {
            SpawnManager = invoker as FoeSpawnerManager;
            LifeComponent.ResetLife();
            SpawnManager.IncreaseAliveFoes(this);
            MovementComponent.Speed = SpawnManager.FoeSpeedFactor;
        }
        #endregion

       
    }
}
