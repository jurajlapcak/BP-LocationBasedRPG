using System;
using UnityEngine;

namespace LocationRPG
{
    public class Player : Unit
    {
        //current xp
        private double _xp;

        //xp from level 1, 0 xp, to current level and xp 
        private double _xpSum;

        //xp from 0 to xp required to level up 
        private double _requiredXp;

        //base xp used to calculate level up formula
        private double _baseXp;

        private double _xpRate;

        public static event Action LeveledUp;

        public double Xp
        {
            get => _xp;
            set => _xp = value;
        }

        public double RequiredXp
        {
            get => _requiredXp;
            set => _requiredXp = value;
        }

        public double BaseXp
        {
            get => _baseXp;
            set => _baseXp = value;
        }

        public double XpRate
        {
            get => _xpRate;
            set => _xpRate = value;
        }

        public double XpSum
        {
            get => _xpSum;
            set => _xpSum = value;
        }

        public void AddXp(double addedXp)
        {
            _xp += addedXp;
            _xpSum += addedXp;

            int newLevel = (int) CalculateLevel(_baseXp, _xpRate, _xpSum) + 1;
            int levelDiff = newLevel - _level;
            if (levelDiff >= 1)
            {
                _xp -= _requiredXp;
                _requiredXp = CalculateRequiredXp(_baseXp, _xpRate, newLevel) -
                              CalculateRequiredXp(_baseXp, _xpRate, newLevel - 1);

                _level = newLevel;
                UpdateCombatStats(_level);
                _attack = 100 + (_level - 1) *CharacterConstants.COMBATMULTIPLIER; 
                LeveledUp?.Invoke();
            }
        }

        //calculate how much xp is needed from level 0 to level
        //WARNING level in this method is indexed from 0, but this class uses level from index 1
        public double CalculateRequiredXp(double baseXp, double rate, int level)
        {
            if ((1 - rate) == 0f)
            {
                return 0;
            }

            double numerator = 1 - Mathf.Pow((float) rate, level);
            double denominator = 1 - rate;
            double fraction = numerator / denominator;
            return baseXp * fraction;
        }

        //calculate level based on xpSum
        //WARNING level in this method is indexed from 0, but this class uses level from index 1
        public double CalculateLevel(double baseXp, double rate, double xpSum)
        {
            if (baseXp == 0)
            {
                return 0;
            }

            double numerator = xpSum * (1 - rate);
            double fraction = numerator / baseXp;
            double x = 1 - fraction;

            return Math.Log(x, rate);
        }

        public Player()
        {
            Load();
        }

        private void Init()
        {
            _level = 1;
            _hp = CharacterConstants.BASEHEALTH;
            _currentHp = _hp;
            _attack = CharacterConstants.BASEATTACK;
            _defense = 1;

            //progression system
            _baseXp = 100;
            _xpRate = 1.1;

            _xp = 0;
            _xpSum = 0;
            
            //testing
            // _xp = 90;
            // _xpSum = 90;
            // _attack = 100;
            
            _requiredXp = CalculateRequiredXp(_baseXp, _xpRate, 1);
        }

        public void Save()
        {
            SaveSystem.SavePlayer(this);
        }

        public void Load()
        {
            PlayerData playerData = SaveSystem.LoadPlayer();
            if (playerData is null || playerData.Hp == 0)
            {
                Init();
            }
            else
            {
                _level = playerData.Level;
                _xp = playerData.Xp;
                _requiredXp = playerData.RequiredXp;
                _baseXp = playerData.BaseXp;
                _xpRate = playerData.XpRate;
                _xpSum = playerData.XpSum;
                _hp = playerData.Hp;
                _attack = playerData.Attack;
            }
        }
    }
}