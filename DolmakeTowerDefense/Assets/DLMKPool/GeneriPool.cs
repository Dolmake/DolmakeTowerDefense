using System;
using System.Collections.Generic;

namespace P8Core.P8Common
{
    public class GenericPool<T> where T : class, new()
    {
        List<T> _used = new List<T>();//used objects
        List<T> _notUsed = new List<T>();//not used objects

        public List<T> UsedObjects { get { return _used; } }

        public int CountUsed { get { return _used.Count; } }
        public int CountNotUsed { get { return _notUsed.Count; } }


        bool _initialized = false;
        public bool IsInitialized { get { return _initialized; } }

        int _maxUsedAtTime = int.MaxValue;
        /// <summary>
        /// Indicates the maximun number of objects in the pool
        /// </summary>
        public int MaxObjectsUsedAtTime { get { return _maxUsedAtTime; } set { _maxUsedAtTime = value; } }

        /// <summary>
        /// Invoke to initialize the pool
        /// </summary>
        /// <param name="startCapacity"> Start capacity</param>
        /// <returns>True if success</returns>
        public bool Initialize(int startCapacity)
        {
            if (!_initialized)
            {
                for (int i = 0; i < startCapacity; ++i)
                {
                    Get(true);
                }
                _ReleaseAll();
                _initialized = true;
            }
            return _initialized;
        }

        /// <summary>
        /// Invoke to finish the pool
        /// </summary>
        /// <returns></returns>
        public bool Deinitialize()
        {
            if (_initialized)
            {
                _ReleaseAll();
                while (_notUsed.Count > 0)
                {
                    //GC... is your Turn
                    _notUsed[0] = null;
                    _notUsed.RemoveAt(0);
                }
                _initialized = false;
            }
            return !_initialized;
        }

        /// <summary>
        /// Get an object from pool
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            return Get(false);
        }

        /// <summary>
        /// Get an object from pool
        /// </summary>
        /// <param name="forceCreation">force Instantiate</param>
        /// <returns></returns>
        public T Get(bool forceCreation)
        {
            if (forceCreation) return _Instantiate();
            if (_used.Count < MaxObjectsUsedAtTime)
            {
                if (_notUsed.Count > 0)
                {
                    T res = _notUsed[0];
                    _notUsed.RemoveAt(0);
                    _used.Add(res);
                    return res;
                }
                else return _Instantiate();
            }
            else return null;
        }


        /// <summary>
        /// Release an object into the pool
        /// </summary>
        /// <param name="o"></param>
        public void Release(T o)
        {
            if (_used.Remove(o))
            {
                _notUsed.Add(o);
            }
        }



        void _ReleaseAll()
        {
            while (_used.Count > 0)
            {
                _notUsed.Add(_used[0]);
                _used.RemoveAt(0);
            }
        }
        T _Instantiate()
        {
            T o = new T();
            _used.Add(o);
            return o;
        }
    }
}
