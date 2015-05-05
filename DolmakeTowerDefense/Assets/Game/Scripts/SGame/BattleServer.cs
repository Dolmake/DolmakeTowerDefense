
using UnityEngine;
using System.Collections;
using Utils;
using SGame.Foes;
using SGame.Towers;
using System.Collections.Generic;
using SGame.BattleGround;
using SGame.Common;
using DLMKPool;

namespace SGame
{
    public class BattleServer : MonoBehaviour
    {

        GameObject LasersParent;//All lasers has the same parent
		DLMKPool.GameObjectPool _laserPool = new DLMKPool.GameObjectPool();

		DLMKPool.GameObjectPool _explosionPool = new DLMKPool.GameObjectPool();

		#region Entities
		static List<Entity> _entities = new List<Entity>();

		public static List<Entity> Entities {
			get {
				return _entities;
			}
		}

		public static void AddEntity (Entity entity)
		{
			if (!_entities.Contains (entity)) {
				_entities.Add(entity);
			}
		}

		public static void RemoveEntity (Entity entity)
		{
			_entities.Remove(entity);
		}
		#endregion

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
        public GameObject Prefab_Explosion;
        
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
                _explosionPool.Initialize(this.transform, Prefab_Explosion, 10);
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
			FoeSpawnManager.MinTimeToSpawn = FoeSpawnManager.MinTimeToSpawn < 0.3f ? 0.3f :FoeSpawnManager.MinTimeToSpawn;
			FoeSpawnManager.MaxTimeToSpawn = FoeSpawnManager.MaxTimeToSpawn < FoeSpawnManager.MinTimeToSpawn + 0.1f ?
				FoeSpawnManager.MinTimeToSpawn + 0.1f : FoeSpawnManager.MaxTimeToSpawn;
        }


        public void PlayExplosionAt (Vector3 position_WS)
		{
			GameObject explosion = _explosionPool.Get ();
			ParticleSystem particleSystem = explosion.GetComponent<ParticleSystem> ();
			if (particleSystem != null) {
				explosion.transform.position = position_WS;
				particleSystem.Stop();
				particleSystem.Play();
				explosion.PoolRelease(particleSystem.duration);
			}
		}
      
        #endregion
    }
}
