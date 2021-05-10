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
            Application.targetFrameRate = 20;
        }

    }
}