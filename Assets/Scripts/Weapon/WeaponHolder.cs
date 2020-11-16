using Constants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapon
{
    public class WeaponHolder : MonoBehaviour
    {
        private static WeaponHolder _instance;
        [SerializeField] private InputActionAsset inputActions;

        private GameObject weapon;
        private Weapon touchWeapon;
        private Camera camera;

        // Use this for initialization
        private void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }

            camera = Camera.main;

            InputAction action = inputActions.FindAction(GameInput.Look);
            InputAction pickupAction = inputActions.FindAction(GameInput.Obtain);
            action.performed += OnMouseMove;
            pickupAction.performed += OnPickupPress;
            action.Enable();
            pickupAction.Enable();
        }

        // Update is called once per frame
        private void Update()
        {
            if (weapon == null) return;
            weapon.gameObject.transform.position = transform.position;
        }

        private void OnDisable()
        {
            InputAction action = inputActions.FindAction(GameInput.Look);
            InputAction pickupAction = inputActions.FindAction(GameInput.Obtain);
            action.performed -= OnMouseMove;
            pickupAction.performed -= OnPickupPress;
        }

        private void OnMouseMove(InputAction.CallbackContext context)
        {
            if (weapon == null) return;
            Vector2 mousePos = context.ReadValue<Vector2>();
            Vector2 realPos = camera.ScreenToWorldPoint(mousePos);
            weapon.gameObject.transform.rotation =
                Quaternion.LookRotation(Vector3.forward, realPos - (Vector2) transform.position);
        }

        private void OnPickupPress(InputAction.CallbackContext context)
        {
            if (touchWeapon != null && touchWeapon.gameObject != weapon)
            {
                touchWeapon.DoPickup(this);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag(GameTag.Pickup))
            {
                touchWeapon = collision.GetComponent<Weapon>();
                if (touchWeapon != null) touchWeapon.ShowUI();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(GameTag.Pickup))
            {
                if (touchWeapon != null)
                {
                    touchWeapon.ShowUI(false);
                    touchWeapon = null;
                }
            }
        }

        public void SetWeapon(GameObject weapon) => this.weapon = weapon;
    }
}
