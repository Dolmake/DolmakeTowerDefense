using UnityEngine;

namespace Utils
{
    public static class HelperClass
    {

        /// <summary>
        /// Extension method for getting interfaces
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static T GetInterface<T>(this Component component) where T : class
        {
            return component.GetComponent(typeof(T)) as T;
        }

        /// <summary>
        /// Extension method for getting interfaces
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static T[] GetInterfaces<T>(this Component component) where T : class
        {
            //DOES NOT WORK !!!!  Component[] components = component.GetComponents(typeof(T)) as T[];
            Component[] components = component.GetComponents(typeof(T));// as T[];
            if (components != null)
            {
                T[] result = new T[components.Length];
                for (int i = 0; i < components.Length; ++i)
                    result[i] = components[i] as T;

                return result;
            }
            else return null;
        }

        /// <summary>
        /// Extension method for getting interfaces
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static T GetInterface<T>(this GameObject go) where T : class
        {
            return go.GetComponent(typeof(T)) as T;
        }

        /// <summary>
        /// Extension method for getting interfaces
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static T[] GetInterfaces<T>(this GameObject go) where T : class
        {
            //DOES NOT WORK !!!!  Component[] components = component.GetComponents(typeof(T)) as T[];
            Component[] components = go.GetComponents(typeof(T));// as T[];
            if (components != null)
            {
                T[] result = new T[components.Length];
                for (int i = 0; i < components.Length; ++i)
                    result[i] = components[i] as T;

                return result;
            }
            else return null;
        }
		/*
        /// <summary>
        /// Extension method for releasing object from a pool
        /// </summary>
        /// <param name="go"></param>
        public static void PoolRelease(this GameObject go)
        {
            go.SendMessage("mRelease", SendMessageOptions.DontRequireReceiver);
        }
        */

        /// <summary>
        /// Check distances
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static bool DistanceLessThan(Vector3 a, Vector3 b, float distance)
        {
            return (b - a).sqrMagnitude < (distance * distance);
        }
       
    }
}
