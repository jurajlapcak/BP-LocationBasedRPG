using System;
using Animations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Characters.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject playerModel;
        private PlayerCombatController _playerCombatController;
        private PlayerAnimationController _playerAnimationController;
        private void Awake()
        {
            Assert.IsNotNull(playerModel);
            _playerCombatController = new PlayerCombatController();
            _playerAnimationController = new PlayerAnimationController(playerModel.GetComponent<Animator>(),
                AnimationConstants.PLAYER_IDLE_FOOTS);
        }
        
        public PlayerCombatController PlayerCombatController => _playerCombatController;
        public PlayerAnimationController Animation => _playerAnimationController;

    }
}
