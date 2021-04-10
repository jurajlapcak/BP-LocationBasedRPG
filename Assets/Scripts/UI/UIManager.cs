using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LocationRPG
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text xpText;
        [SerializeField] private Text levelText;
        [SerializeField] private GameObject menu;
        private PlayerController _currentPlayer = GameManager.Instance.CurrentPlayer;

        private void Awake()
        {
            Assert.IsNotNull(xpText);
            Assert.IsNotNull(levelText);
            Assert.IsNotNull(menu);
        }

        private void Update()
        {
            updateLevel();
            updateXP();
        }

        public void updateLevel()
        {
            levelText.text = _currentPlayer.Player.Level.ToString();
        }

        public void updateXP()
        {
            xpText.text = _currentPlayer.Player.Xp + " / " +
                          _currentPlayer.Player.RequiredXp;
        }
    }
}