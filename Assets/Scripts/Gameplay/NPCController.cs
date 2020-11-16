using System;
using System.Collections.Generic;
using Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

        private Quest inProgressQuest;

        public bool IsQuestInProgress() => inProgressQuest != null;
        public bool HasQuests() => quests.Count > 0;
        public bool IsLastQuestDone() => inProgressQuest && inProgressQuest.isDone;
        [SerializeField] private UnityEvent onCompleteAllQuests;

        private void Start()
        {
            inProgressQuest = null;
            questIcon.color = colorSchema.questWarningColor;
            light2D.color = colorSchema.questWarningColor;
        }

        public void AcceptQuest()
        {
            if (quests.Count > 0 && !inProgressQuest && !QuestManager.Instance.ActiveQuest)
            {
                inProgressQuest = quests[0];
                QuestManager.Instance.PushQuest(quests[0]);
                questIcon.color = colorSchema.questInProgressColor;
                light2D.color = colorSchema.questInProgressColor;
            }
        }

        public void CompleteQuest()
        {
            if (inProgressQuest == null) return;

            quests.RemoveAt(0);
            if (quests.Count <= 0)
            {
                questIcon.gameObject.SetActive(false);
                light2D.gameObject.SetActive(false);
                onCompleteAllQuests.Invoke();
            }
            else
            {
                questIcon.color = colorSchema.questWarningColor;
                light2D.color = colorSchema.questWarningColor;
            }

            if (QuestManager.Instance.ActiveQuest == inProgressQuest)
            {
                QuestManager.Instance.CompleteQuest();
            }

            inProgressQuest = null;
        }
    }
}
