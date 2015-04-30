using UnityEngine;
using System.Collections;

namespace SGame.Common
{
    public class EntityLife : MonoBehaviour, ILife
    {
        public event System.Action<ILife> OnLifeChanged;
        public event System.Action<ILife> OnNoLife;


        public int _startingLife = 1;
        public int StartingLife
        {
            get
            {
                return _startingLife;
            }
            set
            {
                _startingLife = value;
            }
        }


        int _life = 1;
        public int Life
        {
            get
            {
                return _life;
            }
            set
            {
                if (_life != value)
                {
                    _life = value;
                    _life = _life < 0 ? 0 : _life;
                    ThrowOnLifeChanged();
                    if (_life <= 0)
                        ThrowOnNoLife();
                }
            }
        }

       

        public void ResetLife()
        {
            Life = StartingLife;
        }

        private void ThrowOnLifeChanged()
        {
            if (OnLifeChanged != null)
                OnLifeChanged(this);
        }

        private void ThrowOnNoLife()
        {
            if (OnNoLife != null)
                OnNoLife(this);
        }  
    }
}
