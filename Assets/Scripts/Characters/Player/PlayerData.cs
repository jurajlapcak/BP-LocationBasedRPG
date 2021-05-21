using System;

namespace LocationRPG
{
    [Serializable]
    public class PlayerData
    {
        private int _level;
        private double _xp;
        private double _requiredXp;
        private double _baseXp;
        private double _xpRate;
        private double _xpSum;
        private double _hp;
        private double _defense;
        private double _attack;

        public int Level => _level;

        public double Xp => _xp;

        public double RequiredXp => _requiredXp;

        public double BaseXp => _baseXp;

        public double XpRate => _xpRate;

        public double XpSum => _xpSum;
        
        public double Hp => _hp;

        public double Defense => _defense;

        public double Attack => _attack;

        public PlayerData(Player playerStats)
        {
            _level = playerStats.Level;
            _xp = playerStats.Xp;
            _requiredXp = playerStats.RequiredXp;
            _baseXp = playerStats.BaseXp;
            _xpRate = playerStats.XpRate;
            _xpSum = playerStats.XpSum;
            _hp = playerStats.Hp;
            _defense = playerStats.Defense;
            _attack = playerStats.Attack;
        }
    }
}