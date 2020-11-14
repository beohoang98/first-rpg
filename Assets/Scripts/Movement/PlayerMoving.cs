using Constants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class PlayerMoving : MonoBehaviour
    {
        private enum Direction {
            Idle = -1,
            Front = 0,
            Side = 1,
            Back = 2,
        }

        private Rigidbody2D _rigidbody;
        private InputAction _movementAction;
        private InputAction _lookAction;
        private Animator _animator;
        private SpriteRenderer _renderer;
        private Camera _camera;
        
        [SerializeField] private InputActionAsset inputs;
        [SerializeField] private float speed = 5f;
        [SerializeField] private GameObject light2d;
        private static readonly int DirectionIndex = Animator.StringToHash("direction");

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
            _camera = Camera.main;

            _movementAction = inputs.FindAction(GameInput.Move);
            _movementAction.performed += OnMoving;
            _movementAction.canceled += OnMoving;
            _movementAction.Enable();

            _lookAction = inputs.FindAction(GameInput.Look);
            _lookAction.performed += OnLook;
            _lookAction.Enable();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            if (light2d != null)
            {
                Vector2 position = context.ReadValue<Vector2>();
                light2d.transform.LookAt(_camera.ScreenToWorldPoint(position), Vector3.forward);
            }
        }

        private void OnMoving(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            _rigidbody.velocity = direction * speed;
            Debug.Log($"Move {direction.ToString()}");

            if (direction.x == 0) {
                if (direction.y > 0) {
                    _animator.SetInteger(DirectionIndex, (int)Direction.Back);
                } else if (direction.y < 0) {
                    _animator.SetInteger(DirectionIndex, (int)Direction.Front);
                } else {
                    // idle
                    _animator.SetInteger(DirectionIndex, (int)Direction.Idle);
                }
            } else
            {
                _animator.SetInteger(DirectionIndex, (int)Direction.Side);
                _renderer.flipX = !(direction.x > 0);
            }
        }
    }
}
