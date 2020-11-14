using Constants;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class FightingArea : MonoBehaviour
    {
        [SerializeField] private Enemy[] enemies;
        [SerializeField] private GameObject player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTag.Player))
            {
                foreach (var enemy in enemies)
                {
                    enemy.OnPlayerEnterFightingArea(player);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(GameTag.Player))
            {
                foreach (var enemy in enemies)
                {
                    enemy.OnPlayerLeaveFightingArea(player);
                }
            }
        }
    }
}
