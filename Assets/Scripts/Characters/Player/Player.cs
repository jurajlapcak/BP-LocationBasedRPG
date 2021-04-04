using UnityEngine;
using UnityEngine.Assertions;

namespace LocationRPG
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject playerModel;
        private PlayerStats _playerStats;
        private PlayerAnimationController _playerAnimationController;

        private void Awake()
        {
            Assert.IsNotNull(playerModel);
            _playerStats = new PlayerStats();
            _playerAnimationController = new PlayerAnimationController(playerModel.GetComponent<Animator>(),
                AnimationConstants.PLAYER_IDLE);
        }

        public PlayerStats PlayerStats => _playerStats;
        public PlayerAnimationController Animation => _playerAnimationController;
    }
}