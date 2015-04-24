using System;
using System.Collections.Generic;
using UnityEngine;

namespace DLMKPool
{
    /// <summary>
    /// Implementation
    /// </summary>
    public class GameComponentPool<T> : IPool<T> where T : Component
    {
        T _prefab;
        Transform _parent;
        List<T> _used = new List<T>();
        List<T> _notUsed = new List<T>();

        public List<T> UsedObjects { get { return _used; } }


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
        public bool Initialize(Transform parent, T prefab, int startCapacity)
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
        public T Get()
        {
            return Get(false);
        }

        /// <summary>
        /// Returns a Gameobject forcing an Instantiate
        /// </summary>
        /// <param name="forceInstantiate"></param>
        /// <returns></returns>
        public T Get(bool forceInstantiate)
        {
            T result = null;

            if (forceInstantiate) result = _Instantiate();
            else
            {
                if (_used.Count < MaxObjectsUsedAtTime)
                {
                    if (_notUsed.Count > 0)
                    {
                        T res = _notUsed[_notUsed.Count - 1];
                        _notUsed.RemoveAt(_notUsed.Count - 1);
                        _used.Add(res);
                        _Activate(res.gameObject);
                        result = res;
                    }
                    else result = _Instantiate();
                }
                else result = null;
            }

            if (OnGetInvoker != null)
                OnGetInvoker(result);
            return result;
        }

        /// <summary>
        /// Release a Gameobject
        /// </summary>
        /// <param name="o"></param>
        public bool Release(T o)
        {
            if (o != null && _used.Remove(o))
            {
                _Deactivate(o.gameObject);
                _notUsed.Add(o);
                if (OnReleaseInvoker != null) OnReleaseInvoker(o);
                return true;
            }
            else return false;
        }

        bool _Release(object o)
        {
            return Release((T)o);
        }


        #region MISC

        void _ReleaseAll()
        {
            while (_used.Count > 0)
            {
                if (_used[0] != null)
                {
                    _Deactivate(_used[0].gameObject);
                    _notUsed.Add(_used[0]);
                    if (OnReleaseInvoker != null) OnReleaseInvoker(_used[0]);
                }
                _used.RemoveAt(0);
            }
        }

        T _Instantiate()
        {
            T o = Component.Instantiate(_prefab) as T;

            if (o == null)
                Debug.Log(string.Format("Can not instantiate {0}", _prefab.name));

            //Always add a factoryReleaser to simplify deallocation
            PoolReleaser releaser = o.GetComponent<PoolReleaser>();
            if (releaser == null)
                releaser = o.gameObject.AddComponent<PoolReleaser>();
            releaser.mSetDelegate(new PoolReleaser.ReleaseDelegate(_Release), o);//.mSetFactory(this);


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
            o.SetActive(false);
            //On deactivate always reassing parent...
            o.transform.parent = _parent;
        }

        #endregion



        System.Action<T> OnGetInvoker;
        public event System.Action<T> OnGet
        {
            add
            {
                OnGetInvoker += value;
            }
            remove
            {
                OnGetInvoker -= value;
            }
        }

        System.Action<T> OnReleaseInvoker;
        public event System.Action<T> OnRelease
        {
            add
            {
                OnReleaseInvoker += value;
            }
            remove
            {
                OnReleaseInvoker -= value;
            }
        }
    }
}
