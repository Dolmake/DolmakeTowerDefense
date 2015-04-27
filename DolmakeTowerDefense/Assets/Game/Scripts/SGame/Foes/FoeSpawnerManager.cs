using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;
using P8Core.P8Common;

namespace SGame.Foes
{
    public class FoeSpawnerManager : MonoBehaviour
    {
        int _deadFoes = 0;
        List<GameObject> _foesPlaying = new List<GameObject>();
		List<DLMKPool.GameObjectPool> _pools = new List<DLMKPool.GameObjectPool>();       

        /// <summary>
        /// Public to be able to edit in the Editor
        /// </summary>
        public Transform[] E_FoeSpawns;
        public float MinTimeToSpawn = 1f;
        public float MaxTimeToSpawn = 5f;
        public float FoeSpeedFactor = 1f;

        public GameObject Prefab_Foe;//Foe prefab  

        public GameObject Prefab_Foe_Zigzag;// ZigZag Foe Prefab  

        /// <summary>
        /// Number of casualties
        /// </summary>
        public int DeadFoes
        {
            get
            {
                return _deadFoes;
            }
            private set
            {
                _deadFoes = value;
            }
        }
       
        /// <summary>
        /// Foes currently on the screen
        /// </summary>
        public List<GameObject> FoesPlaying
        {
            get
            {
                return _foesPlaying;
            }
        }
       
        void OnEnable()
        {
            if (Prefab_Foe != null)
            {
				DLMKPool.GameObjectPool poolFoes = new DLMKPool.GameObjectPool();
                poolFoes.Initialize(this.transform, Prefab_Foe, 5);
                _pools.Add(poolFoes);

				DLMKPool.GameObjectPool foesZigZagPool = new DLMKPool.GameObjectPool();
                foesZigZagPool.Initialize(this.transform, Prefab_Foe_Zigzag, 5);
                _pools.Add(foesZigZagPool);

                Invoke("mSpawnFoe", TimeToSpawnAFoe());
            }

        }

        void OnDestroy()
        {
            for (int i = 0; i < _pools.Count; ++i)            
                _pools[i].Deinitialize();            
            _pools.Clear();
        }

        #region PUBLIC
        public GameObject GetFoe()
        {
            int poolIndex = Random.Range(0, _pools.Count);
            return _pools[poolIndex].Get();
        }

        public void IncreaseDeadFoes(Foe foe)
        {
            DeadFoes += 1;
            RemoveFoe(foe.gameObject);
        }
        public void IncreaseAliveFoes(Foe foe)
        {
            AddFoe(foe.gameObject);
        }
        #endregion

        #region Messages

        public void mSpawnFoe()
        {
            if (this.enabled)
            {
                Transform spawner = E_FoeSpawns[Random.Range(0, E_FoeSpawns.Length - 1)];

				//Spawn a Foe
				GameObject foe = GetFoe();

				if (foe != null)
				{
					//Set foe at Pos
					Vector3 pos_WS = spawner.position;
					pos_WS.y = BattleServer.SINGLETON.BattleY;
					foe.transform.position = pos_WS;
					
					foe.SendMessage("mOnSpawn", this, SendMessageOptions.DontRequireReceiver);
				}

				//Invoke again in TimeToSpawinAFoe
                Invoke("mSpawnFoe", TimeToSpawnAFoe());
            }
        }

        #endregion        

        #region Misc

        float TimeToSpawnAFoe()
        {
            return Random.Range(MinTimeToSpawn, MaxTimeToSpawn);
        }
        void AddFoe(GameObject foe)
        {
            FoesPlaying.Add(foe);
        }
        void RemoveFoe(GameObject foe)
        {
            FoesPlaying.Remove(foe);
        }
        #endregion

    }
}
