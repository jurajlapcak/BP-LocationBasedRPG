using System;

namespace LocationRPG
{
    public class Unit
    {
        protected int _level;
        protected int _levelBase;

        protected float _hp;
        protected float _currentHp;

        protected float _defense;
        protected float _currentDefense;

        protected float _attack;

        public Unit()
        {
            _level = 1;

            _hp = 100;
            _defense = 1;
            _attack = 10;
            
            _currentHp = _hp;
            _currentDefense = _defense;
        }

        public int Level
        {
            get => _level;
            set => _level = value;
        }

        public int LevelBase
        {
            get => _levelBase;
            set => _levelBase = value;
        }

        public float Hp
        {
            get => _hp;
            set => _hp = value;
        }

        public float CurrentHp
        {
            get => _currentHp;
            set => _currentHp = value;
        }

        public float Defense
        {
            get => _defense;
            set => _defense = value;
        }

        public float Attack
        {
            get => _attack;
            set => _attack = value;
        }

        //if return = true then unit has died
        //          = false then unit didn't die
        public bool TakeDamage(float damage)
        {
            _currentHp -= damage;

            if (_currentHp <= 0)
            {
                return true;
            }
            
            return false;
        }

        public void IncreaseDefense(float defense)
        {
            _currentDefense += defense;
        }
    }
}