using Assets.Scripts.Gameplay;
using UnityEngine;

namespace Gameplay
{
    public class QuestManager : MonoBehaviour
    {
        private static QuestManager _instance;
        public static QuestManager Instance => _instance;

        private Quest _activeQuest;
        public Quest ActiveQuest => _activeQuest;
        [SerializeField] QuestWindow questWindow;

        // Use this for initialization
        void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            } else
            {
                Destroy(this);
            }
            _activeQuest = null;
        }

        public void PushQuest(Quest quest)
        {
            _activeQuest = quest;
            questWindow.SetQuest(ref quest);
            _activeQuest.onUpdates += OnQuestUpdate;
            Object[] list = FindObjectsOfType(quest.targetType.GetClass());
            switch (quest.type)
            {
                case QuestType.KILL:
                    foreach (var item in list)
                    {
                        if (item is Killable killable)
                        {
                            killable.AddEventKilled(quest.DoKilled);
                        }
                    }

                    break;
                case QuestType.COLLECT:
                    foreach (var item in list)
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
                case RewardType.ITEM:
                    // spawn items
                    break;
                case RewardType.WEAPON:
                    // do nothing
                    break;
            }
        }

        public void CompleteQuest()
        {
            _activeQuest = null;
            questWindow.Hide();
        }
        
        
    }
}
