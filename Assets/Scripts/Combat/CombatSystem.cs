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
        private AttackState _attackState;

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
            _attackState = AttackState.NONE;
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            yield return new WaitUntil(() => CombatSceneManager.Instance.IsInitialized);

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

            combatUIManager.CombatOverlay.UpdateBar(CombatBars.PLAYERBAR, _playerController.Unit.CurrentHp,
                _playerController.Unit.Hp);
            combatUIManager.CombatOverlay.UpdateBar(CombatBars.MONSTERBAR, _monsterController.Unit.CurrentHp,
                _monsterController.Unit.Hp);

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
            _attackState = AttackState.MONSTERATTACKING;
            yield return Attack(_monsterController, _playerController);
        }

        public void OnAttackButton()
        {
            if (_state != CombatState.PLAYERTURN || _attackState == AttackState.PLAYERATTACKING)
            {
                return;
            }

            _attackState = AttackState.PLAYERATTACKING;
            StartCoroutine(PlayerAttack());
        }

        public void OnDefendButton()
        {
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
            yield return new WaitForSeconds(UnitController<Unit>.MoveTime);

            //attack
            unitController.AnimationController.PlayAttack();

            yield return new WaitForSeconds(unitController.BeforeAttackTime);
            enemUnitController.AnimationController.PlayHit();

            yield return new WaitForSeconds(unitController.AfterAttackTime);

            //temporary dmg
            bool hasDied = enemUnitController.Unit.TakeDamage(unitController.Unit.Attack);
            double currentHp = 0f;
            //update UI current hp
            if (!hasDied)
            {
                currentHp = enemUnitController.Unit.CurrentHp;
            }
            else
            {
                enemUnitController.AnimationController.PlayDying();
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
            yield return new WaitForSeconds(UnitController<Unit>.MoveTime);

            unitController.AnimationController.ToggleIdle();

            switch (_state)
            {
                case CombatState.PLAYERTURN:
                    if (hasDied)
                    {
                        _state = CombatState.WON;
                        EndBattle();
                        break;
                    }

                    StartCoroutine(MonsterTurn());
                    break;
                case CombatState.MONSTERTURN:
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
                Monster monster = (Monster)_monsterController.Unit;
                _playerController.Unit.AddXp(monster.RewardXp);
                
                combatUIManager.ResultOverlay.ShowWin();
            }
            else if (_state == CombatState.LOST)
            {
                combatUIManager.ResultOverlay.ShowLose();
            }
            
            
            _playerController.SavePlayer();
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