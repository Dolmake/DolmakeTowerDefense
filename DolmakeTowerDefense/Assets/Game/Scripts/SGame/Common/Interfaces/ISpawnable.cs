using UnityEngine;
using System.Collections;

namespace SGame.Common
{
    public interface ISpawnable
    {
        void mOnSpawn(MonoBehaviour invoker);
    }
}
