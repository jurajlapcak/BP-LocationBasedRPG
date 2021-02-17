using UnityEngine;
using UnityEngine.Assertions;

namespace LocationRPG
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private Player currentPlayer;
        public Player CurrentPlayer => currentPlayer;

        private void Awake()
        {
            Assert.IsNotNull(currentPlayer);
        }
    }
}