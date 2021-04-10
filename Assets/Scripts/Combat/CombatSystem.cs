using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LocationRPG
{
    public class CombatSystem : MonoBehaviour
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
                PlayerTurn();
            }
            else if (turn == MONSTER_TURN)
            {
                _state = CombatState.MONSTERTURN;
                MonsterTurn();
            }
        }

        private void PlayerTurn()
        {
        }

        private void MonsterTurn()
        {
        }

        private IEnumerator PlayerAttack()
        {
            yield return new WaitForSeconds(2f);
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