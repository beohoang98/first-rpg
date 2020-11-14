using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public abstract class Killable : MonoBehaviour
    {
        private OnKilled _onKilled = delegate {  };
        public delegate void OnKilled();

        public void AddEventKilled(OnKilled cb)
        {
            _onKilled += cb;
        }
        
        protected void TriggerKilledCallback()
        {
            _onKilled.Invoke();
            _onKilled = delegate {  };
        }
    }
}
