using System;
using System.Collections.Generic;
using UnityEngine;


namespace DLMKPool
{
    public interface IPool<T> where T : class
    {

        event System.Action<T> OnGet, OnRelease;

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
        List<T> UsedObjects { get; }

        /// <summary>
        /// Initialize the Factory
        /// </summary>
        /// <param name="parent">Parent to attach new GameObjects</param>
        /// <param name="prefab">Prefab to clone</param>
        /// <param name="capacity">Initial size</param>
        /// <returns>True if success</returns>
        bool Initialize(Transform parent, T prefab, int capacity);

        /// <summary>
        /// Deinitialize the Factory
        /// </summary>
        /// <returns></returns>
        bool Deinitialize();

        /// <summary>
        /// Get a new Gameobject
        /// </summary>
        /// <returns></returns>
        T Get();

        /// <summary>
        /// Get a new object forcing an Instantiate
        /// </summary>
        /// <param name="forceInstantiate"></param>
        /// <returns></returns>
        T Get(bool forceInstantiate);

        /// <summary>
        /// Release the GameObject. Use it when finish the use
        /// </summary>
        /// <param name="o"></param>
        bool Release(T o);

    }

       
}
