
using UnityEngine;
using System.Collections;
using Utils;
using SGame.Foes;
using SGame.Towers;
using System.Collections.Generic;
using SGame.BattleGround;
using SGame.Common;

namespace SGame
{
    public class BattleServer : MonoBehaviour
    {

        GameObject LasersParent;//All lasers has the same parent
		DLMKPool.GameObjectPool _laserPool = new DLMKPool.GameObjectPool();
		List<Entity> _entities = new List<Entity>();

		public List<Entity> Entities {
			get {
				return _entities;
			}
		}

        /// <summary>
        /// Factor that defines how difficulty increases
        /// </summary>
        public float IncreaseDifficultyFactor = 2f;

        /// <summary>
        /// Height of battle
        /// </summary>
        public float BattleY = 1f;
       
        public int CurrentLevel { get; private set; }

        /// <summary>
        /// In charge of spawns
        /// </summary>
        public FoeSpawnerManager FoeSpawnManager;

        /// <summary>
        /// In charge of towers deployment
        /// </summary>
        public TowerManager TowerManager;

        /// <summary>
        /// Allied Wall Life
        /// </summary>
        public AlliedWall Allied;
        
       
        public GameObject Prefab_Laser;
        
        /// <summary>
        /// Pool of lasers
        /// </summary>
		public DLMKPool.GameObjectPool LaserPool
        {
            get { return _laserPool; }           
        }

        /// <summary>
        /// Number of towers waiting for deploy
        /// </summary>
        public int TowersLeft
        {
            get
            {
                return TowerManager.TowersLeft;
            }
        }

        /// <summary>
        /// Number of casualties
        /// </summary>
        public int DeadFoes
        {
            get
            {
                return FoeSpawnManager.DeadFoes;
            }
        }

        /// <summary>
        /// Number of Foes currently on the screen
        /// </summary>
        public List<GameObject> FoesPlaying
        {
            get
            {
                return FoeSpawnManager.FoesPlaying;
            }
        }

        #region SINGLETON
        static BattleServer _instance = null;
        public static BattleServer SINGLETON
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        void Awake()
        {
           IniSingleton();
        }

        void OnEnable ()
		{
			IniSingleton();
		}

		void IniSingleton ()
		{
			if (_instance != null) return;

			_instance = this;
            if (Prefab_Laser != null)
            {
                LasersParent = new GameObject("Laser Pool");
                LasersParent.transform.parent = this.transform;
                _laserPool.Initialize(LasersParent.transform, Prefab_Laser, 10);
                CurrentLevel = 1;
            }
		}

        void OnDestroy()
        {
            _instance = null;
        }

        #region Public
        public void StartPlaying()
        {
            FoeSpawnManager.gameObject.SetActive(true);
            TowerManager.gameObject.SetActive(true);
        }

        public void FinishPlaying()
        {
            FoeSpawnManager.gameObject.SetActive(false);
            TowerManager.gameObject.SetActive(false);
        }


        public void IncreaseLevel()
        {
            this.CurrentLevel++;
            TowerManager.MaxTowers++;
            //if (CurrentLevel < 2)
            	//FoeSpawnManager.FoeSpeedFactor *= IncreaseDifficultyFactor;
			
			FoeSpawnManager.MaxTimeToSpawn /= IncreaseDifficultyFactor;
			FoeSpawnManager.MinTimeToSpawn /= IncreaseDifficultyFactor;
        }

        public void AddEntity (Entity entity)
		{
			if (!_entities.Contains (entity)) {
				_entities.Add(entity);
			}
		}

		public void RemoveEntity (Entity entity)
		{
			_entities.Remove(entity);
		}
        #endregion
    }
}
