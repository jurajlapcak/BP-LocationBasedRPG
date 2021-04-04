using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LocationRPG
{
    public static class SaveSystem
    {
        public static void SavePlayer(PlayerStats playerStats)
        {
            string path = Application.persistentDataPath + "/player.bin";

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(path);
            PlayerData playerData = new PlayerData(playerStats);
            bf.Serialize(file, playerData);
            file.Close();
        }

        public static PlayerData LoadPlayer()
        {
            string path = Application.persistentDataPath + "/player.bin";

            if (File.Exists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path, FileMode.Open);
                PlayerData playerData = (PlayerData) bf.Deserialize(file);
                file.Close();

                return playerData;
            }
            else
            {
                Debug.Log("Save file not found in " + path);
                return null;
            }
        }
    }
}