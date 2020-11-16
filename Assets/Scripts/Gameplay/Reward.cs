using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Gameplay
{
    [System.Serializable]
    public class Reward
    {
        public GameObject rewardObject;
        public string name;
        public RewardType type;
    }

    public enum RewardType
    {
        Weapon,
        Item,
    }
}
