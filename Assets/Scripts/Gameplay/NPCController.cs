using System.Collections.Generic;
using Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Gameplay
{
    [ExecuteInEditMode]
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private List<Quest> quests;
        [SerializeField] private TextMeshProUGUI questIcon;
        [SerializeField] private Light2D light2D;
        [SerializeField] private ColorSchema colorSchema;

        private Quest _inProgressQuest;

        public bool IsQuestInProgress() => _inProgressQuest != null;
        public bool HasQuests() => quests.Count > 0;
        public bool IsLastQuestDone() => _inProgressQuest && _inProgressQuest.isDone;

        private void Start()
        {
            _inProgressQuest = null;
            questIcon.color = colorSchema.questWarningColor;
            light2D.color = colorSchema.questWarningColor;
        }

        public void AcceptQuest()
        {
            if (quests.Count > 0 && !_inProgressQuest && !QuestManager.Instance.ActiveQuest)
            {
                _inProgressQuest = quests[0];
                QuestManager.Instance.PushQuest(quests[0]);
                questIcon.color = colorSchema.questInProgressColor;
                light2D.color = colorSchema.questInProgressColor;
            }
        }

        public void CompleteQuest()
        {
            if (_inProgressQuest == null) return;
            
            quests.RemoveAt(0);
            if (quests.Count <= 0)
            {
                questIcon.gameObject.SetActive(false);
                light2D.gameObject.SetActive(false);
            }
            else
            {
                questIcon.color = colorSchema.questWarningColor;
                light2D.color = colorSchema.questWarningColor;
            }

            if (QuestManager.Instance.ActiveQuest == _inProgressQuest)
            {
                QuestManager.Instance.CompleteQuest();
            }
            _inProgressQuest = null;
        }
    }

}
