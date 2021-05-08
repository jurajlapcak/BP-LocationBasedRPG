namespace LocationRPG
{
    public class Player : Unit
    {
        private int _xp;
        private int _requiredXp;
        
        public int Xp
        {
            get => _xp;
            set => _xp = value;
        }

        public int RequiredXp
        {
            get => _requiredXp;
            set => _requiredXp = value;
        }
        
        public Player() : base()
        {
            _xp = 0;
            _requiredXp = 100;
        }
        
        //TODO: init
        public void Init()
        {
        }

        public void Save()
        {
            SaveSystem.SavePlayer(this);
        }

        public void Load()
        {
            PlayerData playerData = SaveSystem.LoadPlayer();
            if (playerData is null)
            {
                Init();
            }
            else
            {
                _level = playerData.Level;
                _xp = playerData.Xp;
                _requiredXp = playerData.RequiredXp;
                _levelBase = playerData.LevelBase;

                _hp = playerData.Hp;
                _defense = playerData.Defense;
                _attack = playerData.Attack;
            }
        }
    }
}