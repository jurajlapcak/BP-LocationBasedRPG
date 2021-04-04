using System;

namespace LocationRPG
{
    [Serializable]
    public class PlayerData
    {
        private int _level;
        private int _xp;
        private int _requiredXp;
        private int _levelBase;
        private float _hp;
        private float _defense;
        private float _attack;

        //TODO: inventory

        public int Level => _level;

        public int Xp => _xp;

        public int RequiredXp => _requiredXp;

        public int LevelBase => _levelBase;

        public float Hp => _hp;

        public float Defense => _defense;

        public float Attack => _attack;
        
        public PlayerData(PlayerStats playerStats)
        {
            _level = playerStats.Level;
            _xp = playerStats.Xp;
            _requiredXp = playerStats.RequiredXp;
            _levelBase = playerStats.LevelBase;
            
            _hp = playerStats.Hp;
            _defense = playerStats.Defense;
            _attack = playerStats.Attack;
        }
    }
}