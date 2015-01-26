using UnityEngine;
using System.Collections;
using SGame.Common;
using Utils;

namespace SGame.Lasers
{
    public class Laser : MonoBehaviour, ISpawnable
    {

        #region Components

        public DesctructableInPool _destroyLaser;//public to assing by Editor
        DesctructableInPool DestroyLaser
        {
            get
            {
                if (_destroyLaser == null)
                    _destroyLaser = GetComponent<DesctructableInPool>();
                return _destroyLaser;
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
        #endregion

        public void mOnSpawn(MonoBehaviour invoker)
        {
            LifeComponent.ResetLife();
        }

        void OnEnable()
        {
            LifeComponent.OnNoLife += new System.Action<ILife>(LifeComponent_OnNoLife);
        }
        void OnDisable()
        {
            LifeComponent.OnNoLife -= new System.Action<ILife>(LifeComponent_OnNoLife);
        }

        void LifeComponent_OnNoLife(ILife obj)
        {
            DestroyLaser.mDestroy();
        }
    }
}
