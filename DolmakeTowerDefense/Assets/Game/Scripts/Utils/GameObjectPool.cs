using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utils
{
    public interface IPool
    {
        /// <summary>
        /// Max objects used at the same time
        /// </summary>
        int MaxObjectsUsedAtTime { get; }

        /// <summary>
        /// Is Factory Initialized?
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Internal list of Used objects
        /// </summary>
        List<GameObject> UsedObjects { get; }

        /// <summary>
        /// Initialize the Factory
        /// </summary>
        /// <param name="parent">Parent to attach new GameObjects</param>
        /// <param name="prefab">Prefab to clone</param>
        /// <param name="capacity">Initial size</param>
        /// <returns>True if success</returns>
        bool Initialize(Transform parent, GameObject prefab, int capacity);

        /// <summary>
        /// Deinitialize the Factory
        /// </summary>
        /// <returns></returns>
        bool Deinitialize();

        /// <summary>
        /// Get a new Gameobject
        /// </summary>
        /// <returns></returns>
        GameObject Get();

        /// <summary>
        /// Get a new object forcing an Instantiate
        /// </summary>
        /// <param name="forceInstantiate"></param>
        /// <returns></returns>
        GameObject Get(bool forceInstantiate);

        /// <summary>
        /// Release the GameObject. Use it when finish the use
        /// </summary>
        /// <param name="o"></param>
        void Release(GameObject o);

    }

    /// <summary>
    /// Implementation
    /// </summary>
    public class GameObjectPool : IPool
    {
        GameObject _prefab;
        Transform _parent;
        List<GameObject> _used = new List<GameObject>();
        List<GameObject> _notUsed = new List<GameObject>();

        public List<GameObject> UsedObjects { get { return _used; } }


        bool _initialized = false;
        public bool IsInitialized { get { return _initialized; } }

        int _maxUsedAtTime = int.MaxValue;
        public int MaxObjectsUsedAtTime { get { return _maxUsedAtTime; } set { _maxUsedAtTime = value; } }

        /// <summary>
        /// Initialize the factory.
        /// </summary>
        /// <param name="parent"> Parent reference to hold the Gameobjects</param>
        /// <param name="prefab">Prefab for the Gameobjects</param>
        /// <param name="startCapacity">Start Capacity</param>
        /// <returns></returns>
        public bool Initialize(Transform parent, GameObject prefab, int startCapacity)
        {
            if (!_initialized)
            {
                _prefab = prefab;
                _parent = parent;

                //Create the Gameobjects...
                for (int i = 0; i < startCapacity; ++i)
                {
                    Get(true);
                }
                //...and release them
                _ReleaseAll();
                _initialized = true;
            }
            return _initialized;
        }

        /// <summary>
        /// Deinitialize the factory. Invoke when finish
        /// </summary>
        /// <returns></returns>
        public bool Deinitialize()
        {
            if (_initialized)
            {
                _ReleaseAll();

                //Destroy all of the Gameobjects
                while (_notUsed.Count > 0)
                {
                    GameObject.DestroyImmediate(_notUsed[0]);
                    _notUsed.RemoveAt(0);
                }

                _initialized = false;
            }
            return !_initialized;
        }

        /// <summary>
        /// Returns a Gameobject
        /// </summary>
        /// <returns></returns>
        public GameObject Get()
        {
            return Get(false);
        }

        /// <summary>
        /// Returns a Gameobject forcing an Instantiate
        /// </summary>
        /// <param name="forceInstantiate"></param>
        /// <returns></returns>
        public GameObject Get(bool forceInstantiate)
        {
            if (forceInstantiate) return _Instantiate();
            if (_used.Count < MaxObjectsUsedAtTime)
            {
                if (_notUsed.Count > 0)
                {
                    GameObject res = _notUsed[_notUsed.Count - 1];
                    _notUsed.RemoveAt(_notUsed.Count - 1);
                    _used.Add(res);
                    _Activate(res);
                    return res;
                }
                else return _Instantiate();
            }
            else return null;
        }

        /// <summary>
        /// Release a Gameobject
        /// </summary>
        /// <param name="o"></param>
        public void Release(GameObject o)
        {
            if (o != null && _used.Remove(o))
            {
                _Deactivate(o);
                _notUsed.Add(o);
            }
        }


        #region MISC

        void _ReleaseAll()
        {
            while (_used.Count > 0)
            {
                if (_used[0] != null)
                {
                    _Deactivate(_used[0]);
                    _notUsed.Add(_used[0]);
                }
                _used.RemoveAt(0);
            }
        }
        GameObject _Instantiate()
        {
            GameObject o = GameObject.Instantiate(_prefab) as GameObject;

            //Always add a factoryReleaser to simplify deallocation
            PoolReleaser releaser = o.GetComponent<PoolReleaser>();
            if (releaser == null)
                releaser = o.AddComponent<PoolReleaser>();
            releaser.mSetFactory(this);

            o.transform.parent = _parent;
            _used.Add(o);
            o.name = _prefab.name + "_" + _used.Count;
            return o;
        }
        void _Activate(GameObject o)
        {
            o.SetActive(true);
        }
        void _Deactivate(GameObject o)
        {
            o.transform.parent = _parent;
            //On deactivate always reasing parent...
            o.SetActive(false);          
           
        }

        #endregion


    }

}

