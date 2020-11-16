using Manager;
using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 20f;
        private float health;

        public OnHealthChange onHealthChange = delegate(float f) {  };
        public OnDie onDie = delegate {  };
        
        private void Start()
        {
            health = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            onHealthChange.Invoke(health);
            if (health <= 0)
            {
                onDie.Invoke();
                GameManager.instance.GameOver();
            }
        }

        public float GetMaxHealth() => maxHealth;

        public delegate void OnHealthChange(float health);
        public delegate void OnDie();
    }
}
