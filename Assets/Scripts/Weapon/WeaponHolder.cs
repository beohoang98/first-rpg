using Constants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapon
{
    public class WeaponHolder : MonoBehaviour
    {
        private static WeaponHolder _instance;
        [SerializeField] private InputActionAsset inputActions;

        private GameObject _weapon;
        private Weapon _touchWeapon;
        private Camera _camera;

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

            _camera = Camera.main;

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
            if (_weapon == null) return;
            _weapon.gameObject.transform.position = transform.position;
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
            if (_weapon == null) return;
            Vector2 mousePos = context.ReadValue<Vector2>();
            Vector2 realPos = _camera.ScreenToWorldPoint(mousePos);
            _weapon.gameObject.transform.rotation =
                Quaternion.LookRotation(Vector3.forward, realPos - (Vector2) transform.position);
        }

        private void OnPickupPress(InputAction.CallbackContext context)
        {
            Debug.Log(context);
            if (_touchWeapon != null && _touchWeapon.gameObject != _weapon)
            {
                _touchWeapon.DoPickup(this);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag(GameTag.Pickup))
            {
                _touchWeapon = collision.GetComponent<Weapon>();
                _touchWeapon.ShowUI();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(GameTag.Pickup))
            {
                _touchWeapon.ShowUI(false);
                _touchWeapon = null;
            }
        }

        public void SetWeapon(GameObject weapon) => _weapon = weapon;
    }
}
