using UnityEngine;
using System.Collections;
using Utils;

namespace SGame.Foes
{
    public class FoeSpawner : MonoBehaviour
    {
        public GameObject SpawnFoe(FoeSpawnerManager spawnManager)
        {
            GameObject foe = spawnManager.GetFoe();
            //Set foe at Pos
            Vector3 pos_WS = this.transform.position;
            pos_WS.y = BattleServer.SINGLETON.BattleY;
            foe.transform.position = pos_WS;
            
            foe.SendMessage("mOnSpawn", spawnManager, SendMessageOptions.DontRequireReceiver);
            return foe;
        }
    }
}
