using UnityEngine;
using System.Collections;

namespace Utils
{
    /// <summary>
    /// Component for working with Pools
    /// </summary>
    public class PoolReleaser : MonoBehaviour
    {
        GameObjectPool _pool;
        public void mSetFactory(GameObjectPool pool)
        {
            _pool = pool;
        }


        public void mRelease()
        {
            Release();
        }

        private void Release()
        {
            if (_pool != null)
            {
                //Debug.Log("Releasing Object " + this.gameObject.name);
                _pool.Release(this.gameObject);                
            }
        }
    }
}