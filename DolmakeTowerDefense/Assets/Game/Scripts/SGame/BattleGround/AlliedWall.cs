using UnityEngine;
using System.Collections;
using SGame.Common;
using Utils;

namespace SGame.BattleGround
{
    public class AlliedWall : Entity
    {
       



        public override void Life_OnNoLife(ILife obj)
        {        	
            BattleServer.SINGLETON.FinishPlaying();
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger on the wall" + other.name);
            LifeComponent.Life--;
        }
    }
}
