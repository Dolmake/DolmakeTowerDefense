using UnityEngine;
using System.Collections;

namespace SGame.Common
{
    public class EntityMovement : MonoBehaviour, IMovement
    {
        public Vector3 _direction = new Vector3(-1f, 0, 0);
        public Vector3 Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
            }
        }
        public float _speed = 1f;
        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }


        #region Components
        public Transform _transform;//public to assing by Editor
        Transform TransformCached
        {
            get
            {
                if (_transform == null)
                    _transform = this.transform;

                return _transform;
            }
        }
        #endregion
        

        // Update is called once per frame
        void Update()
        {
            this.TransformCached.Translate(Direction * Time.deltaTime * Speed);
        }

        public void mSetDirection(Vector3 direction)
        {
            Direction = direction;
        }
    }
}
