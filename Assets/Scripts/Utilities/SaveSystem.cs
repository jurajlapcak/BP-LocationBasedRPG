using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LocationRPG
{

    //SAVE paths: https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html
    public static class SaveSystem
    {
        public static void SavePlayer(Player player)
        {
            string path = Application.persistentDataPath + "/player.bin";

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(path);
            PlayerData playerData = new PlayerData(player);
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