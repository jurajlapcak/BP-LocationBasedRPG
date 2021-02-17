namespace LocationRPG
{
    public class PlayerCombatController 
    {
        private int level = 1;
        private int xp = 0;
        private int requiredXp = 100;
        private int levelBase = 100;
        private float hp = 100;
        private float defense = 1;
        private float attack = 1;
        public int Level => level;

        public int Xp => xp;

        public int RequiredXp => requiredXp;

        public int LevelBase => levelBase;

        public float Hp => hp;


    }
}