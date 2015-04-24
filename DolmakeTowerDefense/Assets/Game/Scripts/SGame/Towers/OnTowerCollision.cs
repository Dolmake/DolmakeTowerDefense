using UnityEngine;
using System.Collections;

namespace SGame.Towers
{
    public class OnTowerCollision : MonoBehaviour
    {
        #region Components
        public Tower _toweLife;//public to assing by Editor
        Tower TowerLife
        {
            get
            {
                if (_toweLife == null)
                    _toweLife = GetComponent<Tower>();
                return _toweLife;
            }
        }
        #endregion

        void OnTriggerEnter(Collider other)
        {
			if (!this.enabled) return;
            Debug.Log("Trigger on the Tower");
            TowerLife.mOnImpact(1);
        }
    }
}
