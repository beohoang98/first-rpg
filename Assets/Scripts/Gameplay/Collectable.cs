using System;
using UnityEngine;

namespace Gameplay
{
    public abstract class Collectable : MonoBehaviour
    {
        private OnCollect _onCollect = delegate {  };
        public delegate void OnCollect();

        public void AddEventCollected(OnCollect cb)
        {
            _onCollect += cb;
        }
        protected void TriggerCollectCallback()
        {
            _onCollect.Invoke();
            _onCollect = delegate {  };
        }
    }
}
