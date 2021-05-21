using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace LocationRPG
{
    public class Monster : Unit
    {
        protected double _rewardXp;

        public double RewardXp => _rewardXp;
        public Monster() : base()
        {
        }
        
    }
}