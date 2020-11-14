using Constants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private GameObject interactPopup;
        NPCController _currentNPC;
        [SerializeField] private InputActionAsset inputActionAsset;

        void Start()
        {
            interactPopup.SetActive(false);
            InputAction interactAction = inputActionAsset.FindAction(GameInput.Interact);
            interactAction.Enable();
            interactAction.started += OnInteract;
        }

        void OnInteract(InputAction.CallbackContext context)
        {
            if (_currentNPC != null)
            {
                if (_currentNPC.IsLastQuestDone())
                {
                    _currentNPC.CompleteQuest();
                }
                else if (_currentNPC.HasQuests())
                {
                    _currentNPC.AcceptQuest();
                }
            }
        }

        void OnTriggerStay2D(Collider2D triggerCollider)
        {
            if (triggerCollider.CompareTag(GameTag.NPC))
            {
                _currentNPC = triggerCollider.gameObject.GetComponent<NPCController>();
                if (_currentNPC.HasQuests())
                {
                    interactPopup.transform.position = triggerCollider.transform.position + new Vector3(0, 1, 0);
                    interactPopup.SetActive(true);
                }
            }
        }

        void OnTriggerExit2D(Collider2D triggerCollider)
        {
            if (triggerCollider.CompareTag("NPC"))
            {
                interactPopup.SetActive(false);
                _currentNPC = null;
            }
        }
    }
}
