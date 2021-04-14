using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LocationRPG
{
    public class CombatSystem : Singleton<CombatSystem>
    {
        [SerializeField] private CombatSceneManager combatSceneManager;

        private Unit _player;
        private Unit _monster;

        private CombatState _state;

        private const int PLAYER_TURN = 0;
        private const int MONSTER_TURN = 1;

        public CombatState State => _state;

        private void Start()
        {
            _state = CombatState.START;
            StartCoroutine(SetupCombat());
        }

        private IEnumerator SetupCombat()
        {
            _player = combatSceneManager.Player;
            _monster = combatSceneManager.Monster;

            int turn = GenerateRandTurn();

            yield return new WaitForSeconds(2f);

            if (turn == PLAYER_TURN)
            {
                _state = CombatState.PLAYERTURN;
                StartCoroutine(PlayerTurn());
            }
            else if (turn == MONSTER_TURN)
            {
                _state = CombatState.MONSTERTURN;
                StartCoroutine(MonsterTurn());
            }
        }

        private IEnumerator PlayerTurn()
        {
            yield return new WaitForSeconds(2f);
        }

        private IEnumerator MonsterTurn()
        {
            yield return new WaitForSeconds(2f);
            
            _state = CombatState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }

        private IEnumerator PlayerAttack()
        {
            //temporary dmg
            bool hasDied = _monster.TakeDamage(10f);
            
            //update UI current hp
            
            yield return new WaitForSeconds(2f);
            
            //if monster dead then win
            if (hasDied)
            {
                _state = CombatState.WON;
                EndBattle();
            }
            else
            {
                //Change state to EnemyTurn
                _state = CombatState.MONSTERTURN;
                StartCoroutine(MonsterTurn());
            }
        }
        
        private IEnumerator PlayerDefend()
        {
            _player.IncreaseDefense(2f);
            
            yield return new WaitForSeconds(2f);
            
            //Change state to EnemyTurn
            _state = CombatState.MONSTERTURN;
            StartCoroutine(MonsterTurn());
        }

        private void EndBattle()
        {
            if (_state == CombatState.WON)
            {
                Debug.Log("You've WON!");
            }else if (_state == CombatState.LOST)
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