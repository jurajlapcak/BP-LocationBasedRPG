using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Map;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
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

            Debug.Log("Before: "+_monsterController.BeforeAttackTime);
            Debug.Log("After: " + _monsterController.AfterAttackTime);
            Debug.Log("Remaining: "+_monsterController.RemainingTime);

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
            combatUIManager.CombatOverlay.PlayerHp.text =
                _playerController.Unit.CurrentHp + "/" + _playerController.Unit.Hp;
            combatUIManager.CombatOverlay.MonsterHp.text =
                _monsterController.Unit.CurrentHp + "/" + _monsterController.Unit.Hp;

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
            yield return StartCoroutine(MonsterAttack());
        }

        private IEnumerator MonsterAttack()
        {
            yield return Attack(_monsterController, _playerController);
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

        private IEnumerator PlayerAttack()
        {
            yield return Attack(_playerController, _monsterController);
        }

        private IEnumerator PlayerDefend()
        {
            _playerController.Unit.IncreaseDefense(2f);

            yield return new WaitForSeconds(2f);

            //start EnemyTurn
        }

        private IEnumerator Attack<T, TU>(UnitController<T> unitController, UnitController<TU> enemUnitController)
            where T : Unit where TU : Unit
        {
            //move to enemy
            unitController.MoveToEnemy(enemUnitController.gameObject);
            unitController.AnimationController.ToggleWalking();
            yield return new WaitForSeconds(1.5f);

            //attack
            unitController.AnimationController.PlayAttack();

            yield return new WaitForSeconds(unitController.BeforeAttackTime);
            enemUnitController.AnimationController.PlayHit();

            yield return new WaitForSeconds(unitController.AfterAttackTime);

            //temporary dmg
            bool hasDied = enemUnitController.Unit.TakeDamage(10f);
            float currentHp = 0f;
            //update UI current hp
            if (!hasDied)
            {
                currentHp = enemUnitController.Unit.CurrentHp;
            }

            if (_state == CombatState.PLAYERTURN)
            {
                combatUIManager.CombatOverlay.UpdateBar(CombatBars.MONSTERBAR, currentHp, enemUnitController.Unit.Hp);
            }
            else if (_state == CombatState.MONSTERTURN)
            {
                combatUIManager.CombatOverlay.UpdateBar(CombatBars.PLAYERBAR, currentHp, enemUnitController.Unit.Hp);
            }

            //wait remaining time
            yield return new WaitForSeconds(unitController.RemainingTime);

            //move back
            unitController.MoveToPlace();
            unitController.AnimationController.ToggleWalking();
            yield return new WaitForSeconds(1.5f);

            unitController.AnimationController.ToggleIdle();
            
            switch (_state)
            {
                case CombatState.PLAYERTURN :
                    if (hasDied)
                    {
                        _state = CombatState.WON;
                        EndBattle();
                        break;
                    }
                    StartCoroutine(MonsterTurn());
                    break;
                case CombatState.MONSTERTURN :
                    if (hasDied)
                    {
                        _state = CombatState.LOST;
                        EndBattle();
                        break;
                    }
                    StartCoroutine(PlayerTurn());
                    break;
                default:
                    Debug.Log("Weird state");
                    break;
            }
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