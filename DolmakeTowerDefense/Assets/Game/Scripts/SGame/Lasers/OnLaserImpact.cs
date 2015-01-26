using UnityEngine;
using System.Collections;
using SGame.Common;

namespace SGame.Lasers
{
    public class OnLaserImpact : MonoBehaviour
    {       

        #region Components       

        public EntityLife _life;
        EntityLife LifeComponent
        {
            get
            {
                if (_life == null)
                    _life = GetComponent<EntityLife>();
                return _life;
            }
        }

        #endregion

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger on the Laser " + other.name);
            //other.SendMessage("mOnImpact", ImpactPower, SendMessageOptions.DontRequireReceiver);
            LifeComponent.Life--;
        }        
    }
}
