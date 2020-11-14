using Attributes;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class QuestWindow : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI currentAmount;
        [SerializeField] private TextMeshProUGUI requiredAmount;
        [SerializeField] private TextMeshProUGUI passText;
        [SerializeField] private ColorSchema colorSchema;

        private void Start()
        {
            panel.SetActive(false);
            passText.gameObject.SetActive(false);
        }

        public void SetQuest(ref Quest quest)
        {
            OnQuestUpdate(quest);
            quest.onUpdates += OnQuestUpdate;
            panel.SetActive(true);
        }

        public void Hide()
        {
            panel.SetActive(false);
        }

        private void OnQuestUpdate(in Quest quest)
        {
            title.SetText(quest.title);
            description.SetText(quest.description);
            requiredAmount.SetText($"{quest.RequiredAmount.ToString()}");
            currentAmount.SetText($"{quest.Amount.ToString()}");
            if (quest.isDone)
            {
                title.color = colorSchema.questCompleteColor;
                title.fontStyle = FontStyles.Strikethrough;
                description.fontStyle = FontStyles.Strikethrough;
                passText.gameObject.SetActive(true);
                passText.color = colorSchema.questCompleteColor;
            }
            else
            {
                title.color = colorSchema.questInProgressColor;
                title.fontStyle = FontStyles.Normal;
                description.fontStyle = FontStyles.Normal;
                passText.gameObject.SetActive(false);
            }
        }
    }
}
