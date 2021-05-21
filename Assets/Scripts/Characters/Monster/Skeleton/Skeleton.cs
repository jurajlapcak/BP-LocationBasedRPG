namespace LocationRPG
{
    public class Skeleton : Monster
    {
        private const float BASEREWARDXP = 10;
        
        public Skeleton(Player player): base()
        {
            _level = player.Level;
            
            _rewardXp = BASEREWARDXP + ((_level - 1) * CharacterConstants.LEVELMULTIPLIER);
            UpdateCombatStats(_level);
        }
    }
}