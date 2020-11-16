using Constants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private GameObject interactPopup;
        NPCController currentNPC;
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
            if (currentNPC != null)
            {
                if (currentNPC.IsLastQuestDone())
                {
                    currentNPC.CompleteQuest();
                }
                else if (currentNPC.HasQuests())
                {
                    currentNPC.AcceptQuest();
                }
            }
        }

        void OnTriggerStay2D(Collider2D triggerCollider)
        {
            if (triggerCollider.CompareTag(GameTag.NPC))
            {
                currentNPC = triggerCollider.gameObject.GetComponent<NPCController>();
                if (currentNPC.HasQuests())
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
                currentNPC = null;
            }
        }
    }
}
