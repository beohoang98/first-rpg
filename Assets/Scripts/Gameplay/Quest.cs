using System;
using Assets.Scripts.Gameplay;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    [CreateAssetMenu(fileName = "Quest", menuName = "Create Quest")]
    public class Quest : ScriptableObject
    {
        public string title;
        public string description;
        public QuestType type;
        public Reward reward;
        public string targetItemName;

        [SerializeField] int requiredAmount = 0;
        int amount;

        public int RequiredAmount => requiredAmount;
        public int Amount => amount;
        public OnUpdate onUpdates;

        public bool isDone => amount >= requiredAmount;

        public void DoCollected()
        {
            if (type == QuestType.Collect) ++amount;
            onUpdates.Invoke(this);
        }

        public void DoKilled()
        {
            if (type == QuestType.Kill) ++amount;
            onUpdates.Invoke(this);
        }

        public delegate void OnUpdate(in Quest quest);

        private void OnEnable()
        {
            amount = 0;
        }
    }

    [Serializable]
    public enum QuestType
    {
        Kill,
        Collect,
    }
}
