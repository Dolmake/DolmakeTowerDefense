using UnityEngine;
using System.Collections;
using Utils;

namespace SGame.Common
{
    public class LifeBarController : MonoBehaviour
    {
        public Transform LifeBar;

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

        // Use this for initialization

        void OnEnable()
        {
            LifeComponent.OnLifeChanged += new System.Action<ILife>(Life_OnLifeChanged);
        }

      

        void OnDisable()
        {
            LifeComponent.OnLifeChanged -= new System.Action<ILife>(Life_OnLifeChanged);
        }

        void Life_OnLifeChanged(ILife obj)
        {
            UpdateLifeBar(obj);
        }
        


        #region MISC
        void UpdateLifeBar(ILife life)
        {
			if (LifeBar != null){
            	Vector3 localScale = LifeBar.localScale;
            	localScale.x = (float)life.Life / (float)life.StartingLife;
            	LifeBar.localScale = localScale;
			}
        }
        #endregion
    }
}
