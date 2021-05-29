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
            _state = CombatState.Start;
            _attackState = AttackState.None;
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

            combatUIManager.CombatOverlay.UpdateBar(CombatBars.PlayerBar, _playerController.Unit.CurrentHp,
                _playerController.Unit.Hp);
            combatUIManager.CombatOverlay.UpdateBar(CombatBars.MonsterBar, _monsterController.Unit.CurrentHp,
                _monsterController.Unit.Hp);

            _isInitialized = true;
            OnInitialize?.Invoke();
        }

        private IEnumerator PlayerTurn()
        {
            _playerController.Unit.Defense = 0;
            _state = CombatState.PlayerTurn;
            yield return new WaitForSeconds(1.5f);
            //Wait for player action
        }

        private IEnumerator MonsterTurn()
        {
            _state = CombatState.EnemyTurn;
            yield return StartCoroutine(MonsterAttack());
        }

        private IEnumerator MonsterAttack()
        {
            _attackState = AttackState.MonsterAttacking;
            yield return Attack(_monsterController, _playerController);
        }

        public void OnAttackButton()
        {
            if (_state != CombatState.PlayerTurn || _attackState == AttackState.PlayerAttacking)
            {
                return;
            }

            _attackState = AttackState.PlayerAttacking;
            StartCoroutine(PlayerAttack());
        }

        public void OnDefendButton()
        {
            if (_state != CombatState.PlayerTurn || _attackState == AttackState.PlayerAttacking)
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
            _playerController.Unit.IncreaseDefense();
            yield return new WaitForSeconds(2f);
            
            ChangeTurn();
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

            if (_state == CombatState.PlayerTurn)
            {
                combatUIManager.CombatOverlay.UpdateBar(CombatBars.MonsterBar, currentHp, enemUnitController.Unit.Hp);
            }
            else if (_state == CombatState.EnemyTurn)
            {
                combatUIManager.CombatOverlay.UpdateBar(CombatBars.PlayerBar, currentHp, enemUnitController.Unit.Hp);
            }

            //wait remaining time
            yield return new WaitForSeconds(unitController.RemainingTime);

            //move back
            unitController.MoveToPlace();
            unitController.AnimationController.ToggleWalking();
            yield return new WaitForSeconds(UnitController<Unit>.MoveTime);

            unitController.AnimationController.ToggleIdle();

            ChangeTurn(hasDied);
        }

        private void ChangeTurn(bool hasDied = false){
            switch (_state)
            {
                case CombatState.PlayerTurn:
                    if (hasDied)
                    {
                        _state = CombatState.Won;
                        EndBattle();
                        break;
                    }

                    StartCoroutine(MonsterTurn());
                    break;
                case CombatState.EnemyTurn:
                    if (hasDied)
                    {
                        _state = CombatState.Lost;
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
            if (_state == CombatState.Won)
            {
                Monster monster = (Monster)_monsterController.Unit;
                _playerController.Unit.AddXp(monster.RewardXp);
                
                combatUIManager.ResultOverlay.ShowWin();
            }
            else if (_state == CombatState.Lost)
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