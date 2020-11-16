using Constants;
using Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapon
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public class Weapon : Collectable
    {
        [SerializeField] private float damage = 5f;
        [SerializeField] private GameObject bullet;
        [SerializeField] private Canvas canvasObject;
        [SerializeField] private InputActionAsset inputs;
        [SerializeField] private AudioSource audioSource;
        private bool isUsing;

        // Use this for initialization
        private void Start()
        {
            canvasObject.worldCamera = Camera.main;
            canvasObject.gameObject.SetActive(false);
            audioSource = GetComponent<AudioSource>();
            isUsing = false;
        }

        private void SetUsing(bool isUsing)
        {
            this.isUsing = isUsing;
            InputAction action = inputs.FindAction(GameInput.Fire);
            if (isUsing)
            {
                action.started += Shoot;
                action.Enable();
            }
            else
            {
                action.started -= Shoot;
            }
        }

        private void OnDestroy()
        {
            inputs.FindAction(GameInput.Fire).started -= Shoot;
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);
            bulletObj.SetActive(true);
            bulletObj.GetComponent<Bullet>().damage = damage;
            audioSource.Play();
        }

        public void DoPickup(WeaponHolder holder)
        {
            if (isUsing) return;
            SetUsing(true);
            ShowUI(false);
            holder.SetWeapon(gameObject);
            Debug.Log("Pickup");
            TriggerCollectCallback();
        }

        public void ShowUI(bool show = true)
        {
            canvasObject.gameObject.SetActive(show && !isUsing);
        }
    }
}
