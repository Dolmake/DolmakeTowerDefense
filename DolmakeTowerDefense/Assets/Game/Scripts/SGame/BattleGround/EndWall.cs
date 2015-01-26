using UnityEngine;
using System.Collections;

namespace SGame.BattleGround
{
    public class EndWall : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger on the wall" + other.name);
            other.SendMessage("mOnEnterEndWall", SendMessageOptions.DontRequireReceiver);
        }
    }
}
