using UnityEngine;
using System.Collections;
using Utils;

namespace SGame.Common
{
    public class LifeTime : MonoBehaviour
    {
        public float StartingTimeAlive = 5f;
        float _accumTime = 0f;

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
            _accumTime = 0f;            
        }

        // Update is called once per frame
        void Update()
        {
            _accumTime += Time.deltaTime;
            if (_accumTime >= StartingTimeAlive)
            {
                LifeComponent.Life = 0;               
            }
        }
    }
}
