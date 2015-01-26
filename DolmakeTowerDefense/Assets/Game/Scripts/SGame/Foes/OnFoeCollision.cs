using UnityEngine;
using System.Collections;

namespace SGame.Foes
{
    public class OnFoeCollision : MonoBehaviour
    {
        #region Components
        public Foe _foeLife;//public to assing by Editor
        Foe FoeLife
        {
            get
            {
                if (_foeLife == null)
                    _foeLife = GetComponent<Foe>();
                return _foeLife;
            }
        }
        #endregion

        void mOnEnterEndWall()
        {
            OnHardImpact();
        }

        void OnTriggerEnter(Collider other)
        {
            //Debug.Log("Trigger on the Foe");
            if (other.tag == "LASER")
                OnLaserImpact();
            else
                OnHardImpact();
        }

        private void OnHardImpact()
        {
            Debug.Log("FOE HARD IMPACT");
            FoeLife.mOnImpact(int.MaxValue);
        }

        private void OnLaserImpact()
        {
            Debug.Log("FOE LASER");
            FoeLife.mOnImpact(1);
        }
    }
}
