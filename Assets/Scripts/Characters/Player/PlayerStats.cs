using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LocationRPG
{
    public class PlayerStats
    {
        private int _level = 1;
        private int _xp = 0;
        private int _requiredXp = 10;
        private int _levelBase = 10;

        private float _hp = 100;
        private float _defense = 1;
        private float _attack = 1;

        //TODO: inventory

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

        //TODO: init
        public void Init()
        {
        }

        public void Save()
        {
            SaveSystem.SavePlayer(this);
        }

        public void Load()
        {
            PlayerData playerData = SaveSystem.LoadPlayer();
            if (playerData is null)
            {
                Init();
            }
            else
            {
                _level = playerData.Level;
                _xp = playerData.Xp;
                _requiredXp = playerData.RequiredXp;
                _levelBase = playerData.LevelBase;

                _hp = playerData.Hp;
                _defense = playerData.Defense;
                _attack = playerData.Attack;
            }
        }
    }
}