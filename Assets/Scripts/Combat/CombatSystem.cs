using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace LocationRPG
{
    public class CombatSystem : Singleton<CombatSystem>
    {
        [SerializeField] private CombatSceneManager combatSceneManager;
        [SerializeField] private CombatUIManager combatUIManager;

        private PlayerController _playerController;
        private MonsterController _monsterController;

        private CombatState _state;

        private bool _isInitialized;

        private const int PLAYER_TURN = 0;
        private const int MONSTER_TURN = 1;

        public CombatState State => _state;

        public bool IsInitialized => _isInitialized;

        //observer pattern
        public static event Action OnInitialize;
        
        private void OnEnable()
        {
            _isInitialized = false;
            Assert.IsNotNull(combatSceneManager);
            Assert.IsNotNull(combatUIManager);
        }

        private void Start()
        {
            _state = CombatState.START;
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            _playerController = combatSceneManager.PlayerController;
            _monsterController = combatSceneManager.MonsterController;

            StartCoroutine(UIInit());

            yield return new WaitUntil(() => _isInitialized);
            
            int turn = GenerateRandTurn();

            if (turn == PLAYER_TURN)
            {
                StartCoroutine(PlayerTurn());
            }
            else if (turn == MONSTER_TURN)
            {
                StartCoroutine(MonsterTurn());
            }
        }

        private IEnumerator UIInit()
        {
            yield return new WaitUntil(() => combatUIManager.IsInitialized);
            yield return new WaitUntil(() => combatSceneManager.IsInitialized);
            combatUIManager.CombatOverlay.PlayerHp.text = _playerController.Unit.CurrentHp + "/" + _playerController.Unit.Hp;
            combatUIManager.CombatOverlay.MonsterHp.text = _monsterController.Unit.CurrentHp + "/" + _monsterController.Unit.Hp;
            
            _isInitialized = true;
            OnInitialize?.Invoke();
        }

        private IEnumerator PlayerTurn()
        {
            _state = CombatState.PLAYERTURN;

            yield return new WaitForSeconds(1.5f);

            //Wait for player action
        }

        private IEnumerator MonsterTurn()
        {
            _state = CombatState.MONSTERTURN;

            //move to player
            _monsterController.MoveToPlayer(_playerController.gameObject);
            _monsterController.AnimationController.ToggleRunning();
            yield return new WaitForSeconds(1.5f);
            _monsterController.AnimationController.PlayAttack();
            yield return new WaitForSeconds(2f);
            _monsterController.AnimationController.ToggleRunning();

            //move back
            _monsterController.MoveToPlace();            
            yield return new WaitForSeconds(1.5f);
            _monsterController.AnimationController.ToggleIdle();

            //start PlayerTurn
            StartCoroutine(PlayerTurn());
        }

        private IEnumerator PlayerAttack()
        {
            //move to monster
            //play animation, moving => attacking
            _playerController.MoveToMonster(_monsterController.gameObject);
            _playerController.AnimationController.ToggleWalking();
            yield return new WaitForSeconds(1.5f);
            _playerController.AnimationController.PlayAttack();
            yield return new WaitForSeconds(2f);
            _playerController.AnimationController.ToggleWalking();

            //temporary dmg
            bool hasDied = _monsterController.Unit.TakeDamage(10f);
            float currentHp = 0f;
            //update UI current hp
            if (!hasDied)
            {
                currentHp = _monsterController.Unit.CurrentHp;
            }

            combatUIManager.CombatOverlay.MonsterHp.text = currentHp + "/" + _monsterController.Unit.Hp;
            combatUIManager.CombatOverlay.UpdateBar(CombatBars.MONSTERBAR, currentHp, _monsterController.Unit.Hp);

            //move back
            _playerController.MoveToPlace();            
            yield return new WaitForSeconds(1.5f);
            _playerController.AnimationController.ToggleIdle();

            //if monster dead then win
            if (hasDied)
            {
                _state = CombatState.WON;
                EndBattle();
            }
            else
            {
                //start EnemyTurn
                StartCoroutine(MonsterTurn());
            }
        }

        private IEnumerator PlayerDefend()
        {
            _playerController.Unit.IncreaseDefense(2f);

            yield return new WaitForSeconds(2f);

            //start EnemyTurn
            StartCoroutine(MonsterTurn());
        }

        private void EndBattle()
        {
            if (_state == CombatState.WON)
            {
                Debug.Log("You've WON!");
            }
            else if (_state == CombatState.LOST)
            {
                Debug.Log("You've LOST!");
            }
        }

        public void OnAttackButton()
        {
            Debug.Log(_state);
            if (_state != CombatState.PLAYERTURN)
            {
                return;
            }

            StartCoroutine(PlayerAttack());
        }

        public void OnDefendButton()
        {
            Debug.Log(_state);
            if (_state != CombatState.PLAYERTURN)
            {
                return;
            }

            StartCoroutine(PlayerDefend());
        }

        //generates random turn
        //returns int:  PLAYER_TURN = 0
        //              MONSTER_TURN = 1
        private int GenerateRandTurn()
        {
            bool lessFive = Random.Range(0, 10) >= 5;
            return (lessFive ? PLAYER_TURN : MONSTER_TURN);
        }
    }
}