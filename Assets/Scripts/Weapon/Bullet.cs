using Constants;
using UnityEngine;

namespace Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        [SerializeField] private float speed;

        // Use this for initialization
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = transform.up * speed;
        }

        public float damage { get; set; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(GameTag.Ground))
            {
                Destroy(gameObject);
            }
            else if (collision.collider.CompareTag(GameTag.Enemy))
            {
                Destroy(gameObject);
                collision.collider.gameObject.GetComponent<Enemy.Enemy>().TakeDamage(damage);
            }
        }
    }
}
