using UnityEngine;
using UnityEngine.Assertions;

namespace LocationRPG
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerModel;
        private Player _player;
        private PlayerAnimationController _playerAnimationController;

        public Player Player
        {
            get => _player;
            set => _player = value;
        }
        public PlayerAnimationController Animation => _playerAnimationController;
        
        private void Awake()
        {
            Assert.IsNotNull(playerModel);
            _player = new Player();
            _playerAnimationController = new PlayerAnimationController(playerModel.GetComponent<Animator>(),
                AnimationConstants.PLAYER_IDLE);
        }
    }
}