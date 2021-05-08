namespace LocationRPG
{
    public class Skeleton : Monster
    {
        public Skeleton(Player player): base()
        {
            _level = player.Level;
        }
    }
}