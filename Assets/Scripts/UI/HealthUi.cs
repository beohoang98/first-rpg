using System;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [ExecuteInEditMode]
    public class HealthUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthLabel;
        [SerializeField] private Slider slider;
        [SerializeField] private PlayerController player;

        private void Awake()
        {
            healthLabel = GetComponentInChildren<TextMeshProUGUI>();
            slider = GetComponentInChildren<Slider>();
            player = FindObjectOfType<PlayerController>();

            if (player)
            {
                slider.maxValue = player.GetMaxHealth();
                slider.minValue = 0;
                player.onHealthChange += SetHealth;
                SetHealth(player.GetMaxHealth());
            }
        }

        public void SetHealth(float health)
        {
            healthLabel.SetText($"{health:F}");
            slider.value = health;
        }
    }
}
