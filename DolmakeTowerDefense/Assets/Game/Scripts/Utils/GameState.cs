using UnityEngine;
using System.Collections;


namespace Utils
{
    public abstract class GameState : MonoBehaviour
    {
        public virtual FSM FiniteStateMachine { get; set; }
        public GameState NextState;
        public virtual string Name
        {
            get { return this.ToString(); }
        }

        bool _active = false;
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                if (_active == value) return;
                _active = value;
                if (!value)
                {
                    OnDeactive();
                    Debug.Log("GameState: " + this.Name + " DEACTIVE");
                }
                else
                {
                    Debug.Log("GameState: " + this.Name + " ACTIVE");
                    OnActive();
                }

            }
        }
        protected abstract void OnActive();
        protected abstract void OnDeactive();

        public abstract void OnUpdate();

    }
}

