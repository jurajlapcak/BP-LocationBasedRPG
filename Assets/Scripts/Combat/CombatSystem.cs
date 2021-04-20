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

        private Unit _player;
        private Unit _monster;

        private CombatState _state;

        private bool _isInitialized;

        private const int PLAYER_TURN = 0;
        private const int MONSTER_TURN = 1;

        public CombatState State => _state;

        public bool IsInitialized => _isInitialized;

        private void OnEnable()
        {
            _isInitialized = false;
            Assert.IsNotNull(combatSceneManager);
            Assert.IsNotNull(combatUIManager);
        }

        private void Start()
        {
            _state = CombatState.START;
            StartCoroutine(SetupCombat());
        }

        private IEnumerator SetupCombat()
        {
            _player = combatSceneManager.Player;
            _monster = combatSceneManager.Monster;

            StartCoroutine(UIInit());

            int turn = GenerateRandTurn();

            _isInitialized = true;

            yield return new WaitForSeconds(2f);

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
            combatUIManager.CombatOverlay.PlayerHp.text = _player.CurrentHp + "/" + _player.Hp;
            combatUIManager.CombatOverlay.MonsterHp.text = _monster.CurrentHp + "/" + _monster.Hp;
        }

        private IEnumerator PlayerTurn()
        {
            _state = CombatState.PLAYERTURN;

            yield return new WaitForSeconds(2f);

            //Wait for player action
        }

        private IEnumerator MonsterTurn()
        {
            _state = CombatState.MONSTERTURN;

            yield return new WaitForSeconds(2f);

            //start PlayerTurn
            StartCoroutine(PlayerTurn());
        }

        private IEnumerator PlayerAttack()
        {
            //temporary dmg
            bool hasDied = _monster.TakeDamage(10f);
            float currentHp = 0f;
            //update UI current hp
            if (!hasDied)
            {
                currentHp = _monster.CurrentHp;
            }

            combatUIManager.CombatOverlay.MonsterHp.text = currentHp + "/" + _monster.Hp;
            combatUIManager.CombatOverlay.UpdateBar(CombatBars.MONSTERBAR, currentHp, _monster.Hp);

            yield return new WaitForSeconds(2f);

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
            _player.IncreaseDefense(2f);

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