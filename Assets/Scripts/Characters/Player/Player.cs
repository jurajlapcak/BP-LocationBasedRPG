using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LocationRPG
{
    public class Player : Unit
    {
        //TODO: inventory

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