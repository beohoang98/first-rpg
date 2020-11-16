using System;
using Constants;
using UnityEngine;

namespace Weapon
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(AudioSource))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D rigidbody;
        [SerializeField] private float speed;
        [SerializeField] private AudioSource noiseSource;

        // Use this for initialization
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = transform.up * speed;

            noiseSource = GetComponent<AudioSource>();
            noiseSource.loop = false;
        }

        private void Reset()
        {
            Start();
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
