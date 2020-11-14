using Attributes;
using Gameplay;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AIDestinationSetter))]
    public class Enemy : Killable
    {
        [SerializeField] private float maxHealth = 30f;
        private float _health = 30f;

        [SerializeField] private ColorSchema colorSchema;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Slider healthSlider;
        private Vector3 _startPoint;
        private AIPath _aiPath;
        private AIDestinationSetter _aiDestinationSetter;

        private void Start()
        {
            canvas.worldCamera = Camera.main;
            healthSlider.gameObject.SetActive(false);
            _startPoint = transform.localPosition;
            _aiPath = GetComponent<AIPath>();
            _aiDestinationSetter = GetComponent<AIDestinationSetter>();
            
            healthSlider.minValue = 0f;
            healthSlider.maxValue = maxHealth;
            healthSlider.fillRect.GetComponent<SpriteRenderer>().color = colorSchema.healthColor;
            _health = maxHealth;
        }

        public void OnPlayerEnterFightingArea(GameObject player)
        {
            _aiDestinationSetter.target = player.transform;
        }

        public void OnPlayerLeaveFightingArea(GameObject player)
        {
            _aiDestinationSetter.target = null;
            _aiPath.destination = _startPoint;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            UpdateHealthBar();
            if (_health > 0) return;
            TriggerKilledCallback();
            Destroy(gameObject);
        }

        private void UpdateHealthBar() 
        {
            healthSlider.value = _health;
            healthSlider.gameObject.SetActive(true);
        }
    }
}
