using System;
using UnityEngine;

namespace LocationRPG
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] protected int _level;
        [SerializeField] protected int _xp;
        [SerializeField] protected int _requiredXp;
        [SerializeField] protected int _levelBase;

        [SerializeField] protected float _hp;
        [SerializeField] protected float _currentHp;

        [SerializeField] protected float _defense;
        [SerializeField] protected float _currentDefense;

        [SerializeField] protected float _attack;

        private void OnEnable()
        {
            _currentHp = _hp;
            _currentDefense = _defense;
        }

        public int Level
        {
            get => _level;
            set => _level = value;
        }

        public int Xp
        {
            get => _xp;
            set => _xp = value;
        }

        public int RequiredXp
        {
            get => _requiredXp;
            set => _requiredXp = value;
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

            Debug.Log(_currentHp);
            
            return false;
        }

        public void IncreaseDefense(float defense)
        {
            _currentDefense += defense;
            
            Debug.Log(_currentDefense);
        }
    }
}