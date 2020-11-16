using System;
using Constants;
using Gameplay;
using UnityEngine;

namespace Enemy
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class FightingArea : MonoBehaviour
    {
        [SerializeField] private Enemy[] enemies;
        [SerializeField] private PlayerController player;

        private void Start()
        {
            enemies = GetComponentsInChildren<Enemy>();
            player = FindObjectOfType<PlayerController>();
        }

        private void Reset()
        {
            Start();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTag.Player))
            {
                foreach (var enemy in enemies)
                {
                    enemy.OnPlayerEnterFightingArea(player.gameObject);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(GameTag.Player))
            {
                foreach (var enemy in enemies)
                {
                    enemy.OnPlayerLeaveFightingArea(player.gameObject);
                }
            }
        }
    }
}
