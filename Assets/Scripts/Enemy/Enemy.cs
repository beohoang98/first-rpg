using Attributes;
using Constants;
using Gameplay;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AIDestinationSetter))]
    [RequireComponent(typeof(AIPath))]
    public class Enemy : Killable
    {
        [SerializeField] private int damage = 10;
        [SerializeField] private float maxHealth = 30f;
        [SerializeField] private ColorSchema colorSchema;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Slider healthSlider;

        private AIDestinationSetter aiDestinationSetter;

        private AIPath aiPath;
        private float health = 30f;
        private Vector3 startPoint;

        private void Start()
        {
            canvas.worldCamera = Camera.main;
            healthSlider.gameObject.SetActive(false);
            startPoint = transform.position;
            aiPath = GetComponent<AIPath>();
            aiDestinationSetter = GetComponent<AIDestinationSetter>();

            healthSlider.minValue = 0f;
            healthSlider.maxValue = maxHealth;
            healthSlider.fillRect.gameObject.GetComponent<Image>().color = colorSchema.healthColor;
            health = maxHealth;
        }

        public void OnPlayerEnterFightingArea(GameObject player)
        {
            aiDestinationSetter.target = player.transform;
        }

        public void OnPlayerLeaveFightingArea(GameObject player)
        {
            aiDestinationSetter.target = null;
            aiPath.destination = startPoint;
        }

        public void TakeDamage(float _damage)
        {
            health -= _damage;
            UpdateHealthBar();
            if (health > 0) return;
            TriggerKilledCallback();
            Destroy(gameObject);
        }

        private void UpdateHealthBar()
        {
            healthSlider.value = health;
            healthSlider.gameObject.SetActive(true);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(GameTag.Player))
            {
                other.collider.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
    }
}
