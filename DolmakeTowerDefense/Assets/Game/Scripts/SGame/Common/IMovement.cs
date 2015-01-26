using UnityEngine;
using System.Collections;

namespace SGame.Common
{
    public interface IMovement
    {
        Vector3 Direction { get; set; }
        float Speed { get; set; }

        void mSetDirection(Vector3 direction);
    }
}
