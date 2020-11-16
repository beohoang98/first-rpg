using System;
using System.Collections;
using Constants;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Items
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Light2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class GameItem : Collectable
    {
        [SerializeField] public GameItemInfo info;
        [SerializeField] private float throwSpeed = 5f;

        [SerializeField] private Light2D light2D;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private TextMeshProUGUI nameLabel;
        [SerializeField] private TextMeshProUGUI keyboardHintLabel;
        [SerializeField] private AudioClip collectSound;

        private bool isInReach = false;
        [SerializeField] public InputAction inputAction { private set; get; }

        private void Start()
        {
            tag = GameTag.Pickup;
            light2D = GetComponent<Light2D>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            inputAction = inputActionAsset.FindAction(GameInput.Obtain);

            nameLabel.SetText(info.name);
            keyboardHintLabel.SetText(inputAction.bindingMask.ToString());

            inputAction.performed += OnPickup;
            inputAction.Enable();

            nameLabel.gameObject.SetActive(false);
            keyboardHintLabel.gameObject.SetActive(false);
        }

        private void Reset()
        {
            Start();
        }

        public void Appear()
        {
            double randomDegree = Random.Range(0, 360) * Math.PI / 180;
            rigidbody2D.velocity = new Vector2(
                x: (float) Math.Cos(randomDegree),
                y: (float) Math.Sin(randomDegree)
            ) * throwSpeed;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (rigidbody2D.velocity.magnitude > 0.1)
            {
                rigidbody2D.velocity *= (0.7f * Time.deltaTime);
            }
            else
            {
                rigidbody2D.velocity = Vector2.zero;
            }

            light2D.intensity = (float) Math.Sin(Time.time * Math.PI) * 0.5f + 1f;
        }

        private void OnPickup(InputAction.CallbackContext context)
        {
            if (isInReach)
            {
                if (collectSound != null) AudioSource.PlayClipAtPoint(collectSound, transform.position);
                TriggerCollectCallback();
                gameObject.SetActive(false);
                StartCoroutine(DestroyAfter(1000));
            }
        }

        private IEnumerator DestroyAfter(float milis)
        {
            yield return new WaitForSeconds(milis / 1000f);
            Destroy(gameObject);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(GameTag.Player))
            {
                isInReach = true;
                nameLabel.gameObject.SetActive(true);
                keyboardHintLabel.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(GameTag.Player))
            {
                isInReach = false;
                nameLabel.gameObject.SetActive(false);
                keyboardHintLabel.gameObject.SetActive(false);
            }
        }
    }
}
