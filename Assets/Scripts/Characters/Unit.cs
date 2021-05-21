using System;

namespace LocationRPG
{
    public class Unit
    {
        protected int _level;

        protected double _hp;
        protected double _currentHp;

        protected double _attack;

        protected double _defense;

        public Unit()
        {
            _level = 1;

            _hp = 100;
            _defense = 1;
            _attack = 10;

            _currentHp = _hp;
        }

        public int Level
        {
            get => _level;
            set => _level = value;
        }

        public double Hp
        {
            get => _hp;
            set => _hp = value;
        }

        public double CurrentHp
        {
            get => _currentHp;
            set => _currentHp = value;
        }

        public double Defense
        {
            get => _defense;
            set => _defense = value;
        }

        public double Attack
        {
            get => _attack;
            set => _attack = value;
        }

        //if return = true then unit has died
        //          = false then unit didn't die
        public bool TakeDamage(double damage)
        {
            _currentHp -= damage;

            if (_currentHp <= 0)
            {
                return true;
            }

            return false;
        }

        public void IncreaseDefense(double defense)
        {
            _defense += defense;
        }


//TODO: geometricka postupnost a nie relativne pripocitanie k staremu levelu
        public void UpdateCombatStats(int level)
        {
            _attack = CharacterConstants.BASEATTACK + ((level - 1) * CharacterConstants.COMBATMULTIPLIER);
            _hp = CharacterConstants.BASEHEALTH + ((level - 1)) * CharacterConstants.COMBATMULTIPLIER;
            _currentHp = _hp;
        }
    }
}