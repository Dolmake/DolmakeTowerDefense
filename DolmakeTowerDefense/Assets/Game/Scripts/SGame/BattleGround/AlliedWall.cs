using UnityEngine;
using System.Collections;
using SGame.Common;
using Utils;

namespace SGame.BattleGround
{
    public class AlliedWall : MonoBehaviour
    {
        #region Components
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

        void OnEnable()
        {
            LifeComponent.ResetLife();
            LifeComponent.OnNoLife += new System.Action<ILife>(Life_OnNoLife);
        }

        void OnDisable()
        {
            LifeComponent.OnNoLife -= new System.Action<ILife>(Life_OnNoLife);
        }

        void Life_OnNoLife(ILife obj)
        {
            BattleServer.SINGLETON.FinishPlaying();
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger on the wall" + other.name);
            LifeComponent.Life--;
        }
    }
}
