using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace LocationRPG
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerModel;
        [SerializeField]private Player player;
        private PlayerAnimationController _playerAnimationController;

        public Player Player
        {
            get => player;
            set => player = value;
        }
        public PlayerAnimationController Animation => _playerAnimationController;

        private void OnEnable()
        {
            Assert.IsNotNull(playerModel);
            Assert.IsNotNull(player);
        }

        private void Awake()
        {
            // LoadPlayer();
            _playerAnimationController = new PlayerAnimationController(playerModel.GetComponent<Animator>(),
                AnimationConstants.PLAYER_IDLE);
        }

        public void SavePlayer()
        {
            player.Save();
        }

        public void LoadPlayer()
        {
            player.Load();
        }
    }
}