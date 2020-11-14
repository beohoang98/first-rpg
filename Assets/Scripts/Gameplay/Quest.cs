using System;
using Assets.Scripts.Gameplay;
using UnityEditor;
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
        public MonoScript targetType;

        [SerializeField] int requiredAmount = 0;
        int _amount;

        public int RequiredAmount => requiredAmount;
        public int Amount => _amount;
        public OnUpdate onUpdates;

        public bool isDone => _amount >= requiredAmount;

        public void DoCollected()
        {
            if (type == QuestType.COLLECT) ++_amount;
            onUpdates.Invoke(this);
        }
        
        public void DoKilled()
        {
            if (type == QuestType.KILL) ++_amount;
            onUpdates.Invoke(this);
        }

        public delegate void OnUpdate(in Quest quest);

        private void OnEnable()
        {
            _amount = 0;
        }
    }

    [Serializable]
    public enum QuestType
    {
        KILL,
        COLLECT,
    }
}
