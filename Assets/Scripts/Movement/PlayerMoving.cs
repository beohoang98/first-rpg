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

        private Rigidbody2D rigidbody;
        private InputAction movementAction;
        private InputAction lookAction;
        private Animator animator;
        private SpriteRenderer renderer;
        private Camera camera;
        
        [SerializeField] private InputActionAsset inputs;
        [SerializeField] private float speed = 5f;
        [SerializeField] private GameObject light2d;
        private static readonly int DirectionIndex = Animator.StringToHash("direction");

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
            camera = Camera.main;

            movementAction = inputs.FindAction(GameInput.Move);
            movementAction.performed += OnMoving;
            movementAction.canceled += OnMoving;
            movementAction.Enable();

            lookAction = inputs.FindAction(GameInput.Look);
            lookAction.performed += OnLook;
            lookAction.Enable();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            if (light2d != null)
            {
                Vector2 position = context.ReadValue<Vector2>();
                light2d.transform.LookAt(camera.ScreenToWorldPoint(position), Vector3.forward);
            }
        }

        private void OnMoving(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            rigidbody.velocity = direction * speed;
            Debug.Log($"Move {direction.ToString()}");

            if (direction.x == 0) {
                if (direction.y > 0) {
                    animator.SetInteger(DirectionIndex, (int)Direction.Back);
                } else if (direction.y < 0) {
                    animator.SetInteger(DirectionIndex, (int)Direction.Front);
                } else {
                    // idle
                    animator.SetInteger(DirectionIndex, (int)Direction.Idle);
                }
            } else
            {
                animator.SetInteger(DirectionIndex, (int)Direction.Side);
                renderer.flipX = !(direction.x > 0);
            }
        }
    }
}
