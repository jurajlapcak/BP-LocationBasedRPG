using UnityEngine;
using UnityEngine.Assertions;

namespace LocationRPG
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private PlayerController currentPlayer;
        
        public PlayerController CurrentPlayer => currentPlayer;

        private void Awake()
        {
            Assert.IsNotNull(currentPlayer);
        }

        public void Save()
        {
            currentPlayer.Player.Save();
        }

        public void Load()
        {
            currentPlayer.Player.Load();
        }
    }
}