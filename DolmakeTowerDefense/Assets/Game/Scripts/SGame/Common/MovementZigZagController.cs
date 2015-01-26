using UnityEngine;
using System.Collections;
using SGame;
using Utils;


namespace SGame.Common
{
    public class MovementZigZagController : MonoBehaviour
    {
        public float _zigZagFactor = 1f;
        public float TimeToZigZag
        {
            get
            {
                return _zigZagFactor / MovementComponent.Speed;
            }
        }
        float _accumTime = 0f;

        #region Components

        public IMovement _movementComponent;
        public IMovement MovementComponent
        {
            get
            {
                if (_movementComponent == null)
                    _movementComponent = this.GetInterface<IMovement>();
                return _movementComponent;
            }
        }
       
        #endregion

        void OnEnable()
        {
            _accumTime = TimeToZigZag;
        }

        // Update is called once per frame
        void Update()
        {
            _accumTime -= Time.deltaTime;
            if (_accumTime <= 0)
            {
                _accumTime = TimeToZigZag;
                ChangeDirection();
            }

        }      

        #region MISC
        private void ChangeDirection()
        {
            Vector3 direction = MovementComponent.Direction;
            direction.z *= -1f;
            MovementComponent.Direction = direction;
        }

        #endregion
    }
}
