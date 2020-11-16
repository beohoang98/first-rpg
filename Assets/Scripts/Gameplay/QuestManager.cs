using System.Linq;
using Assets.Scripts.Gameplay;
using UnityEngine;

namespace Gameplay
{
    public class QuestManager : MonoBehaviour
    {
        private static QuestManager _instance;
        public static QuestManager Instance => _instance;

        private Quest activeQuest;
        public Quest ActiveQuest => activeQuest;
        [SerializeField] private QuestWindow questWindow;
        [SerializeField] private AudioClip completeAudio;

        // Use this for initialization
        void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }

            activeQuest = null;
        }

        public void PushQuest(Quest quest)
        {
            activeQuest = quest;
            questWindow.SetQuest(ref quest);
            activeQuest.onUpdates += OnQuestUpdate;
            QuestItem[] list = Resources.FindObjectsOfTypeAll<QuestItem>();
            QuestItem[] filtered = list.Where(item => item.GetType().Name.Equals(quest.targetItemName)).ToArray();

            switch (quest.type)
            {
                case QuestType.Kill:
                    foreach (var item in filtered)
                    {
                        if (item is Killable killable)
                        {
                            killable.AddEventKilled(quest.DoKilled);
                        }
                    }

                    break;
                case QuestType.Collect:
                    foreach (var item in filtered)
                    {
                        if (item is Collectable collectable)
                        {
                            collectable.AddEventCollected(quest.DoCollected);
                        }
                    }

                    break;
            }
        }

        private void OnQuestUpdate(in Quest quest)
        {
            if (!quest.isDone) return;
            switch (quest.reward.type)
            {
                case RewardType.Item:
                    // spawn items
                    break;
                case RewardType.Weapon:
                    // do nothing
                    break;
            }
        }

        public void CompleteQuest()
        {
            activeQuest = null;
            questWindow.Hide();
            if (completeAudio != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(completeAudio, Camera.main.transform.position);
            }
        }
    }
}
