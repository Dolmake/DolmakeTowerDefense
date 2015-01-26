using UnityEngine;
using System.Collections;
using Scripts.SMain;

namespace Utils
{
    /// <summary>
    /// Finite State Machine
    /// </summary>
    public class FSM : MonoBehaviour
    {
        public GameState EntryPoint;
        public GameState ExitPoint;

        GameState _currentGameState = null;

        void Awake()
        {
            
            GameState[] states = GetComponents<GameState>();
            foreach (GameState g in states)
            {
                g.FiniteStateMachine = this;
            }

        }
       

        // Use this for initialization
        void Start()
        {            
            _currentGameState = EntryPoint;
            _currentGameState.Active = true;
        }

        void Update()
        { 
            if (_currentGameState != null)
                _currentGameState.OnUpdate();
        }
       

        public void ChangeState(GameState newState)
        {
            Debug.Log("Changing state from" + _currentGameState + " to " + newState);
            if (_currentGameState != null)
            {
                _currentGameState.Active = false;
            }

            _currentGameState = newState;
            _currentGameState.Active = true;
        }

        public void ChangeState()
        {
            if (_currentGameState != null)
            {
                ChangeState(_currentGameState.NextState);
            }
        }

        public void Reset()
        {
            ChangeState(EntryPoint);
        }
    }
}
